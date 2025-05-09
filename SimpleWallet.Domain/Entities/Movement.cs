using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleWallet.Domain.Entities.Base;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Domain.Entities
{
    public class Movement : BaseEntity
    {
        public int WalletId { get; set; }

        public decimal Amount { get; set; }

        public MovementType Type { get; set; }

        public Movement(int walletId, decimal amount, MovementType type) : base()
        {
            WalletId = walletId;
            Amount = amount;
            Type = type;
        }

        public Movement(int id, int walletId, decimal amount, MovementType type) : base(id)
        {
            WalletId = walletId;
            Amount = amount;
            Type = type;
        }

        public void UpdateAmount(decimal amount)
        {
            Amount += amount;
        }
    }
}