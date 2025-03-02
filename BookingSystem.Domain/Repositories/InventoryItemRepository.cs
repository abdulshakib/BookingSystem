using BookingSystem.Domain.Data;
using BookingSystem.Infrastructure.IRepositories;
using BookingSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Repositories
{
    public class InventoryItemRepository : GenericRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(BookingDbContext context) : base(context)
        {
        }

    }
}
