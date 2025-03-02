using BookingSystem.Application.Handler.InventoryItem;
using BookingSystem.Application.Interfaces;
using BookingSystem.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Commands.InventoryItem
{
    public class UploadInventoryCsvCommand : IRequest<Unit>
    {
        public IFormFile File { get; set; }

    }
  
}
