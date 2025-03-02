using BookingSystem.Application.Query;
using BookingSystem.Infrastructure.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Handler.Member
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, List<BookingSystem.Infrastructure.Models.Member>>
    {
        private readonly IGenericRepository<BookingSystem.Infrastructure.Models.Member> _memberRepository;

        public GetAllMembersQueryHandler(IGenericRepository<BookingSystem.Infrastructure.Models.Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<List<BookingSystem.Infrastructure.Models.Member>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            return (List<Infrastructure.Models.Member>)await _memberRepository.GetAllAsync();
        }
    }
}
