using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using SimpleWallet.Domain.Entities.Base;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Domain.Entities
{
    public class Wallet : AuditableBaseEntity
    {
        public string DocumentId { get; set; }

        public DocumentType DocumentType { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Wallet(string documentId, DocumentType documentType, string name, decimal balance) : base()
        {
            DocumentId = documentId;
            DocumentType = documentType;
            Name = name;
            Balance = balance;
        }

        public Wallet(int id, string documentId, DocumentType documentType, string name, decimal balance) : base(id)
        {
            DocumentId = documentId;
            DocumentType = documentType;
            Name = name;
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero");
            }
            Balance -= amount;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}