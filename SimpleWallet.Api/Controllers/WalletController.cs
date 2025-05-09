using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Features.Wallet.Create;
using SimpleWallet.Application.Features.Wallet.Delete;
using SimpleWallet.Application.Features.Wallet.Transfer;
using SimpleWallet.Application.Features.Wallet.Update;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;

namespace SimpleWallet.Api.Controllers
{
    public record TransferDto(int FromWalletId, int ToWalletId, decimal Amount);


    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        protected readonly IWalletService _walletService;
        protected readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public WalletController(IWalletService walletService, IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _walletService = walletService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var wallet = await _walletService.GetByIdAsync(id);
            if (wallet == null)
            {
                return NotFound($"Wallet with ID {id} not found.");
            }
            return Ok(wallet);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var wallets = await _walletService.GetAllAsync();
            return Ok(wallets);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalletCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWalletCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new { id });
            return NoContent();
        }
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferWalletCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpGet("by-document-type/{documentType}")]
        public async Task<IActionResult> GetByDocumentType(string documentType)
        {
            if (string.IsNullOrEmpty(documentType))
            {
                return BadRequest("Document type is required.");
            }

            if (!Enum.TryParse<DocumentType>(documentType, true, out var documentTypeEnum))
            {
                return BadRequest("Invalid document type.");
            }

            var wallets = await _walletService.GetByDocumentTypeAsync(documentTypeEnum);
            return Ok(wallets);
        }

        [HttpGet("by-document-id/{documentId}")]
        public async Task<IActionResult> GetByDocumentId(string documentId)
        {
            if (string.IsNullOrEmpty(documentId))
            {
                return BadRequest("Document ID is required.");
            }

            var wallet = await _walletService.GetByDocumentIdAsync(documentId);
            if (wallet == null)
            {
                return NotFound($"Wallet with Document ID {documentId} not found.");
            }

            return Ok(wallet);
        }

    }
}
