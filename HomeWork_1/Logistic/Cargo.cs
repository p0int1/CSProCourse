namespace Logistic.ConsoleClient
{
    internal class Cargo
    {
        internal float Volume { get; set; }
        internal int Weight { get; set; }
        internal string Code { get; set; } = "no code";

        internal string GetInformation()
        {
            return $"Cargo volume: {Volume} \n Weight: {Weight} кг. \n Cargo code: {Code}";
        }
    }
}
