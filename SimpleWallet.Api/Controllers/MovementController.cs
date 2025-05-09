using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService _movementService;
        private readonly IMapper _mapper;

        public MovementController(IMovementService movementService, IMapper mapper)
        {
            _movementService = movementService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movement = await _movementService.GetByIdAsync(id);
            if (movement == null)
            {
                return NotFound($"Movement with ID {id} not found.");
            }
            return Ok(movement);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movements = await _movementService.GetAllAsync();
            return Ok(movements);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovementDto movementDto)
        {
            if (movementDto == null)
            {
                return BadRequest("Movement data is required.");
            }

            var newMovement = _mapper.Map<Movement>(movementDto);

            var createdMovement = await _movementService.CreateAsync(newMovement);
            return CreatedAtAction(nameof(GetById), new { id = createdMovement.Id }, createdMovement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MovementDto movementDto)
        {
            if (movementDto == null || id != movementDto.Id)
            {
                return BadRequest("Invalid movement data.");
            }

            var movementToUpdate = _mapper.Map<Movement>(movementDto);
            var updatedMovement = await _movementService.UpdateAsync(movementToUpdate);
            if (updatedMovement == null)
            {
                return NotFound($"Movement with ID {id} not found.");
            }
            return Ok(updatedMovement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _movementService.GetByIdAsync(id);
            if (exists == null)
            {
                return NotFound($"Movement with ID {id} not found.");
            }

            await _movementService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("wallet/{walletId}")]
        public async Task<IActionResult> GetByWalletId(int walletId)
        {
            var movements = await _movementService.GetByWalletIdAsync(walletId);
            if (movements == null || !movements.Any())
            {
                return NotFound($"No movements found for Wallet ID {walletId}.");
            }
            return Ok(movements);
        }

        [HttpGet("wallet/{walletId}/dateRange")]
        public async Task<IActionResult> GetByWalletIdAndDateRange(int walletId, DateTime startDate, DateTime endDate)
        {
            var movements = await _movementService.GetByWalletIdAndDateRangeAsync(walletId, startDate, endDate);
            if (movements == null || !movements.Any())
            {
                return NotFound($"No movements found for Wallet ID {walletId} in the specified date range.");
            }
            return Ok(movements);
        }
    }
}
