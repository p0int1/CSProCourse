using Logistic.Core.Services;
using Logistic.Enums;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService<Vehicle> _vehicleReportService;
        private readonly IReportService<Warehouse> _warehouseReportService;
        private readonly IEntityService<Vehicle> _vehicleService;
        private readonly IEntityService<Warehouse> _warehouseService;
        private readonly string _reportDir;

        public ReportController(IReportService<Vehicle> vehicleReportService, IReportService<Warehouse> warehouseReportService, 
            IEntityService<Vehicle> vehicleService, IEntityService<Warehouse> warehouseService)
        {
            (_vehicleReportService, _warehouseReportService, _vehicleService, _warehouseService) = 
                (vehicleReportService, warehouseReportService, vehicleService, warehouseService);
            var appDir = Directory.GetCurrentDirectory();
            _reportDir = Path.Combine(appDir, "reports");
            Directory.CreateDirectory(_reportDir);
        }

        [HttpPost("createReport/{reportType}")]
        [SwaggerOperation(Summary = "Create vehicle & warehouse report")]
        public IActionResult CreateReport([FromRoute] ReportType reportType)
        {
            var vehicleFilePath = _vehicleReportService.CreateReport(_vehicleService.GetAll(), reportType, _reportDir);
            var warehouseFilePath = _warehouseReportService.CreateReport(_warehouseService.GetAll(), reportType, _reportDir);
            return Ok($"Created: {Path.GetFileName(vehicleFilePath)}, {Path.GetFileName(warehouseFilePath)}");
        }

        [HttpPut("loadReport/{fileName}")]
        [SwaggerOperation(Summary = "Load report in memoryRepository")]
        public IActionResult LoadReport([FromRoute] string fileName)
        {
            var filePath = Path.Combine(_reportDir, fileName.Trim());
            if (!System.IO.File.Exists(filePath)) return BadRequest($"file {filePath} does not exist!");
            if (filePath.Contains("Vehicle"))
            {
                _vehicleService.DeleteAll();
                _vehicleReportService.LoadReport(filePath).ForEach(x => _vehicleService.Create(x));
            }
            else
            {
                _warehouseService.DeleteAll();
                _warehouseReportService.LoadReport(filePath).ForEach(x => _warehouseService.Create(x));
            }
            return Ok($"data successfully received from {fileName}");
        }

        [HttpGet("getListFiles")]
        [SwaggerOperation(Summary = "Get list files in reportDir")]
        public IActionResult GetListFiles() => Ok($"Files in directory: " + 
            $"{string.Join(", ", Directory.GetFiles(_reportDir).Select(filePath => Path.GetFileName(filePath)).ToList())}");
    }
}
