using EmployeeReports.Server.Models;

namespace EmployeeReports.Server.Services.Contracts
{
    public interface IFileParser
    {
        Task<IList<EmployeeProjectRecord>> ParseAsync(Stream stream);
    }
}
