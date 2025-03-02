using BookingSystem.Application.Commands.Booking;
using BookingSystem.Application.Interfaces;
using BookingSystem.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Handler.Booking
{
    public class BookItemCommandHandler : IRequestHandler<BookItemCommand, BookItemResult>
    {
        private readonly IBookingService _bookingService;

        public BookItemCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookItemResult> Handle(BookItemCommand request, CancellationToken cancellationToken)
        {
            return await _bookingService.BookItemAsync(request.MemberId, request.InventoryItemId);
        }
    }
}
