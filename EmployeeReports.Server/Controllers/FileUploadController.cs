using EmployeeReports.Server.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeReports.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileParser _fileParser;
        private readonly IEmployeePairService _employeePairService;

        public FileUploadController(
            IFileParser fileParser,
            IEmployeePairService employeePairService) 
        { 
            _fileParser = fileParser;
            _employeePairService = employeePairService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var records = await _fileParser.ParseAsync(file.OpenReadStream());

            var topPairGroup = _employeePairService.GetTopWorkingDaysPairGroup(records);

            return Ok(topPairGroup);
        }
    }
}
