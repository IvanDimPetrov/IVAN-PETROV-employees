using EmployeeReports.Server.Models;
using EmployeeReports.Server.Services.Contracts;
using EmployeeReports.Server.Utils;

namespace EmployeeReports.Server.Services
{
    public class CsvFileParser : IFileParser
    {
        public async Task<IList<EmployeeProjectRecord>> ParseAsync(Stream stream)
        {
            var records = new List<EmployeeProjectRecord>();

            using (stream)
            {
                using var reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var parts = line.Split(',', StringSplitOptions.TrimEntries);

                    if (!int.TryParse(parts[0], out var empId) || !int.TryParse(parts[1], out var projectId))
                    {
                        continue;
                    }

                    if (!DateParser.TryParse(parts[2], out var dateFrom))
                    {
                        continue;
                    }

                    DateTime dateTo = DateTime.Now;
                    if (!string.Equals(parts[3], "NULL", StringComparison.OrdinalIgnoreCase) &&
                        !DateParser.TryParse(parts[3], out dateTo))
                    {
                        continue;
                    }

                    records.Add(new EmployeeProjectRecord
                    {
                        EmpID = empId,
                        ProjectID = projectId,
                        DateFrom = dateFrom,
                        DateTo = dateTo
                    });
                }
            }

            return records;
        }
    }
}
