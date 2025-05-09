using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Application.Response;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Application.Features.Wallet.Update;

public class UpdateWalletCommand : IRequest<Response<WalletDto>>
{

    [JsonPropertyName("id")]
    [Required(ErrorMessage = "ID is required")]
    public int Id { get; set; }

    [JsonPropertyName("documentId")]
    [StringLength(20, ErrorMessage = "Document ID cannot be longer than 20 characters")]
    public string DocumentId { get; set; }

    [JsonPropertyName("documentType")]
    [EnumDataType(typeof(DocumentType))]
    public DocumentType DocumentType { get; set; }

    [JsonPropertyName("name")]
    [StringLength(120, ErrorMessage = "Name cannot be longer than 120 characters")]
    public string Name { get; set; }
}

public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, Response<WalletDto>>
{
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateWalletCommandHandler> _logger;

    public UpdateWalletCommandHandler(IWalletService walletService, IMapper mapper, ILogger<UpdateWalletCommandHandler> logger)
    {
        _walletService = walletService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response<WalletDto>> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var existingWallet = await _walletService.GetByIdAsync(request.Id);
            if (existingWallet == null)
            {
                return Response<WalletDto>.Fail($"Wallet with ID {request.Id} not found.");
            }

            existingWallet.DocumentId = request.DocumentId;
            existingWallet.DocumentType = request.DocumentType;
            existingWallet.Name = request.Name;
            existingWallet.UpdatedAt = DateTime.UtcNow;

            var walletToUpdate = _mapper.Map<Domain.Entities.Wallet>(existingWallet);

            var wallet = await _walletService.UpdateAsync(walletToUpdate);

            var resp = Response<WalletDto>.Ok(_mapper.Map<WalletDto>(wallet), "Wallet updated successfully.");

            _logger.LogInformation($"Wallet with ID {wallet.Id} updated successfully.");

            return resp;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the wallet.");
            return Response<WalletDto>.Fail($"An error occurred while updating the wallet: {ex.Message}", details: [new ErrorDetail("UpdateWalletError", ex.Message)]);
        }
    }
}