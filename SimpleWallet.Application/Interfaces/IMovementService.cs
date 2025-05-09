using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces.Base;
using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Application.Interfaces;

public interface IMovementService : IBaseService<Movement, MovementDto>
{
    Task<IEnumerable<MovementDto>> GetByWalletIdAsync(int walletId);
    Task<IEnumerable<MovementDto>> GetByWalletIdAndDateRangeAsync(int walletId, DateTime startDate, DateTime endDate);
}