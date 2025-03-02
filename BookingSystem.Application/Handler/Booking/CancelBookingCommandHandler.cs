using BookingSystem.Application.Commands.Booking;
using BookingSystem.Application.Interfaces;
using MediatR;

namespace BookingSystem.Application.Handler.Booking
{
    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand,Unit>
    {
        private readonly IBookingService _bookingService;

        public CancelBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<Unit> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            await _bookingService.CancelBookingAsync(request.BookingId);
            return Unit.Value;
        }
    }
}
