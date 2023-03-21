using System;
using System.Collections.Generic;
using System.Text;

namespace Logistic.ConsoleClient
{
    internal class Vehicle
    {
        const float POUNDS_IN_KILOGRAM = 2.2046f;
        private float volume = 0f;
        private int weightKilograms = 0;
        private float weightPounds = 0f;
        private VehicleType Type { get; set; }
        public string Number { get; set; } = "no number";
        public int MaxCargoWeightKg { get; set; }
        public float MaxCargoWeightPnd { get; set; }
        public float MaxCargoVolume { get; set; }
        private List<Cargo> Cargos { get; set; }

        private StringBuilder sb = new StringBuilder();

        public Vehicle(VehicleType type, int maxCargoWeightKg, float maxCargoVolume)
        {
            Type = type;
            MaxCargoWeightKg = maxCargoWeightKg;
            MaxCargoWeightPnd = (float)(maxCargoWeightKg * POUNDS_IN_KILOGRAM);
            MaxCargoVolume = maxCargoVolume;
            Cargos = new List<Cargo>();
        }

        private string GetCargoVolumeLeft()
        {
            return (MaxCargoVolume - volume).ToString() + "(m3)";
        }

        private string GetCargoWeightLeft(WeightUnit weightUnit)
        {
            string weight;
            switch (weightUnit)
            {
                case WeightUnit.Kilograms:
                default:
                    weight = (MaxCargoWeightKg - weightKilograms).ToString() + "(kg)";
                    break;
                case WeightUnit.Pounds:
                    weight = (MaxCargoWeightPnd - weightPounds).ToString() + "(pd)";
                    break;
            }
            return weight;
        }

        internal string GetInformation()
        {
            sb.Clear();
            sb.AppendLine($"Number           : {Number}");
            sb.AppendLine(new string('=', 30));
            sb.AppendLine($"Transport type   : {Type}");
            sb.AppendLine($"Max Cargo(kg)    : {MaxCargoWeightKg}");
            sb.AppendLine($"Max volume(m3)   : {MaxCargoVolume}");
            if (Cargos.Count > 0) 
            {
                sb.AppendLine(new string('-', 30));
                sb.AppendLine($"Number of cargo  : {Cargos.Count}");
                sb.AppendLine($"Total weight(kg) : {weightKilograms}");
                sb.AppendLine($"Total volume(m3) : {volume}");
                sb.AppendLine($"            Left : {GetCargoWeightLeft(WeightUnit.Kilograms)}\n" +
                              $"                 : {GetCargoVolumeLeft()}");
            }
            return sb.ToString();
        }

        internal void LoadCargo(Cargo cargo)
        {
            Cargos.Add(cargo);
            volume += cargo.Volume;
            weightKilograms += cargo.Weight;
            weightPounds = weightKilograms * POUNDS_IN_KILOGRAM;

            if ((MaxCargoWeightKg - weightKilograms) < 0)
            {
                throw new Exception("Load capacity exceeded\n");
            }
            if ((MaxCargoVolume - volume) < 0)
            {
                throw new Exception("Not enough space\n");
            }
        }
    }
}
