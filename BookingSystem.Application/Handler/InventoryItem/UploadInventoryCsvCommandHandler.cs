using BookingSystem.Application.Commands.InventoryItem;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Services;
using BookingSystem.Common;
using BookingSystem.Common.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Handler.InventoryItem
{
    public class UploadInventoryCsvCommandHandler : IRequestHandler<UploadInventoryCsvCommand,Unit>
    {
        private readonly IInventoryService _inventoryService;

        public UploadInventoryCsvCommandHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<Unit> Handle(UploadInventoryCsvCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                throw new ArgumentException("No file uploaded.");

            // Parse the CSV file
            using (var stream = new StreamReader(request.File.OpenReadStream()))
            {
                var csvContent = await stream.ReadToEndAsync();

                var inventoryItems = CsvParser.ParseCsv(csvContent, columns => new InventoryItemCsvModel
                {
                    Title = columns[0],
                    Description = columns[1],
                    RemainingCount = int.Parse(columns[2]),
                    ExpirationDate = DateTime.Parse(columns[3], CultureInfo.InvariantCulture)
                });

                foreach (var item in inventoryItems)
                {
                    var inventoryItem = new BookingSystem.Infrastructure.Models.InventoryItem
                    {
                        Title = item.Title,
                        Description = item.Description,
                        RemainingCount = item.RemainingCount,
                        ExpirationDate = item.ExpirationDate
                    };

                    // Add inventory item using the service
                    var result = await _inventoryService.AddInventoryItemAsync(inventoryItem);
                    if (result > 0)
                    {
                        // Optionally, log success or handle the result as needed
                        Console.WriteLine("Inventory item added successfully.");
                    }
                }
            }

            return Unit.Value;  // Indicating success, as no response is required
        }
    }


}
