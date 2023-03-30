using System.Collections.Generic;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services
{
    public class VehicleService
    {
        public InMemeoryRepository<Vehicle> memoryRepositoryVehicle;

        public VehicleService(InMemeoryRepository<Vehicle> memeoryRepository)
        {
            memoryRepositoryVehicle = memeoryRepository;
        }

        public void Create(Vehicle vehicle) => memoryRepositoryVehicle.Create(vehicle);

        public Vehicle GetById(int vehicleId) => memoryRepositoryVehicle.Read(vehicleId);

        public List<Vehicle> GetAll() => memoryRepositoryVehicle.ReadAll();

        public bool DeleteById(int vehicleId) => memoryRepositoryVehicle.Delete(vehicleId);

        public void DeleteAll() => memoryRepositoryVehicle.DeleteAll();

        public void LoadCargo(Vehicle vehicle, int vehicleId) => memoryRepositoryVehicle.Update(vehicle, vehicleId);

        public void UnloadCargo(Vehicle vehicle, int vehicleId) => memoryRepositoryVehicle.Update(vehicle, vehicleId);

        public void UnloadAllCargos(Vehicle vehicle, int vehicleId) => memoryRepositoryVehicle.Update(vehicle, vehicleId);
    }
}
