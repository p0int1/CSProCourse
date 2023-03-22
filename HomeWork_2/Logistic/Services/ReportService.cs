using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Logistic.ConsoleClient.Enums;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services
{
    public class ReportService
    {
        List<Vehicle> vehicles;
        List<Warehouse> warehouses;
        JsonRepository<List<Vehicle>> vehicleJson;
        JsonRepository<List<Warehouse>> warehouseJson;
        XmlRepository<List<Vehicle>> vehicleXml;
        XmlRepository<List<Warehouse>> warehouseXml;
        string reportDir;

        public ReportService()
        {
            vehicleJson = new JsonRepository<List<Vehicle>>();
            warehouseJson = new JsonRepository<List<Warehouse>>();
            vehicleXml = new XmlRepository<List<Vehicle>>();
            warehouseXml = new XmlRepository<List<Warehouse>>();
            string appDir = Directory.GetCurrentDirectory();
            reportDir = Path.Combine(appDir, "reports");
            Directory.CreateDirectory(reportDir);
        }

        public void CreateReport(ReportType reportType)
        {
            vehicles = InfrastructureBuilder.vehicleService.memoryRepositoryVehicle.ReadAll();
            warehouses = InfrastructureBuilder.warehouseService.memoryRepositoryWarehouse.ReadAll();
            switch (reportType)
            {
                case ReportType.json:
                    vehicleJson.Create(vehicles, reportDir);
                    warehouseJson.Create(warehouses, reportDir);
                    DataEntryAndPrint.ColorPrint("**** json report files created", ConsoleColor.Blue);
                    break;
                case ReportType.xml:
                    vehicleXml.Create(vehicles, reportDir);
                    warehouseXml.Create(warehouses, reportDir);
                    DataEntryAndPrint.ColorPrint("**** xml report files created", ConsoleColor.Blue);
                    break;
            }
        }

        public void LoadReport(string filePath)
        {
            var reportType = filePath.Split('.').Last();
            var entityType = filePath.Contains("vehicle") ? "vehicle" : "warehouse";
            var actionType = $"{entityType}.{reportType}";
            switch (actionType)
            {
                case "vehicle.json":
                    vehicles = vehicleJson.Read(filePath);
                    InfrastructureBuilder.vehicleService.memoryRepositoryVehicle.DeleteAll();
                    vehicles.ForEach(x => InfrastructureBuilder.vehicleService.memoryRepositoryVehicle.Create(x));
                    InfrastructureBuilder.vehicleService.memoryRepositoryVehicle.ReadAll();
                    break;
                case "warehouse.json":
                    warehouses = warehouseJson.Read(filePath);
                    InfrastructureBuilder.warehouseService.memoryRepositoryWarehouse.DeleteAll();
                    warehouses.ForEach(x => InfrastructureBuilder.warehouseService.memoryRepositoryWarehouse.Create(x));
                    InfrastructureBuilder.warehouseService.memoryRepositoryWarehouse.ReadAll();
                    break;
                case "vehicle.xml":
                     vehicleXml.Read(filePath);
                    InfrastructureBuilder.vehicleService.memoryRepositoryVehicle.DeleteAll();
                    vehicles.ForEach(x => InfrastructureBuilder.vehicleService.memoryRepositoryVehicle.Create(x));
                    InfrastructureBuilder.vehicleService.memoryRepositoryVehicle.ReadAll();
                    break;
                case "warehouse.xml":
                    //warehouses = warehouseXml.Read(filePath);
                    InfrastructureBuilder.warehouseService.memoryRepositoryWarehouse.DeleteAll();
                    warehouses.ForEach(x => InfrastructureBuilder.warehouseService.memoryRepositoryWarehouse.Create(x));
                    InfrastructureBuilder.warehouseService.memoryRepositoryWarehouse.ReadAll();
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }
    }
}
