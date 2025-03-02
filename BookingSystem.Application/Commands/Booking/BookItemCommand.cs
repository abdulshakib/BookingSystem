using BookingSystem.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Commands.Booking
{
    public class BookItemCommand : IRequest<BookItemResult>
    {
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
    }
}
