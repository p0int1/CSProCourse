using System;

namespace Logistic.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool proceed = true;
            InfrastructureBuilder.Builder();
            while (proceed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("---> ");
                var userInput = Console.ReadLine().ToLower();
                var commandParts = userInput.Split(' ');
                Console.ResetColor();
                switch (commandParts?[0])
                {
                    case "add":
                        CommandExecutor.Add(commandParts);
                        break;
                    case "get":
                        CommandExecutor.Get(commandParts);
                        break;
                    case "get-all":
                        CommandExecutor.GetAll(commandParts);
                        break;
                    case "update":
                        CommandExecutor.Update(commandParts);
                        break;
                    case "delete":
                        CommandExecutor.Delete(commandParts);
                        break;
                    case "delete-all":
                        CommandExecutor.DeleteAll(commandParts);
                        break;
                    case "load-cargo":
                        CommandExecutor.LoadCargo(commandParts);
                        break;
                    case "unload-cargo":
                        CommandExecutor.UnloadCargo(commandParts);
                        break;
                    case "unload-all-cargos":
                        CommandExecutor.UnloadAllCargos(commandParts);
                        break;
                    case "create-report":
                        CommandExecutor.CreateReport(commandParts);
                        break;
                    case "load-report":
                        CommandExecutor.LoadReport(commandParts);
                        break;
                    case "help":
                        DataEntryAndPrint.ShowHelp();
                        break;
                    case "quit":
                        proceed = false;
                        break;
                    default:
                        DataEntryAndPrint.ColorPrint("**** unknown first part of the command", ConsoleColor.Red);
                        break;
                }
            }
            Console.WriteLine("press any key...");
            Console.ReadKey();
        }
    }
}
