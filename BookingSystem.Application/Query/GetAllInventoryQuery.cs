using BookingSystem.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Query
{
    public class GetAllInventoryQuery : IRequest<List<InventoryItem>>
    {
       
    }
}
