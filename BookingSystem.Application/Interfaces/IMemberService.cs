using BookingSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Interfaces
{
    public interface IMemberService
    {
        Task<int> AddMemberAsync(Member member);
    }
}
