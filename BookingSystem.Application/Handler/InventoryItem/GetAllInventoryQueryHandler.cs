using BookingSystem.Application.Query;
using BookingSystem.Infrastructure.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Handler.InventoryItem
{
    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, List<BookingSystem.Infrastructure.Models.InventoryItem>>
    {
        private readonly IInventoryItemRepository _inventoryRepository; 

        public GetAllInventoryQueryHandler(IInventoryItemRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<List<BookingSystem.Infrastructure.Models.InventoryItem>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
        {
            var inventoryItems = await _inventoryRepository.GetAllAsync();
            return (List<Infrastructure.Models.InventoryItem>)inventoryItems;
        }
    }

}
