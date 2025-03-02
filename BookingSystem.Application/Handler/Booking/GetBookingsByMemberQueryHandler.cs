using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Query;
using MediatR;

namespace BookingSystem.Application.Handler.Booking
{
    public class GetBookingsByMemberQueryHandler : IRequestHandler<GetBookingsByMemberQuery, List<BookingSystem.Infrastructure.Models.Booking>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByMemberQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<BookingSystem.Infrastructure.Models.Booking>> Handle(GetBookingsByMemberQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingService.GetBookingsByMemberIdAsync(request.MemberId);
            return bookings.ToList();
        }
    }
}
