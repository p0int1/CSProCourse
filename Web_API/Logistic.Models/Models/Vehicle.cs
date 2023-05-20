using Logistic.Enums;

namespace Logistic.Models
{
    public class Vehicle : IEntity
    {
        [Obsolete]
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string Number { get; set; }
        public int MaxCargoWeightKg { get; set; }
        [Obsolete]
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        [Obsolete]
        public List<Cargo> Cargos { get; set; } = new List<Cargo>();
    }
}
