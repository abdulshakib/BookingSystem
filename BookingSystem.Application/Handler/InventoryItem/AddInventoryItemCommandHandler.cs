using BookingSystem.Application.Commands.InventoryItem;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Handler.InventoryItem
{
    public class AddInventoryItemCommandHandler : IRequestHandler<AddInventoryItemCommand, int>
    {
        private readonly InventoryService _inventoryService;

        public AddInventoryItemCommandHandler(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<int> Handle(AddInventoryItemCommand request, CancellationToken cancellationToken)
        {
            var inventoryItem = new BookingSystem.Infrastructure.Models.InventoryItem
            {
                Title = request.Title,
                Description = request.Description,
                RemainingCount = request.RemainingCount,
                ExpirationDate = request.ExpirationDate
            };

            return await _inventoryService.AddInventoryItemAsync(inventoryItem);
        }
    }
}
