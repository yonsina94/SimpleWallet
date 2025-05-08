using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWallet.Domain.Entities.Base;

public abstract class AuditableBaseEntity : BaseEntity
{
    [Column("UpdatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public AuditableBaseEntity()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
    public AuditableBaseEntity(int id) : base(id)
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public AuditableBaseEntity(int id, DateTime createdAt) : base(id, createdAt)
    {
        UpdatedAt = DateTime.UtcNow;
    }
    public AuditableBaseEntity(int id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt)
    {
        UpdatedAt = updatedAt;
    }
    
}