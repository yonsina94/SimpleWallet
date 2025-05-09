using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Application.Services.Base;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Infraestructure.Repositories.Interfaces;

namespace SimpleWallet.Application.Services;

public class MovementService(IMapper mapper, IMovementRepository repository, ILogger<MovementService> logger) : BaseService<Movement, MovementDto>(mapper, repository), IMovementService
{
    private readonly ILogger<MovementService> _logger = logger;

    public async Task<IEnumerable<MovementDto>> GetByWalletIdAndDateRangeAsync(int walletId, DateTime startDate, DateTime endDate)
    {
        ArgumentNullException.ThrowIfNull(walletId);
        ArgumentNullException.ThrowIfNull(startDate);
        ArgumentNullException.ThrowIfNull(endDate);

        var movements = await _repository.GetAllAsync(m => m.WalletId == walletId && m.CreatedAt >= startDate && m.CreatedAt <= endDate);
        return await Task.FromResult(_mapper.Map<IEnumerable<MovementDto>>(movements.ToList()));
    }

    public async Task<IEnumerable<MovementDto>> GetByWalletIdAsync(int walletId)
    {
        ArgumentNullException.ThrowIfNull(walletId);

        var movements = await _repository.GetAllAsync(m => m.WalletId == walletId);
        return await Task.FromResult(_mapper.Map<IEnumerable<MovementDto>>(movements.ToList()));
    }
}