using AutoMapper;
using MediatR;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Application.Features.Wallet;

public class CreateWallet
{
    public class Command : IRequest<WalletDto>
    {
        public string DocumentId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    public class Handler : IRequestHandler<Command, WalletDto>
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public Handler(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService;
            _mapper = mapper;
        }

        public async Task<WalletDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var newWallet = new Domain.Entities.Wallet(request.DocumentId, request.DocumentType, request.Name, request.Balance);

            var createdWallet = await _walletService.CreateAsync(newWallet);
            return _mapper.Map<WalletDto>(createdWallet);
        }
    }
}