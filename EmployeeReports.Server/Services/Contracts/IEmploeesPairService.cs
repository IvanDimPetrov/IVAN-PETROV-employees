using EmployeeReports.Server.DTOs;
using EmployeeReports.Server.Models;

namespace EmployeeReports.Server.Services.Contracts
{
    public interface IEmployeePairService
    {
        IList<EmployeePairResult> GetAllPairs(IList<EmployeeProjectRecord> records);

        IList<EmployeePairResult> GetTopWorkingDaysPairGroup(IList<EmployeeProjectRecord> records);
    }
}
