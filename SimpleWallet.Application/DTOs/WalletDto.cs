using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SimpleWallet.Application.DTOs.Base;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Application.DTOs;

public class WalletDto : BaseDto
{

    [JsonPropertyName("documentId")]
    [Required(ErrorMessage = "Document ID is required")]
    [StringLength(20, ErrorMessage = "Document ID cannot be longer than 20 characters")]
    public string DocumentId { get; set; }

    [JsonPropertyName("documentType")]
    [Required(ErrorMessage = "Document Type is required")]
    [EnumDataType(typeof(DocumentType))]
    public DocumentType DocumentType { get; set; }

    [JsonPropertyName("name")]
    [Required(ErrorMessage = "Name is required")]
    [StringLength(120, ErrorMessage = "Name cannot be longer than 120 characters")]
    public string Name { get; set; }

    [JsonPropertyName("balance")]
    [DataType(DataType.Currency)]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Balance must be a positive number")]
    [Required(ErrorMessage = "Balance is required")]
    public decimal Balance { get; set; }
}