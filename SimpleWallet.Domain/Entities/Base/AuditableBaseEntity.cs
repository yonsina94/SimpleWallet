using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWallet.Domain.Entities.Base;

/// <summary>
/// Represents an abstract base entity that includes audit information, 
/// specifically the date and time when the entity was last updated.
/// </summary>
public abstract class AuditableBaseEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// This property is automatically set to the current UTC date and time 
    /// when the entity is instantiated.
    /// </summary>
    [Column("UpdatedAt")]
    public virtual DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableBaseEntity"/> class
    /// and sets the <see cref="UpdatedAt"/> property to the current UTC date and time.
    /// </summary>
    public AuditableBaseEntity()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableBaseEntity"/> class
    /// with the specified identifier and sets the <see cref="UpdatedAt"/> property 
    /// to the current UTC date and time.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    public AuditableBaseEntity(int id) : base(id)
    {
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableBaseEntity"/> class
    /// with the specified identifier and creation date, and sets the 
    /// <see cref="UpdatedAt"/> property to the current UTC date and time.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="createdAt">The date and time when the entity was created.</param>
    public AuditableBaseEntity(int id, DateTime createdAt) : base(id, createdAt)
    {
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableBaseEntity"/> class
    /// with the specified identifier, creation date, and last updated date.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="createdAt">The date and time when the entity was created.</param>
    /// <param name="updatedAt">The date and time when the entity was last updated.</param>
    public AuditableBaseEntity(int id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt)
    {
        UpdatedAt = updatedAt;
    }
}