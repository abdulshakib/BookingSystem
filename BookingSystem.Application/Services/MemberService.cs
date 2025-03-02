using BookingSystem.Application.Interfaces;
using BookingSystem.Infrastructure.IRepositories;
using BookingSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;

        public MemberService(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<int> AddMemberAsync(Member member)
        {
            await _memberRepository.AddAsync(member);
            return member.Id;
        }
    }
}
