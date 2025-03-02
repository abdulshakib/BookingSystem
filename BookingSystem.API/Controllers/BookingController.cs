using BookingSystem.Application.Commands.Booking;
using BookingSystem.Application.Query;
using BookingSystem.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookItem([FromQuery] int memberId, [FromQuery] int inventoryItemId)
        {
            var result = await _mediator.Send(new BookItemCommand { MemberId = memberId, InventoryItemId = inventoryItemId });

            switch (result)
            {
                case BookItemResult.Success:
                    return Ok("Booking successful.");
                case BookItemResult.MaxBookingsReached:
                    return BadRequest("You have reached the maximum number of bookings.");
                case BookItemResult.InventoryItemNotAvailable:
                    return BadRequest("The selected inventory item is no longer available.");
                default:
                    return StatusCode(500, "An unexpected error occurred while booking the item.");
            }
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookingsByMember([FromQuery] int memberId)
        {
            var bookings = await _mediator.Send(new GetBookingsByMemberQuery { MemberId = memberId });
            return Ok(bookings);
        }

        // Endpoint to cancel a booking based on the booking ID
        [HttpDelete("cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            // Send the command to MediatR to handle it
            await _mediator.Send(new CancelBookingCommand { BookingId = bookingId });

            return Ok("Booking canceled successfully.");
        }
    }
}
