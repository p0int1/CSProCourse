using System;

namespace Logistic.ConsoleClient.Models
{
    public class Cargo
    {
        public Guid Id { get; set; }
        public Invoice Invoice { get; set; }
        public double Volume { get; set; }
        public int Weight { get; set; }
        public string Code { get; set; }
    }
}
