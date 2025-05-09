namespace SimpleWallet.Application.DTOs.Base;

public abstract class BaseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    protected BaseDto()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    protected BaseDto(int id) : this()
    {
        Id = id;
    }

    protected BaseDto(int id, DateTime createdAt, DateTime? updatedAt) : this(id)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}