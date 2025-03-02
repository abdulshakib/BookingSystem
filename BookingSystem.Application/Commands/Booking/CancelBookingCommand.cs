using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Commands.Booking
{
    public class CancelBookingCommand : IRequest<Unit>
    {
        public int BookingId { get; set; }
    }
}
