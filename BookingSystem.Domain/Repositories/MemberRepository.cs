using BookingSystem.Domain.Data;
using BookingSystem.Infrastructure.IRepositories;
using BookingSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(BookingDbContext context) : base(context)
        {
        }

        // Add any additional member-specific operations here
    }
}
