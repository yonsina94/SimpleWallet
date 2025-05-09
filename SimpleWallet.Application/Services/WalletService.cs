namespace SimpleWallet.Application.Services;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Exceptions;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Application.Services.Base;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;
using SimpleWallet.Infraestructure.Context;
using SimpleWallet.Infraestructure.Repositories.Interfaces;

public class WalletService(IMapper mapper, SimpleWalletDbContext dbContext, IWalletRepository repository, IMovementRepository movementRepository, ILogger<WalletService> logger) : BaseService<Wallet, WalletDto>(mapper, repository), IWalletService
{
    private readonly IMovementRepository _movementRepository = movementRepository;
    private readonly ILogger<WalletService> _logger = logger;

    private readonly SimpleWalletDbContext _dbContext = dbContext;
    public async Task<WalletDto> GetByDocumentIdAsync(string documentId)
    {
        ArgumentNullException.ThrowIfNull(documentId);

        var wallet = (await _repository.GetAllAsync(w => w.DocumentId == documentId)).FirstOrDefault();
        if (wallet == null)
        {
            _logger.LogWarning($"Wallet with Document ID {documentId} not found.");
            throw new KeyNotFoundException($"Wallet with Document ID {documentId} not found.");
        }

        return await Task.FromResult(_mapper.Map<WalletDto>(wallet));
    }

    public async Task<IEnumerable<WalletDto>> GetByDocumentTypeAsync(DocumentType documentType)
    {
        ArgumentNullException.ThrowIfNull(documentType);

        var wallets = await _repository.GetAllAsync(w => w.DocumentType == documentType);
        return await Task.FromResult(_mapper.Map<IEnumerable<WalletDto>>(wallets));
    }

    public async Task<bool> TransferFundsAsync(int fromWalletId, int toWalletId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ValidationException("Transfer amount must be greater than zero.");
        }

        var executionStrategy = _dbContext.Database.CreateExecutionStrategy();

        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var fromWallet = await _repository.GetByIdAsync(fromWalletId);
                    var toWallet = await _repository.GetByIdAsync(toWalletId);

                    if (fromWallet == null)
                    {
                        _logger.LogWarning($"Source wallet with id {fromWalletId} not found.");
                        throw new NotFoundException($"Source wallet with id {fromWalletId} not found.");
                    }

                    if (toWallet == null)
                    {
                        _logger.LogWarning($"Destination wallet with id {toWalletId} not found.");
                        throw new NotFoundException($"Destination wallet with id {toWalletId} not found.");
                    }

                    if (fromWallet.Balance < amount)
                    {
                        _logger.LogWarning($"Insufficient funds in wallet {fromWalletId}. Attempted to transfer {amount}, but only {fromWallet.Balance} is available.");
                        throw new InsufficientFundsException(
                            $"Insufficient funds in wallet {fromWalletId} to transfer {amount}. Available: {fromWallet.Balance}",
                            amount,
                            fromWallet.Balance);
                    }

                    fromWallet.Withdraw(amount);
                    toWallet.Deposit(amount);

                    await _repository.UpdateAsync(fromWallet);
                    await _repository.UpdateAsync(toWallet);

                    await _movementRepository.AddAsync(new Movement(fromWalletId, -amount, MovementType.Debit));
                    await _movementRepository.AddAsync(new Movement(toWalletId, amount, MovementType.Credit));

                    await _movementRepository.SaveChangesAsync();
                    await _repository.SaveChangesAsync();

                    await transaction.CommitAsync();
                    _logger.LogInformation($"Funds transfer of {amount} from wallet {fromWalletId} to wallet {toWalletId} completed.");
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "An error occurred during the funds transfer.");
                    throw new Exception("An error occurred during the funds transfer.", ex);
                }
            }
        });
    }
}