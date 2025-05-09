namespace SimpleWallet.Infraestructure.Repositories.Implementation;

using SimpleWallet.Domain.Entities;
using SimpleWallet.Infraestructure.Context;
using SimpleWallet.Infraestructure.Repositories.Implementation.Base;
using SimpleWallet.Infraestructure.Repositories.Interfaces;

public class WalletRepository(SimpleWalletDbContext context) : BaseRepository<Wallet>(context), IWalletRepository
{
}