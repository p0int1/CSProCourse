using Logistic.Core.Services;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : Controller
    {
        private readonly IEntityService<Warehouse> _warehouseService;

        public WarehouseController(IEntityService<Warehouse> service) =>
            (_warehouseService) = (service);

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create new warehouse")]
        public IActionResult Create([FromBody] Warehouse warehouse)
        {
            _warehouseService.Create(warehouse);
            return Ok("warehouse created successfully");
        }

        [HttpGet("get/{entityId}")]
        [SwaggerOperation(Summary = "Get warehouse by id")]
        public IActionResult GetById([FromRoute] int entityId)
        {
            var warehouse = _warehouseService.GetById(entityId);
            return warehouse != null ? Ok(warehouse) 
                : BadRequest("there is no warehouse with this Id");
        }

        [HttpGet("getAll")]
        [SwaggerOperation(Summary = "Get all warehouses")]
        public List<Warehouse> GetAll() => _warehouseService.GetAll();

        [HttpDelete("delete/{entityId}")]
        [SwaggerOperation(Summary = "Delete warehouse by id")]
        public IActionResult DeleteById([FromRoute] int entityId) =>
            _warehouseService.DeleteById(entityId) ? Ok("successfully") 
            : BadRequest("there is no warehouse with this Id");

        [HttpDelete("deleteAll")]
        [SwaggerOperation(Summary = "Delete all warehouses")]
        public void DeleteAll() => _warehouseService.DeleteAll();

        [HttpPut("load/{entityId}")]
        [SwaggerOperation(Summary = "Load cargo in warehouse")]
        public IActionResult LoadCargo([FromRoute] int entityId, [FromBody] Cargo cargo)
        {
            var warehouse = _warehouseService.GetById(entityId);
            if (warehouse == null) return BadRequest("there is no warehouse with this Id");
            warehouse.Cargos.Add(cargo);
            _warehouseService.LoadCargo(warehouse, entityId);
            return Ok("successfully");
        }

        [HttpPut("unload/{entityId}/{cargoId}")]
        [SwaggerOperation(Summary = "Unload cargo from warehouse")]
        public IActionResult UnloadCargo([FromRoute] int entityId, [FromRoute] Guid cargoId)
        {
            var warehouse = _warehouseService.GetById(entityId);
            if (warehouse == null) return BadRequest("there is no warehouse with this Id");
            var cargo = warehouse.Cargos.FirstOrDefault(x => x.Id == cargoId);
            if (cargo == null) return BadRequest("there is no cargo with this Id");
            warehouse.Cargos.Remove(cargo);
            _warehouseService.UnloadCargo(warehouse, entityId);
            return Ok("successfully");
        }

        [HttpPut("uloadAll/{entityId}")]
        [SwaggerOperation(Summary = "Unload all cargos")]
        public IActionResult UnloadAllCargos([FromRoute] int entityId)
        {
            var warehouse = _warehouseService.GetById(entityId);
            if (warehouse == null) return BadRequest("there is no warehouse with this Id");
            warehouse.Cargos.Clear();
            _warehouseService.UnloadAllCargos(warehouse, entityId);
            return Ok("all cargos unloaded");
        }
    }
}