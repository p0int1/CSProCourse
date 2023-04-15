using System;
using System.Text;
using System.Collections.Generic;
using Logistic.Enums;
using Logistic.Models;


namespace Logistic.ConsoleClient
{
    public class DataEntryAndPrint
    {
        public static void VehicleDataEntry(out VehicleType type, out int maxCargo, out double maxVolume, out string number)
        {
            Console.Write("enter VehicleType: ");
            switch (Console.ReadLine().ToLower())
            {
                case "car":
                default:
                    type = VehicleType.Car;
                    break;
                case "ship":
                    type = VehicleType.Ship;
                    break;
                case "plane":
                    type = VehicleType.Plane;
                    break;
                case "train":
                    type = VehicleType.Train;
                    break;
            }
            Console.Write("enter VehicleNumber: ");
            number = Console.ReadLine();
            Console.Write("enter MaxCargoWeightKg: ");
            maxCargo = int.Parse(Console.ReadLine()) ;
            Console.Write("enter MaxCargoVolume: ");
            maxVolume = double.Parse(Console.ReadLine());
        }

        public static void VehicleDataRandom(out VehicleType type, out int maxCargo, out double maxVolume, out string number)
        {
            Random rnd = new Random();
            type = VehicleType.Car;
            maxCargo = rnd.Next(100, 10000);
            maxVolume = rnd.Next(1, 10);
            number = $"{GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 2)}{rnd.Next(1000, 9999)}{GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 2)}";
        }

        public static void CargoDataEntry(out int weightKilograms, out double volume, out string code, 
            out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber)
        {
            Console.Write("enter weightKilograms: ");
            weightKilograms = int.Parse(Console.ReadLine());
            Console.Write("enter volumeCargo: ");
            volume = double.Parse(Console.ReadLine());
            Console.Write("enter codeCargo: ");
            code = Console.ReadLine();
            Console.Write("enter recipientAddress: ");
            recipientAddress = Console.ReadLine();
            Console.Write("enter recipientPhoneNumber: ");
            recipientPhoneNumber = Console.ReadLine();
            Console.Write("enter senderAddress: ");
            senderAddress = Console.ReadLine();
            Console.Write("enter senderPhoneNumber: ");
            senderPhoneNumber = Console.ReadLine();
        }

        public static void CargoDataRandom(out int weightKilograms, out double volume, out string code, 
            out string recipientAddress, out string recipientPhoneNumber, out string senderAddress, out string senderPhoneNumber)
        {
            Random rnd = new Random();
            weightKilograms = rnd.Next(50, 500);
            volume = rnd.Next(1, 2);
            code = GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10);
            recipientAddress = $"STR. {GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 5)}, {rnd.Next(1, 99)}";
            recipientPhoneNumber = $"+380{GetRandomString("0123456789", 10)}";
            senderAddress = $"STR. {GetRandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 5)}, {rnd.Next(1, 99)}";
            senderPhoneNumber = $"+380{GetRandomString("0123456789", 10)}";
        }

        public static void VehicleDataPrint(Vehicle vehicle)
        {
            Console.WriteLine($"ID: {vehicle.Id}\nVehicleType: {vehicle.Type}\nVehicleNumber: {vehicle.Number}\n" +
                $"MaxCargoWeightKg: {vehicle.MaxCargoWeightKg}\nMaxCargoVolume: {vehicle.MaxCargoVolume}");
            if (vehicle.Cargos.Count > 0)
            {
                vehicle.Cargos.ForEach(x => Console.WriteLine($"---- {x.Id} {x.Code} - {x.Weight}, {x.Volume}"));
            }
        }

        public static void WarehouseDataPrint(Warehouse warehouse)
        {
            Console.WriteLine($"ID: {warehouse.Id}");
            if (warehouse.Cargos.Count > 0)
            {
                warehouse.Cargos.ForEach(x => Console.WriteLine($"---- {x.Id} {x.Code} - {x.Weight}, {x.Volume}"));
            }
        }

        public static void VehicleListDataPrint(List<Vehicle> listVehicle)
        {
            foreach(Vehicle vehicle in listVehicle)
            {
                VehicleDataPrint(vehicle);
                Console.WriteLine(new string('-', 25));
            }
        }

        public static void WarehouseListDataPrint(List<Warehouse> listWarehouse)
        {
            foreach (Warehouse warehouse in listWarehouse)
            {
                WarehouseDataPrint(warehouse);
                Console.WriteLine(new string('-', 25));
            }
        }

        public static void ShowHelp()
        {
            ColorPrint("add vehicle - creates a transport entity", ConsoleColor.DarkCyan);
            ColorPrint("add warehouse - creates a warehouse entity", ConsoleColor.DarkCyan);
            ColorPrint("get vehicle 1 - get a transport entity by id", ConsoleColor.DarkCyan);
            ColorPrint("get warehouse 1 - get a warehouse entity by id", ConsoleColor.DarkCyan);
            ColorPrint("get-all vehicle - get all transport entitys", ConsoleColor.DarkCyan);
            ColorPrint("get-all warehouse - get all warehouse entitys", ConsoleColor.DarkCyan);
            ColorPrint("delete vehicle 5 - delete transport entity by id", ConsoleColor.DarkCyan);
            ColorPrint("delete warehouse 5 - delete warehouse entity by id", ConsoleColor.DarkCyan);
            ColorPrint("delete-all vehicle - delete all transport entitys", ConsoleColor.DarkCyan);
            ColorPrint("delete-all warehouse - delete all warehouse entitys", ConsoleColor.DarkCyan);
            ColorPrint("load-cargo vehicle 2 - load cargo in vehicle by vehicleId", ConsoleColor.DarkCyan);
            ColorPrint("load-cargo warehouse 2 - load cargo in warehouse by warehouseId", ConsoleColor.DarkCyan);
            ColorPrint("unload-cargo vehicle 4 - unload cargo from vehicle by vehicleId", ConsoleColor.DarkCyan);
            ColorPrint("unload-cargo warehouse 4 - unload cargo from warehouse by warehouseId", ConsoleColor.DarkCyan);
            ColorPrint("unload-all-cargos vehicle 6 - unload all cargos from vehicle by vehicleId", ConsoleColor.DarkCyan);
            ColorPrint("unload-all-cargos warehouse 6 - unload all cargos from warehouse by warehouseId", ConsoleColor.DarkCyan);
            ColorPrint("create-report xml - serializes entities into xml files", ConsoleColor.DarkCyan);
            ColorPrint("create-report json - serializes entities into json files", ConsoleColor.DarkCyan);
            ColorPrint("load-report filename - deserializes entities from file", ConsoleColor.DarkCyan);
            ColorPrint("help - this page", ConsoleColor.DarkCyan);
            ColorPrint("quit - exit", ConsoleColor.DarkCyan);
        }

        public static Guid GetInputCargoGuid()
        {
            Console.Write("enter CargoGuid: ");
            Guid.TryParse(Console.ReadLine(), out var guid);
            return guid;
        }

        public static void ColorPrint(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static string GetRandomString(string characterSet, int Length)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            for (int i = 0; i < Length; i++)
            {
                int Position = rnd.Next(0, characterSet.Length - 1);
                sb.Append(characterSet[Position]);
            }
            return sb.ToString();
        }
    }
}
