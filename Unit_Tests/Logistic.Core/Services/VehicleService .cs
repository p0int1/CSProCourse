using Logistic.Models;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services
{
    public class VehicleService
    {
        public IRepository<Vehicle> memoryRepositoryVehicle;

        public VehicleService(IRepository<Vehicle> memeoryRepository)
        {
            memoryRepositoryVehicle = memeoryRepository;
        }

        public void Create(Vehicle vehicle) => memoryRepositoryVehicle.Create(vehicle);

        public Vehicle GetById(int vehicleId) => memoryRepositoryVehicle.Read(vehicleId);

        public List<Vehicle> GetAll() => memoryRepositoryVehicle.ReadAll();

        public bool DeleteById(int vehicleId) => memoryRepositoryVehicle.Delete(vehicleId);

        public void DeleteAll() => memoryRepositoryVehicle.DeleteAll();

        public bool LoadCargo(Vehicle vehicle, int vehicleId)
        {
            var result = (vehicle.Cargos.Sum(x => x.Weight) < vehicle.MaxCargoWeightKg 
                && vehicle.Cargos.Sum(x => x.Volume) < vehicle.MaxCargoVolume) ? true : false;
            if (result) { memoryRepositoryVehicle.Update(vehicle, vehicleId); }
            return result; 
        }

        public void UnloadCargo(Vehicle vehicle, int vehicleId) => memoryRepositoryVehicle.Update(vehicle, vehicleId);

        public void UnloadAllCargos(Vehicle vehicle, int vehicleId) => memoryRepositoryVehicle.Update(vehicle, vehicleId);
    }
}
