﻿using BookingSystem.Application.Commands.InventoryItem;
using BookingSystem.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            var query = new GetAllInventoryQuery();
            var result = await _mediator.Send(query);

            if (result == null || result.Count == 0)
            {
                return NotFound("No inventory items found.");
            }

            return Ok(result);  // Return the list of inventory items
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadInventoryCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var command = new UploadInventoryCsvCommand
            {
                File = file
            };

            await _mediator.Send(command);

            return Ok("Inventory file uploaded and processed successfully.");
        }
    }
}
