using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SimpleWallet.Application.DTOs.Base;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Application.DTOs;

public class MovementDto : BaseDto
{

    [JsonPropertyName("walletId")]
    [Required(ErrorMessage = "Wallet ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Wallet ID must be a positive number")]
    public int WalletId { get; set; }

    [JsonPropertyName("amount")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number")]
    [Required(ErrorMessage = "Amount is required")]
    public decimal Amount { get; set; }

    [JsonPropertyName("movementType")]
    [Required(ErrorMessage = "Movement Type is required")]
    [EnumDataType(typeof(MovementType))]
    public MovementType Type { get; set; }
}
public class MovementCreateDto
{
    [JsonPropertyName("walletId")]
    [Required(ErrorMessage = "Wallet ID is required")]
    public int WalletId { get; set; }

    [JsonPropertyName("amount")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number")]
    [Required(ErrorMessage = "Amount is required")]
    public decimal Amount { get; set; }

    [JsonPropertyName("movementType")]
    [Required(ErrorMessage = "Movement Type is required")]
    [EnumDataType(typeof(MovementType))]
    public MovementType Type { get; set; }
}

public class MovementUpdateDto
{

    [JsonPropertyName("walletId")]
    [Required(ErrorMessage = "Wallet ID is required")]
    public int WalletId { get; set; }

    [JsonPropertyName("amount")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number")]
    public decimal Amount { get; set; }

    [JsonPropertyName("movementType")]
    [EnumDataType(typeof(MovementType))]
    public MovementType Type { get; set; }
}