using Logistic.Models;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services
{
    public class WarehouseService
    {
        public IRepository<Warehouse> memoryRepositoryWarehouse;

        public WarehouseService(IRepository<Warehouse> memeoryRepository)
        {
            memoryRepositoryWarehouse = memeoryRepository;
        }

        public void Create(Warehouse warehouse) => memoryRepositoryWarehouse.Create(warehouse);

        public Warehouse GetById(int warehouseId) => memoryRepositoryWarehouse.Read(warehouseId);

        public List<Warehouse> GetAll() => memoryRepositoryWarehouse.ReadAll();

        public bool DeleteById(int warehouseId) => memoryRepositoryWarehouse.Delete(warehouseId);

        public void DeleteAll() => memoryRepositoryWarehouse.DeleteAll();

        public void LoadCargo(Warehouse warehouse, int warehouseId) => memoryRepositoryWarehouse.Update(warehouse, warehouseId);

        public void UnloadCargo(Warehouse warehouse, int warehouseId) => memoryRepositoryWarehouse.Update(warehouse, warehouseId);

        public void UnloadAllCargos(Warehouse warehouse, int warehouseId) => memoryRepositoryWarehouse.Update(warehouse, warehouseId);
    }
}
