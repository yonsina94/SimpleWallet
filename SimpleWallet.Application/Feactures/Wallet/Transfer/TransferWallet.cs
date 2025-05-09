using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Exceptions;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Application.Response;

namespace SimpleWallet.Application.Features.Wallet.Transfer;


public record TransferWalletCommandResponse(int FromWalletId, int ToWalletId, decimal Amount);

public class TransferWalletCommand : IRequest<Response<TransferWalletCommandResponse>>
{
    [JsonPropertyName("fromWalletId")]
    [Required(ErrorMessage = "From Wallet ID is required")]
    public int FromWalletId { get; set; }

    [JsonPropertyName("toWalletId")]
    [Required(ErrorMessage = "To Wallet ID is required")]
    public int ToWalletId { get; set; }

    [JsonPropertyName("amount")]
    [DataType(DataType.Currency)]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number")]
    [Required(ErrorMessage = "Amount is required")]
    public decimal Amount { get; set; }
}

public class TransferWalletCommandHandler(IWalletService walletService, IMapper mapper, ILogger<TransferWalletCommandHandler> logger) : IRequestHandler<TransferWalletCommand, Response<TransferWalletCommandResponse>>
{
    private readonly IWalletService _walletService = walletService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<TransferWalletCommandHandler> _logger = logger;

    public async Task<Response<TransferWalletCommandResponse>> Handle(TransferWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _walletService.TransferFundsAsync(request.FromWalletId, request.ToWalletId, request.Amount);

            var resp = Response<TransferWalletCommandResponse>.Ok(new TransferWalletCommandResponse(request.FromWalletId, request.ToWalletId, request.Amount), "Transfer completed successfully.");

            _logger.LogInformation($"Transfer of {request.Amount} from Wallet ID {request.FromWalletId} to Wallet ID {request.ToWalletId} completed successfully.");

            return resp;
        }
        catch (InsufficientFundsException ex)
        {
            _logger.LogWarning(ex, "Insufficient funds for transfer");
            return Response<TransferWalletCommandResponse>.Fail($"Insufficient funds in Wallet ID {request.FromWalletId}. Current balance: {ex.CurrentBalance}, attempted debit: {ex.AttemptedDebitAmount}");
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Wallet not found");
            return Response<TransferWalletCommandResponse>.Fail($"Wallet not found. {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error transferring between wallets");
            return Response<TransferWalletCommandResponse>.Fail("An error occurred while transferring between wallets.");
        }
    }
}