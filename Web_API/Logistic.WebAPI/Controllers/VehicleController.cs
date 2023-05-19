using Logistic.Core.Services;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IEntityService<Vehicle> _vehicleService;

        public VehicleController(IEntityService<Vehicle> service) =>
            (_vehicleService) = (service);

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create new vehicle")]
        public IActionResult Create([FromBody] Vehicle vehicle)
        {
            _vehicleService.Create(vehicle);
            return Ok("vehicle created successfully");
        }

        [HttpGet("get/{entityId}")]
        [SwaggerOperation(Summary = "Get vehicle by id")]
        public IActionResult GetById([FromRoute] int entityId)
        {
            var vehicle = _vehicleService.GetById(entityId);
            return vehicle != null ? Ok(vehicle) 
                : BadRequest("there is no vehicle with this Id");
        }

        [HttpGet("getAll")]
        [SwaggerOperation(Summary = "Get all vehicles")]
        public List<Vehicle> GetAll() => _vehicleService.GetAll();

        [HttpDelete("delete/{entityId}")]
        [SwaggerOperation(Summary = "Delete vehicle by id")]
        public IActionResult DeleteById([FromRoute] int entityId) => 
            _vehicleService.DeleteById(entityId) ? Ok("successfully") 
            : BadRequest("there is no vehicle with this Id");

        [HttpDelete("deleteAll")]
        [SwaggerOperation(Summary = "Delete all vehicles")]
        public void DeleteAll() => _vehicleService.DeleteAll();

        [HttpPut("load/{entityId}")]
        [SwaggerOperation(Summary = "Load cargo in vehicle")]
        public IActionResult LoadCargo([FromRoute] int entityId, [FromBody] Cargo cargo)
        {
            var vehicle = _vehicleService.GetById(entityId);
            if (vehicle == null) return BadRequest("there is no vehicle with this Id");
            vehicle.Cargos.Add(cargo);
            var isLoaded = _vehicleService.LoadCargo(vehicle, entityId);
            return isLoaded ? Ok("successfully") 
                : BadRequest("free weight or volume is not enough!");
        }

        [HttpPut("unload/{entityId}/{cargoId}")]
        [SwaggerOperation(Summary = "Unload cargo from vehicle")]
        public IActionResult UnloadCargo([FromRoute] int entityId, [FromRoute] Guid cargoId)
        {
            var vehicle = _vehicleService.GetById(entityId);
            if (vehicle == null) return BadRequest("there is no vehicle with this Id");
            var cargo = vehicle.Cargos.FirstOrDefault(x => x.Id == cargoId);
            if (cargo == null) return BadRequest("there is no cargo with this Id");
            vehicle.Cargos.Remove(cargo);
            _vehicleService.UnloadCargo(vehicle, entityId);
            return Ok("successfully");
        }

        [HttpPut("uloadAll/{entityId}")]
        [SwaggerOperation(Summary = "Unload all cargos")]
        public IActionResult UnloadAllCargos([FromRoute] int entityId)
        {
            var vehicle = _vehicleService.GetById(entityId);
            if (vehicle == null) return BadRequest("there is no vehicle with this Id");
            vehicle.Cargos.Clear();
            _vehicleService.UnloadAllCargos(vehicle, entityId);
            return Ok("all cargos unloaded");
        }
    }
}