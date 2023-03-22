using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logistic.ConsoleClient.Services
{
    public class WarehouseService
    {
        static int lastWarehouseId = 0;
        Warehouse warehouse;
        List<Warehouse> listWarehouse;
        public InMemeoryRepository<Warehouse> memoryRepositoryWarehouse;

        public WarehouseService()
        {
            memoryRepositoryWarehouse = new InMemeoryRepository<Warehouse>();
        }

        public void Create()
        {
            warehouse = new Warehouse()
            {
                Id = ++lastWarehouseId
            };
            memoryRepositoryWarehouse.Create(warehouse);
            DataEntryAndPrint.ColorPrint("**** add warehouse in MemoryRepository:", ConsoleColor.Blue);
            DataEntryAndPrint.WarehouseDataPrint(warehouse);
        }

        public void GetById(int warehouseId)
        {
            var result = memoryRepositoryWarehouse.CheckById(warehouseId);
            if (result)
            {
                warehouse = memoryRepositoryWarehouse.Read();
                DataEntryAndPrint.WarehouseDataPrint(warehouse);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
            }
        }

        public void GetAll()
        {
            listWarehouse = memoryRepositoryWarehouse.ReadAll();
            if (listWarehouse.Count > 0)
            {
                DataEntryAndPrint.WarehouseListDataPrint(listWarehouse);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no any warehouse", ConsoleColor.Red);
            }
        }

        public void UpdateById(int warehouseId)
        {
            var result = memoryRepositoryWarehouse.CheckById(warehouseId);
            if (result)
            {
                warehouse = new Warehouse()
                {
                    Id = warehouseId,
                };
                memoryRepositoryWarehouse.Update(warehouse);
                DataEntryAndPrint.ColorPrint("**** update warehouse in MemoryRepository:", ConsoleColor.Blue);
                DataEntryAndPrint.WarehouseDataPrint(warehouse);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
            }
        }

        public void DeleteById(int warehouseId)
        {
            var result = memoryRepositoryWarehouse.CheckById(warehouseId);
            if (result)
            {
                memoryRepositoryWarehouse.Delete();
                DataEntryAndPrint.ColorPrint("**** del warehouse from MemoryRepository!", ConsoleColor.Blue);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
            }
        }

        public void DeleteAll()
        {
            memoryRepositoryWarehouse.DeleteAll();
            DataEntryAndPrint.ColorPrint("**** del all warehouse from MemoryRepository!", ConsoleColor.Blue);
        }

        public void LoadCargo(int vehicleId)
        {
            var result = memoryRepositoryWarehouse.CheckById(vehicleId);
            if (result)
            {
                warehouse = memoryRepositoryWarehouse.Read();
                //DataEntryAndPrint.CargoDataEntry(out int weightKilograms, out double volume, out string code, 
                //    out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber);
                DataEntryAndPrint.CargoDataRandom(out int weightKilograms, out double volume, out string code,
                    out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber);
                warehouse.Cargos.Add(new Cargo
                {
                    Id = Guid.NewGuid(),
                    Invoice = new Invoice
                    {
                        Id = Guid.NewGuid(),
                        RecipientAddress = recipientAddress,
                        RecipientPhoneNumber = recipientPhoneNumber,
                        SenderAddress = senderAddress,
                        SenderPhoneNumber = senderPhoneNumber
                    },
                    Volume = volume,
                    Weight = weightKilograms,
                    Code = code
                });
                memoryRepositoryWarehouse.Update(warehouse);
                DataEntryAndPrint.ColorPrint("**** update warehouse in MemoryRepository:", ConsoleColor.Blue);
                DataEntryAndPrint.WarehouseDataPrint(warehouse);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
            }
        }

        public void UnloadCargo(int warehouseId)
        {
            var result = memoryRepositoryWarehouse.CheckById(warehouseId);
            if (result)
            {
                warehouse = memoryRepositoryWarehouse.Read();
                Guid cargoGuid = DataEntryAndPrint.GetInputCargoGuid();
                var cargo = warehouse.Cargos.FirstOrDefault(x => x.Id == cargoGuid);
                if (cargo != null)
                {
                    warehouse.Cargos.Remove(cargo);
                    memoryRepositoryWarehouse.Update(warehouse);
                    DataEntryAndPrint.ColorPrint("**** update warehouse in MemoryRepository:", ConsoleColor.Blue);
                    DataEntryAndPrint.WarehouseDataPrint(warehouse);
                }
                else
                {
                    DataEntryAndPrint.ColorPrint("**** there is no cargo with this Id", ConsoleColor.Red);
                }
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
            }
        }

        public void UnloadAllCargos(int warehouseId)
        {
            var result = memoryRepositoryWarehouse.CheckById(warehouseId);
            if (result)
            {
                warehouse = memoryRepositoryWarehouse.Read();
                warehouse.Cargos.Clear();
                memoryRepositoryWarehouse.Update(warehouse);
                DataEntryAndPrint.ColorPrint("**** update warehouse in MemoryRepository:", ConsoleColor.Blue);
                DataEntryAndPrint.WarehouseDataPrint(warehouse);
            }
            else
            {
                DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
            }
        }
    }
}
