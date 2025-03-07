﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Commands.Member
{
    public class UploadMemberCsvCommand : IRequest<Unit>
    {
        public IFormFile File { get; set; }
    }
}
