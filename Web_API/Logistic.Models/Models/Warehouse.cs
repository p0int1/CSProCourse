namespace Logistic.Models
{
    public class Warehouse : IEntity
    {
        [Obsolete]
        public int Id { get; set; }
        [Obsolete]
        public List<Cargo> Cargos { get; set; } = new List<Cargo>();
    }
}
