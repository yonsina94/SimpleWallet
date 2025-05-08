using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWallet.Domain.Entities.Base;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public BaseEntity(int id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    public BaseEntity(int id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}