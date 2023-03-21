using System;

namespace Logistic.ConsoleClient
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            void SuccessScenario()
            {
                var instance1 = new Vehicle(VehicleType.Car, 7000, 6.0f) { Number = "AE0373AM" };
                Console.WriteLine(instance1.GetInformation());
                instance1.LoadCargo(new Cargo() { Volume = 2.0f, Weight = 1000, Code = "2GY5R509"});
                instance1.LoadCargo(new Cargo() { Volume = 1.0f, Weight = 2000, Code = "3ZY5R789" });
                instance1.LoadCargo(new Cargo() { Volume = 1.0f, Weight = 2700, Code = "5PT5R156" });
                Console.WriteLine(instance1.GetInformation());
            }

            void ExceptionScenarion()
            {
                var instance2 = new Vehicle(VehicleType.Car, 5000, 5.0f) { Number = "AH2576AE" };
                Console.WriteLine(instance2.GetInformation());
                instance2.LoadCargo(new Cargo() { Volume = 1.0f, Weight = 1100, Code = "7UT5R512" });
                instance2.LoadCargo(new Cargo() { Volume = 1.0f, Weight = 1500, Code = "4CA5R951" });
                instance2.LoadCargo(new Cargo() { Volume = 1.0f, Weight = 1100, Code = "0DN5R222" });
                instance2.LoadCargo(new Cargo() { Volume = 1.0f, Weight = 2500, Code = "1PL5R178" }); // => over
                instance2.LoadCargo(new Cargo() { Volume = 1.0f, Weight = 2900, Code = "2ZP5R004" });
                Console.WriteLine(instance2.GetInformation());

            }
           
            try
            {
                SuccessScenario();
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                Console.Clear();
                ExceptionScenarion();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}