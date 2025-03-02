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
            var rows = csvContent.Split('\n');

            foreach (var row in rows.Skip(1))  // Skipping header row
            {
                if (string.IsNullOrWhiteSpace(row))
                    continue;

                var columns = row.Split(',');

                // Using the provided mapping function to convert each row to an object of type T
                var item = mapRow(columns);
                items.Add(item);
            }

            return items;
        }
    }
}
