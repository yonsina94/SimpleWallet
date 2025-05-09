using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWallet.Application.DTOs;
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

        public WalletController(IWalletService walletService, IMapper mapper)
        {
            _mapper = mapper;
            _walletService = walletService;
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
        public async Task<IActionResult> Create([FromBody] WalletCreateDto walletDto)
        {
            if (walletDto == null)
            {
                return BadRequest("Wallet data is required.");
            }

            var newWallet = _mapper.Map<Wallet>(walletDto);

            var createdWallet = await _walletService.CreateAsync(newWallet);
            return CreatedAtAction(nameof(GetById), new { id = createdWallet.Id }, createdWallet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WalletUpdateDto walletDto)
        {
            if (walletDto == null || id != null)
            {
                return BadRequest("Invalid wallet data.");
            }

            var existingWallet = await _walletService.GetByIdAsync(id);
            if (existingWallet == null)
            {
                return NotFound($"Wallet with ID {id} not found.");
            }

            var updatedWallet = _mapper.Map<Wallet>(walletDto, opc => opc.Items["Id"] = id);
            updatedWallet.Id = id; // Ensure the ID is set correctly
            await _walletService.UpdateAsync(updatedWallet);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingWallet = await _walletService.GetByIdAsync(id);
            if (existingWallet == null)
            {
                return NotFound($"Wallet with ID {id} not found.");
            }

            await _walletService.DeleteAsync(existingWallet.Id);
            return NoContent();
        }
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transferDto)
        {
            if (transferDto == null)
            {
                return BadRequest("Transfer data is required.");
            }

            var success = await _walletService.TransferFundsAsync(transferDto.FromWalletId, transferDto.ToWalletId, transferDto.Amount);
            if (!success)
            {
                return BadRequest("Transfer failed.");
            }

            return NoContent();
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
