using System.Collections.Generic;
using Logistic.ConsoleClient.Enums;

namespace Logistic.ConsoleClient.Models
{
    public class Vehicle : IEntity
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string Number { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; } = new List<Cargo>();
    }
}
