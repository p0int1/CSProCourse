using System;
using System.Collections.Generic;
using System.Linq;
using Logistic.ConsoleClient.Enums;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services
{
    public class VehicleService
    {
        const double POUNDS_IN_KILOGRAM = 2.2046;
        static int lastVechicalId = 0;
        Vehicle vehicle;
        List<Vehicle> listVehicles;
        public InMemeoryRepository<Vehicle> memoryRepositoryVehicle;

        public VehicleService()
        {
            memoryRepositoryVehicle = new InMemeoryRepository<Vehicle>();
        }

        public void Create()
        {
            //DataEntryAndPrint.VehicleDataEntry(out VehicleType type, out int maxCargo, out double maxVolume, out string number);
            DataEntryAndPrint.VehicleDataRandom(out VehicleType type, out int maxCargo, out double maxVolume, out string number);
            vehicle = new Vehicle()
            {
                Id = ++lastVechicalId,
                Type = type,
                MaxCargoWeightKg = maxCargo,
                MaxCargoWeightPnd = maxCargo * POUNDS_IN_KILOGRAM,
                MaxCargoVolume = maxVolume,
                Number = number
            };
            memoryRepositoryVehicle.Create(vehicle);
            DataEntryAndPrint.ColorPrint("**** add vehicle in MemoryRepository:", ConsoleColor.Blue);
            DataEntryAndPrint.VehicleDataPrint(vehicle);
        }

        public void GetById(int vehicleId)
        {
            var result = memoryRepositoryVehicle.CheckById(vehicleId);
            if (result)
            {
                vehicle = memoryRepositoryVehicle.Read();
                DataEntryAndPrint.VehicleDataPrint(vehicle);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
            }
        }

        public void GetAll()
        {
            listVehicles = memoryRepositoryVehicle.ReadAll();
            if (listVehicles.Count > 0)
            {
                DataEntryAndPrint.VehicleListDataPrint(listVehicles);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no any vehicles", ConsoleColor.Red);
            }
        }

        public void UpdateById(int vehicleId)
        {
            var result = memoryRepositoryVehicle.CheckById(vehicleId);
            if (result)
            {
                //DataEntryAndPrint.VehicleDataEntry(out VehicleType type, out int maxCargo, out double maxVolume, out string number);
                DataEntryAndPrint.VehicleDataRandom(out VehicleType type, out int maxCargo, out double maxVolume, out string number);
                vehicle = new Vehicle()
                {
                    Id = vehicleId,
                    Type = type,
                    MaxCargoWeightKg = maxCargo,
                    MaxCargoWeightPnd = maxCargo * POUNDS_IN_KILOGRAM,
                    MaxCargoVolume = maxVolume,
                    Number = number
                };
                memoryRepositoryVehicle.Update(vehicle);
                DataEntryAndPrint.ColorPrint("**** update vehicle in MemoryRepository:", ConsoleColor.Blue);
                DataEntryAndPrint.VehicleDataPrint(vehicle);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
            }
        }

        public void DeleteById(int vehicleId)
        {
            var result = memoryRepositoryVehicle.CheckById(vehicleId);
            if (result)
            {
                memoryRepositoryVehicle.Delete();
                DataEntryAndPrint.ColorPrint("**** del vehicle from MemoryRepository!", ConsoleColor.Blue);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
            }
        }

        public void DeleteAll()
        {
            memoryRepositoryVehicle.DeleteAll();
            DataEntryAndPrint.ColorPrint("**** del all vehicles from MemoryRepository!", ConsoleColor.Blue);
        }

        public void LoadCargo(int vehicleId)
        {
            var result = memoryRepositoryVehicle.CheckById(vehicleId);
            if (result)
            {
                vehicle = memoryRepositoryVehicle.Read();
                //DataEntryAndPrint.CargoDataEntry(out int weightKilograms, out double volume, out string code, 
                //    out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber);
                DataEntryAndPrint.CargoDataRandom(out int weightKilograms, out double volume, out string code, 
                    out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber);
                int totalWeightKilograms = vehicle.Cargos.Sum(x => x.Weight);
                double totalVolume = vehicle.Cargos.Sum(x => x.Volume);
                if (totalWeightKilograms + weightKilograms < vehicle.MaxCargoWeightKg && totalVolume + volume < vehicle.MaxCargoVolume)
                {
                    vehicle.Cargos.Add(new Cargo
                    {
                        Id = Guid.NewGuid(),
                        Invoice = new Invoice
                        {
                            Id = Guid.NewGuid(),
                            RecipientAddress= recipientAddress,
                            RecipientPhoneNumber= recipientPhoneNumber,
                            SenderAddress= senderAddress,
                            SenderPhoneNumber= senderPhoneNumber
                        },
                        Volume = volume,
                        Weight = weightKilograms,
                        Code = code
                    });
                    memoryRepositoryVehicle.Update(vehicle);
                    DataEntryAndPrint.ColorPrint("**** update vehicle in MemoryRepository:", ConsoleColor.Blue);
                    DataEntryAndPrint.VehicleDataPrint(vehicle);
                }
                else
                {
                    DataEntryAndPrint.ColorPrint("**** free weightKg or volume is not enough!", ConsoleColor.Red);
                }
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
            }
        }

        public void UnloadCargo(int vehicleId)
        {
            var result = memoryRepositoryVehicle.CheckById(vehicleId);
            if (result)
            {
                vehicle = memoryRepositoryVehicle.Read();
                Guid cargoGuid = DataEntryAndPrint.GetInputCargoGuid();
                var cargo = vehicle.Cargos.FirstOrDefault(x => x.Id == cargoGuid);
                if (cargo != null)
                {
                    vehicle.Cargos.Remove(cargo);
                    memoryRepositoryVehicle.Update(vehicle);
                    DataEntryAndPrint.ColorPrint("**** update vehicle in MemoryRepository:", ConsoleColor.Blue);
                    DataEntryAndPrint.VehicleDataPrint(vehicle);
                }
                else
                {
                    DataEntryAndPrint.ColorPrint("**** there is no cargo with this Id", ConsoleColor.Red);
                }
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
            }
        }

        public void UnloadAllCargos(int vehicleId)
        {
            var result = memoryRepositoryVehicle.CheckById(vehicleId);
            if (result)
            {
                vehicle = memoryRepositoryVehicle.Read();
                vehicle.Cargos.Clear();
                memoryRepositoryVehicle.Update(vehicle);
                DataEntryAndPrint.ColorPrint("**** update vehicle in MemoryRepository:", ConsoleColor.Blue);
                DataEntryAndPrint.VehicleDataPrint(vehicle);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
            }
        }
    }
}
