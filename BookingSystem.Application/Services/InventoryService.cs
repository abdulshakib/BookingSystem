using BookingSystem.Application.Interfaces;
using BookingSystem.Infrastructure.IRepositories;
using BookingSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IGenericRepository<InventoryItem> _inventoryRepository;

        public InventoryService(IGenericRepository<InventoryItem> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<int> AddInventoryItemAsync(InventoryItem inventoryItem)
        {
            return await _inventoryRepository.AddAsync(inventoryItem);
        }
    }
}
