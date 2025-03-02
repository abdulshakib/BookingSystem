using BookingSystem.Common;
using BookingSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookItemResult> BookItemAsync(int memberId, int inventoryItemId);

        Task CancelBookingAsync(int bookingId);

        Task<IEnumerable<Booking>> GetBookingsByMemberIdAsync(int memberId);

        Task<bool> IsBookingPossibleAsync(int memberId, int inventoryItemId);
    }
}
