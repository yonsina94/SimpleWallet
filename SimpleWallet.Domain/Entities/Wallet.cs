using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleWallet.Domain.Entities.Base;

namespace SimpleWallet.Domain.Entities
{
    public class Wallet: AuditableBaseEntity
    {
        [Column("DocumentId")]
        [Required(ErrorMessage = "Document ID is required")]
        public string DocumentId { get; set; }

        [Column("Name", TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Column("Balance")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive number")]
        [Required(ErrorMessage = "Balance is required")]
        public decimal Balance { get; set; }

        public IEnumerable<Movement> Movements { get; set; } = [];

        public Wallet(string documentId, string name, decimal balance): base()
        {
            DocumentId = documentId;
            Name = name;
            Balance = balance;
        }
      
       public Wallet(int id,string documentId, string name, decimal balance): base(id)
        {
            DocumentId = documentId;
            Name = name;
            Balance = balance;
        }

        public void UpdateBalance(decimal amount)
        {
            Balance += amount;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}