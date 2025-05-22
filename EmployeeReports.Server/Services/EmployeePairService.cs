using EmployeeReports.Server.DTOs;
using EmployeeReports.Server.Models;
using EmployeeReports.Server.Services.Contracts;

namespace EmployeeReports.Server.Services
{
    public class EmployeePairService : IEmployeePairService
    {
        public IList<EmployeePairResult> GetAllPairs(IList<EmployeeProjectRecord> records)
        {
            var result = new List<EmployeePairResult>();

            var groupedByProject = records.GroupBy(r => r.ProjectID);

            foreach (var projectGroup in groupedByProject)
            {
                var projectRecords = projectGroup.ToList();
                for (int i = 0; i < projectRecords.Count; i++)
                {
                    for (int j = i + 1; j < projectRecords.Count; j++)
                    {
                        var record1 = projectRecords[i];
                        var record2 = projectRecords[j];

                        DateTime overlapStart;
                        DateTime overlapEnd;

                        if (record1.DateFrom > record2.DateFrom)
                        {
                            overlapStart = record1.DateFrom;
                        }
                        else
                        {
                            overlapStart = record2.DateFrom;
                        }

                        if (record1.DateTo < record2.DateTo)
                        {
                            overlapEnd = record1.DateTo;
                        }
                        else
                        {
                            overlapEnd = record2.DateTo;
                        }

                        if (overlapEnd >= overlapStart)
                        {
                            int overlapDays = (overlapEnd - overlapStart).Days + 1;

                            result.Add(new EmployeePairResult
                            {
                                EmpID1 = record1.EmpID,
                                EmpID2 = record2.EmpID,
                                ProjectID = record1.ProjectID,
                                DaysWorkedTogether = overlapDays
                            });
                        }
                    }
                }
            }

            return result
                    .OrderByDescending(x => x.DaysWorkedTogether)
                    .ToList();           
        }

        public IList<EmployeePairResult> GetTopWorkingDaysPairGroup(IList<EmployeeProjectRecord> records)
        {
            var allPairs = GetAllPairs(records);

            var topPairGroup = allPairs
                .GroupBy(r => new
                {
                    Emp1 = Math.Min(r.EmpID1, r.EmpID2),
                    Emp2 = Math.Max(r.EmpID1, r.EmpID2)
                })
                .OrderByDescending(x => x.Sum(d => d.DaysWorkedTogether))
                .FirstOrDefault()?
                .ToList()
                .OrderByDescending(x => x.DaysWorkedTogether)
                .ToList();

            return topPairGroup ?? new();
        }
    }
}
