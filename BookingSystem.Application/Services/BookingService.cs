using BookingSystem.Application.Interfaces;
using BookingSystem.Common;
using BookingSystem.Infrastructure.IRepositories;
using BookingSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<InventoryItem> _inventoryItemRepository;
        private readonly IGenericRepository<Booking> _bookingRepository;

        public BookingService(
            IGenericRepository<Member> memberRepository,
            IGenericRepository<InventoryItem> inventoryItemRepository,
            IGenericRepository<Booking> bookingRepository)
        {
            _memberRepository = memberRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<BookItemResult> BookItemAsync(int memberId, int inventoryItemId)
        {
            // Check member existence
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null || member.BookingCount >= 2)
            {
                return BookItemResult.MaxBookingsReached;
            }

            // Check inventory item availability
            var inventoryItem = await _inventoryItemRepository.GetByIdAsync(inventoryItemId);
            if (inventoryItem == null || inventoryItem.RemainingCount <= 0)
            {
                return BookItemResult.InventoryItemNotAvailable;
            }

            // Create and add the booking
            var booking = new Booking
            {
                MemberId = memberId,
                InventoryItemId = inventoryItemId,
                BookingDate = DateTime.UtcNow
            };
            await _bookingRepository.AddAsync(booking);

            // Update the member's booking count
            member.BookingCount++;
            await _memberRepository.UpdateAsync(member);

            // Decrease the inventory item remaining count
            inventoryItem.RemainingCount--;
            await _inventoryItemRepository.UpdateAsync(inventoryItem);

            return BookItemResult.Success;
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            // Retrieve the booking from the repository
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking != null)
            {
                // Retrieve associated member and inventory item
                var member = await _memberRepository.GetByIdAsync(booking.MemberId);
                var inventoryItem = await _inventoryItemRepository.GetByIdAsync(booking.InventoryItemId);

                if (inventoryItem != null)
                {
                    // Increase the inventory item's remaining count
                    inventoryItem.RemainingCount++;
                    await _inventoryItemRepository.UpdateAsync(inventoryItem);
                }

                if (member != null)
                {
                    // Decrease the member's booking count
                    member.BookingCount--;
                    await _memberRepository.UpdateAsync(member);
                }

                // Delete the booking from the repository
                await _bookingRepository.DeleteAsync(bookingId);
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsByMemberIdAsync(int memberId)
        {
            // Get all bookings for the specified member
            var bookings = await _bookingRepository.GetAllAsync();
            return bookings.Where(b => b.MemberId == memberId);
        }

        public async Task<bool> IsBookingPossibleAsync(int memberId, int inventoryItemId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null || member.BookingCount >= 2)
            {
                return false; // Member has reached the max bookings
            }

            var inventoryItem = await _inventoryItemRepository.GetByIdAsync(inventoryItemId);
            if (inventoryItem == null || inventoryItem.RemainingCount <= 0)
            {
                return false; // Inventory item is not available
            }

            return true;
        }
    }
}
