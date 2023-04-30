using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Models;

namespace Logistic.WpfClient
{
    public class AppBuilder
    {
        private static AppBuilder instance;
        public VehicleService vehicleService;
        public ReportService<Vehicle> vehicleReportService;

        protected AppBuilder()
        {
            vehicleService = new VehicleService(new InMemoryRepository<Vehicle>());
            vehicleReportService = new ReportService<Vehicle>(new JsonRepository<Vehicle>(), new XmlRepository<Vehicle>());
        }

        public static AppBuilder GetInstance()
        {
            if (instance == null)
            {
                instance = new AppBuilder();
            }
            return instance;
        }
    }
}
