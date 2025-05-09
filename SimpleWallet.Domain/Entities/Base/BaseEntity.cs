using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWallet.Domain.Entities.Base;

/// <summary>
/// Represents the base entity class that provides common properties for all entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the entity.
    /// Defaults to the current UTC time when the entity is instantiated.
    /// </summary>
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class
    /// with the default creation timestamp.
    /// </summary>
    public BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class
    /// with the specified identifier and the default creation timestamp.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    public BaseEntity(int id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class
    /// with the specified identifier and creation timestamp.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="createdAt">The creation timestamp of the entity.</param>
    public BaseEntity(int id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}