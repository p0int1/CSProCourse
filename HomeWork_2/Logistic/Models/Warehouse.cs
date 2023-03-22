using System.Collections.Generic;

namespace Logistic.ConsoleClient.Models
{
    public class Warehouse : IEntity
    {
        public int Id { get; set; }
        public List<Cargo> Cargos { get; set; } = new List<Cargo>();
    }
}
