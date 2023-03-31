using System;
using System.IO;
using System.Linq;
using Logistic.ConsoleClient.Enums;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient
{
    public class CommandExecutor
    {
        const double POUNDS_IN_KILOGRAM = 2.2046;

        private static bool CheckCommandParts(string[] commandParts, int parts)
        {
            if (commandParts.Length != parts)
            {
                DataEntryAndPrint.ColorPrint("**** wrong format of the command", ConsoleColor.Red);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void Add(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 2)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    //DataEntryAndPrint.VehicleDataEntry(out VehicleType type, out int maxCargo, out double maxVolume, out string number);
                    DataEntryAndPrint.VehicleDataRandom(out VehicleType type, out int maxCargo, out double maxVolume, out string number);
                    var vehicle = new Vehicle()
                    {
                        Type = type,
                        MaxCargoWeightKg = maxCargo,
                        MaxCargoWeightPnd = maxCargo * POUNDS_IN_KILOGRAM,
                        MaxCargoVolume = maxVolume,
                        Number = number
                    };
                    InfrastructureBuilder._vehicleService.Create(vehicle);
                    DataEntryAndPrint.ColorPrint("**** add vehicle in MemoryRepository:", ConsoleColor.Blue);
                    DataEntryAndPrint.VehicleDataPrint(vehicle);
                    break;
                case "warehouse":
                    var warehouse = new Warehouse();
                    InfrastructureBuilder._warehouseService.Create(warehouse);
                    DataEntryAndPrint.ColorPrint("**** add warehouse in MemoryRepository:", ConsoleColor.Blue);
                    DataEntryAndPrint.WarehouseDataPrint(warehouse);
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void Get(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            int.TryParse(commandParts[2], out int id);
            switch (commandParts[1])
            {
                case "vehicle":
                    var vehicle = InfrastructureBuilder._vehicleService.GetById(id);
                    if (vehicle != null)
                    {
                        DataEntryAndPrint.VehicleDataPrint(vehicle);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
                    }
                    break;
                case "warehouse":
                    var warehouse = InfrastructureBuilder._warehouseService.GetById(id);
                    if (warehouse != null)
                    {
                        DataEntryAndPrint.WarehouseDataPrint(warehouse);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
                    }
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void GetAll(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 2)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    var listVehicles = InfrastructureBuilder._vehicleService.GetAll();
                    if (listVehicles.Count > 0)
                    {
                        DataEntryAndPrint.VehicleListDataPrint(listVehicles);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no any vehicles", ConsoleColor.Red);
                    }
                    break;
                case "warehouse":
                    var listWarehouse = InfrastructureBuilder._warehouseService.GetAll();
                    if (listWarehouse.Count > 0)
                    {
                        DataEntryAndPrint.WarehouseListDataPrint(listWarehouse);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no any warehouse", ConsoleColor.Red);
                    }
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void Delete(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            int.TryParse(commandParts[2], out int id);
            switch (commandParts[1])
            {
                case "vehicle":
                    var isVehicleDelete = InfrastructureBuilder._vehicleService.DeleteById(id);
                    if (isVehicleDelete)
                    {
                        DataEntryAndPrint.ColorPrint("**** del vehicle from MemoryRepository!", ConsoleColor.Blue);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
                    }
                    break;
                case "warehouse":
                    var isWarehouseDelete = InfrastructureBuilder._warehouseService.DeleteById(id);
                    if (isWarehouseDelete)
                    {
                        DataEntryAndPrint.ColorPrint("**** del warehouse from MemoryRepository!", ConsoleColor.Blue);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
                    }
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void DeleteAll(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 2)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    InfrastructureBuilder._vehicleService.DeleteAll();
                    DataEntryAndPrint.ColorPrint("**** del all vehicles from MemoryRepository!", ConsoleColor.Blue);
                    break;
                case "warehouse":
                    InfrastructureBuilder._warehouseService.DeleteAll();
                    DataEntryAndPrint.ColorPrint("**** del all warehouse from MemoryRepository!", ConsoleColor.Blue);
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void LoadCargo(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            int.TryParse(commandParts[2], out int id);
            switch (commandParts[1])
            {
                case "vehicle":
                    var vehicle = InfrastructureBuilder._vehicleService.GetById(id);
                    if (vehicle != null)
                    {
                        //DataEntryAndPrint.CargoDataEntry(out int weightKilograms, out double volume, out string code, 
                        //    out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber);
                        DataEntryAndPrint.CargoDataRandom(out int weightKilograms, out double volume, out string code,
                            out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber);
                        vehicle.Cargos.Add(new Cargo
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
                            var isLoaded = InfrastructureBuilder._vehicleService.LoadCargo(vehicle, id);
                        if (isLoaded)
                        {
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
                    break;
                case "warehouse":
                    var warehouse = InfrastructureBuilder._warehouseService.GetById(id);
                    if (warehouse != null)
                    {
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
                        InfrastructureBuilder._warehouseService.LoadCargo(warehouse, id);
                        DataEntryAndPrint.ColorPrint("**** update warehouse in MemoryRepository:", ConsoleColor.Blue);
                        DataEntryAndPrint.WarehouseDataPrint(warehouse);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
                    }
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void UnloadCargo(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            int.TryParse(commandParts[2], out int id);
            switch (commandParts[1])
            {
                case "vehicle":
                    var vehicle = InfrastructureBuilder._vehicleService.GetById(id);
                    if (vehicle != null)
                    {
                        Guid cargoGuid = DataEntryAndPrint.GetInputCargoGuid();
                        var cargo = vehicle.Cargos.FirstOrDefault(x => x.Id == cargoGuid);
                        if (cargo != null)
                        {
                            vehicle.Cargos.Remove(cargo);
                            InfrastructureBuilder._vehicleService.UnloadCargo(vehicle, id);
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
                    break;
                case "warehouse":
                    var warehouse = InfrastructureBuilder._warehouseService.GetById(id);
                    if (warehouse != null)
                    {
                        Guid cargoGuid = DataEntryAndPrint.GetInputCargoGuid();
                        var cargo = warehouse.Cargos.FirstOrDefault(x => x.Id == cargoGuid);
                        if (cargo != null)
                        {
                            warehouse.Cargos.Remove(cargo);
                            InfrastructureBuilder._warehouseService.UnloadCargo(warehouse, id);
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
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void UnloadAllCargos(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            int.TryParse(commandParts[2], out int id);
            switch (commandParts[1])
            {
                case "vehicle":
                    var vehicle = InfrastructureBuilder._vehicleService.GetById(id);
                    if (vehicle != null)
                    {
                        vehicle.Cargos.Clear();
                        InfrastructureBuilder._vehicleService.UnloadAllCargos(vehicle, id);
                        DataEntryAndPrint.ColorPrint("**** update vehicle in MemoryRepository:", ConsoleColor.Blue);
                        DataEntryAndPrint.VehicleDataPrint(vehicle);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no vehicle with this Id", ConsoleColor.Red);
                    }
                    break;
                case "warehouse":
                    var warehouse = InfrastructureBuilder._warehouseService.GetById(id);
                    if (warehouse != null)
                    {
                        warehouse.Cargos.Clear();
                        InfrastructureBuilder._warehouseService.UnloadAllCargos(warehouse, id);
                        DataEntryAndPrint.ColorPrint("**** update warehouse in MemoryRepository:", ConsoleColor.Blue);
                        DataEntryAndPrint.WarehouseDataPrint(warehouse);
                    }
                    else
                    {
                        DataEntryAndPrint.ColorPrint("**** there is no warehouse with this Id", ConsoleColor.Red);
                    }
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void CreateReport(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 2)) { return; }
            var listVehicles = InfrastructureBuilder._vehicleService.GetAll();
            var listWarehouse = InfrastructureBuilder._warehouseService.GetAll();
            switch (commandParts[1])
            {
                case "json":
                    InfrastructureBuilder._vehicleReportService.CreateReport(listVehicles, ReportType.json);
                    InfrastructureBuilder._warehouseReportService.CreateReport(listWarehouse, ReportType.json);
                    DataEntryAndPrint.ColorPrint("**** json report files created", ConsoleColor.Blue);
                    break;
                case "xml":
                    InfrastructureBuilder._vehicleReportService.CreateReport(listVehicles, ReportType.xml);
                    InfrastructureBuilder._warehouseReportService.CreateReport(listWarehouse, ReportType.xml);
                    DataEntryAndPrint.ColorPrint("**** xml report files created", ConsoleColor.Blue);
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void LoadReport(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 2)) { return; }
            if (!File.Exists(commandParts[1])) 
            {
                DataEntryAndPrint.ColorPrint($"**** file {commandParts[1]} not exist!", ConsoleColor.Red);
                return; 
            }
            var entityType = commandParts[1].Contains("vehicle") ? "vehicle" : "warehouse";
            switch (entityType)
            {
                case "vehicle":
                    var vehicleList = InfrastructureBuilder._vehicleReportService.LoadReport(commandParts[1]);
                    InfrastructureBuilder._vehicleService.memoryRepositoryVehicle.DeleteAll();
                    vehicleList.ForEach(x => InfrastructureBuilder._vehicleService.memoryRepositoryVehicle.Create(x));
                    DataEntryAndPrint.ColorPrint("**** Vehicle object restored:", ConsoleColor.Blue);
                    DataEntryAndPrint.VehicleListDataPrint(vehicleList);
                    break;
                case "warehouse":
                    var warehouseList = InfrastructureBuilder._warehouseReportService.LoadReport(commandParts[1]);
                    InfrastructureBuilder._warehouseService.memoryRepositoryWarehouse.DeleteAll();
                    warehouseList.ForEach(x => InfrastructureBuilder._warehouseService.memoryRepositoryWarehouse.Create(x));
                    DataEntryAndPrint.ColorPrint("**** Warehouse object restored:", ConsoleColor.Blue);
                    DataEntryAndPrint.WarehouseListDataPrint(warehouseList);
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown report file format", ConsoleColor.Red);
                    break;
            }
        }
    }
}
