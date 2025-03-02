using BookingSystem.Application.Commands.Member;
using BookingSystem.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var query = new GetAllMembersQuery();
            var result = await _mediator.Send(query);

            if (result == null || result.Count == 0)
            {
                return NotFound("No members found.");
            }

            return Ok(result);  // Return the list of members
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadMemberCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var command = new UploadMemberCsvCommand
            {
                File = file
            };

            await _mediator.Send(command);

            return Ok("Members uploaded and processed successfully.");
        }
    }

}
