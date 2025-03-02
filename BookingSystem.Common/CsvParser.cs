using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Common
{
    public static class CsvParser
    {
        public static IEnumerable<T> ParseCsv<T>(string csvContent, Func<string[], T> mapRow)
        {
            var items = new List<T>();

            // Use TextFieldParser to correctly parse CSV content with quoted fields
            using (var parser = new TextFieldParser(new System.IO.StringReader(csvContent)))
            {
                parser.HasFieldsEnclosedInQuotes = true;  // Handle fields enclosed in quotes
                parser.SetDelimiters(",");  // Handle comma as the delimiter

                // Skip header row
                if (!parser.EndOfData)
                    parser.ReadLine();

                // Parse each line
                while (!parser.EndOfData)
                {
                    var columns = parser.ReadFields();

                    // Use the provided mapping function to convert each row to an object of type T
                    var item = mapRow(columns);
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
