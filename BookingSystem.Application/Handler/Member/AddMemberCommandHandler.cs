using BookingSystem.Application.Commands.Member;
using BookingSystem.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Handler.Member
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, int>
    {
        private readonly IMemberService _memberService;

        public AddMemberCommandHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<int> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var member = new BookingSystem.Infrastructure.Models.Member
            {
                Name = request.Name,
                Surname = request.Surname,
                BookingCount = request.BookingCount,
                DateJoined = request.DateJoined
            };

            return await _memberService.AddMemberAsync(member);
        }
    }
}
