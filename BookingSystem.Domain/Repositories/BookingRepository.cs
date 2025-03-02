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
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly BookingDbContext _dbContext;

        public BookingRepository(BookingDbContext context) : base(context)
        {
        }

        public async Task<bool> BookItemAsync(int memberId, int inventoryItemId)
        {
            var member = await _dbContext.Members.FindAsync(memberId);
            var inventoryItem = await _dbContext.Inventory.FindAsync(inventoryItemId);

            if (member == null || inventoryItem == null || inventoryItem.RemainingCount <= 0 || member.BookingCount >= 2)
                return false;

            var booking = new Booking
            {
                MemberId = memberId,
                InventoryItemId = inventoryItemId,
                BookingDate = DateTime.UtcNow
            };

            _dbContext.Bookings.Add(booking);
            inventoryItem.RemainingCount--;
            member.BookingCount++;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            var booking = await _dbContext.Bookings.FindAsync(bookingId);
            if (booking == null)
                return false;

            var member = await _dbContext.Members.FindAsync(booking.MemberId);
            var inventoryItem = await _dbContext.Inventory.FindAsync(booking.InventoryItemId);

            _dbContext.Bookings.Remove(booking);
            member.BookingCount--;
            inventoryItem.RemainingCount++;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
