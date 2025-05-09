using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces.Base;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Application.Interfaces;

public interface IWalletService : IBaseService<Wallet, WalletDto>
{
    Task<WalletDto> GetByDocumentIdAsync(string documentId);
    Task<IEnumerable<WalletDto>> GetByDocumentTypeAsync(DocumentType documentType);
    Task<bool> TransferFundsAsync(int fromWalletId, int toWalletId, decimal amount);
}