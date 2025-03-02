using BookingSystem.Application.Commands.Member;
using BookingSystem.Application.Interfaces;
using BookingSystem.Common;
using BookingSystem.Common.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Handler.Member
{
    public class UploadMemberCsvCommandHandler : IRequestHandler<UploadMemberCsvCommand, Unit>
    {
        private readonly IMemberService _memberService;

        public UploadMemberCsvCommandHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<Unit> Handle(UploadMemberCsvCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            // Parse the CSV file and store the member data
            using (var stream = new StreamReader(request.File.OpenReadStream()))
            {
                var csvContent = await stream.ReadToEndAsync();

                var members = CsvParser.ParseCsv(csvContent, columns => new MemberCsvModel
                {
                    Name = columns[0],
                    Surname = columns[1],
                    BookingCount = int.Parse(columns[2]),
                    DateJoined = DateTime.Parse(columns[3], CultureInfo.InvariantCulture)
                });

                foreach (var member in members)
                {
                    var memberItem = new BookingSystem.Infrastructure.Models.Member
                    {
                        Name = member.Name,
                        Surname = member.Surname,
                        BookingCount = member.BookingCount,
                        DateJoined = member.DateJoined
                    };

                    await _memberService.AddMemberAsync(memberItem);
                }
            }

            return Unit.Value;
        }
    }

}
