using BookingSystem.Application.Interfaces;
using BookingSystem.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Query
{
    public class GetBookingsByMemberQueryHandler : IRequestHandler<GetBookingsByMemberQuery, IEnumerable<Booking>>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsByMemberQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IEnumerable<Booking>> Handle(GetBookingsByMemberQuery request, CancellationToken cancellationToken)
        {
            return await _bookingService.GetBookingsByMemberIdAsync(request.MemberId);
        }
    }
}
