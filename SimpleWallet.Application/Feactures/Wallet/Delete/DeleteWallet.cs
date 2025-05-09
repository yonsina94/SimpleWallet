using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Application.Response;

namespace SimpleWallet.Application.Features.Wallet.Delete;

public class DeleteWalletCommand : IRequest<Response<WalletDto>>
{
    [JsonPropertyName("id")]
    [Required(ErrorMessage = "ID is required")]
    public int Id { get; set; }
}

public class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand, Response<WalletDto>>
{
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteWalletCommandHandler> _logger;

    public DeleteWalletCommandHandler(IWalletService walletService, IMapper mapper, ILogger<DeleteWalletCommandHandler> logger)
    {
        _walletService = walletService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response<WalletDto>> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingWallet = await _walletService.GetByIdAsync(request.Id);
            if (existingWallet == null)
            {
                return Response<WalletDto>.Fail($"Wallet with ID {request.Id} not found.");
            }

            await _walletService.DeleteAsync(existingWallet.Id);

            var resp = Response<WalletDto>.Ok(_mapper.Map<WalletDto>(existingWallet), "Wallet deleted successfully.");

            _logger.LogInformation($"Wallet with ID {existingWallet.Id} deleted successfully.");

            return resp;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting wallet");
            return Response<WalletDto>.Fail("An error occurred while deleting the wallet.");
        }
    }
}