using BookingSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.IRepositories
{
    public interface IBookingRepository: IGenericRepository<Booking>
    {
        Task<bool> BookItemAsync(int memberId, int inventoryItemId);
        Task<bool> CancelBookingAsync(int bookingId);
    }
}
