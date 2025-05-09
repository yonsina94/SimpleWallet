using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Application.Response;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Application.Features.Wallet.Create;

public class CreateWalletCommand : IRequest<Response<WalletDto>>
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

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Response<WalletDto>>
{
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateWalletCommandHandler> _logger;

    public CreateWalletCommandHandler(IWalletService walletService, IMapper mapper, ILogger<CreateWalletCommandHandler> logger)
    {
        _walletService = walletService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response<WalletDto>> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newWallet = new Domain.Entities.Wallet(request.DocumentId, request.DocumentType, request.Name, request.Balance);

            var createdWallet = await _walletService.CreateAsync(newWallet);

            var resp = Response<WalletDto>.Ok(_mapper.Map<WalletDto>(createdWallet), "Wallet created successfully.");

            _logger.LogInformation($"Wallet with ID {createdWallet.Id} created successfully.");

            return resp;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the wallet.");
            return Response<WalletDto>.Fail("An error occurred while creating the wallet.", details: [new("CreateWalletError", ex.Message)]);
        }
    }
}