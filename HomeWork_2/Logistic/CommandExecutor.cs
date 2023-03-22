using Logistic.ConsoleClient.Enums;
using System;

namespace Logistic.ConsoleClient
{
    public class CommandExecutor
    {
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
                    InfrastructureBuilder.vehicleService.Create();
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.Create();
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void Get(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    InfrastructureBuilder.vehicleService.GetById(int.Parse(commandParts[2]));
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.GetById(int.Parse(commandParts[2]));
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
                    InfrastructureBuilder.vehicleService.GetAll();
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.GetAll();
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void Update(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    InfrastructureBuilder.vehicleService.UpdateById(int.Parse(commandParts[2]));
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.UpdateById(int.Parse(commandParts[2]));
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void Delete(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    InfrastructureBuilder.vehicleService.DeleteById(int.Parse(commandParts[2]));
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.DeleteById(int.Parse(commandParts[2]));
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
                    InfrastructureBuilder.vehicleService.DeleteAll();
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.DeleteAll();
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void LoadCargo(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    InfrastructureBuilder.vehicleService.LoadCargo(int.Parse(commandParts[2]));
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.LoadCargo(int.Parse(commandParts[2]));
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void UnloadCargo(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    InfrastructureBuilder.vehicleService.UnloadCargo(int.Parse(commandParts[2]));
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.UnloadCargo(int.Parse(commandParts[2]));
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void UnloadAllCargos(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 3)) { return; }
            switch (commandParts[1])
            {
                case "vehicle":
                    InfrastructureBuilder.vehicleService.UnloadAllCargos(int.Parse(commandParts[2]));
                    break;
                case "warehouse":
                    InfrastructureBuilder.warehouseService.UnloadAllCargos(int.Parse(commandParts[2]));
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void CreateReport(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 2)) { return; }
            switch (commandParts[1])
            {
                case "json":
                    InfrastructureBuilder.reportService.CreateReport(ReportType.json);
                    break;
                case "xml":
                    InfrastructureBuilder.reportService.CreateReport(ReportType.xml);
                    break;
                default:
                    DataEntryAndPrint.ColorPrint("**** unknown second part of the command", ConsoleColor.Red);
                    break;
            }
        }

        public static void LoadReport(string[] commandParts)
        {
            if (!CheckCommandParts(commandParts, 2)) { return; }
            InfrastructureBuilder.reportService.LoadReport(commandParts[1]);
        }
    }
}
