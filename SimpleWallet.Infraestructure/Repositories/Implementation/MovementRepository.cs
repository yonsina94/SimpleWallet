using SimpleWallet.Domain.Entities;
using SimpleWallet.Infraestructure.Context;
using SimpleWallet.Infraestructure.Repositories.Implementation.Base;
using SimpleWallet.Infraestructure.Repositories.Interfaces;

namespace SimpleWallet.Infraestructure.Repositories.Implementation;

public class MovementRepository(SimpleWalletDbContext context) : BaseRepository<Movement>(context), IMovementRepository
{
}