using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Services;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient
{
    public static class InfrastructureBuilder
    {
        public static VehicleService _vehicleService;
        public static WarehouseService _warehouseService;
        public static ReportService<Vehicle> _vehicleReportService;
        public static ReportService<Warehouse> _warehouseReportService;

        public static void Builder()
        {
            _vehicleService = new VehicleService(new InMemeoryRepository<Vehicle>());
            _warehouseService = new WarehouseService(new InMemeoryRepository<Warehouse>());
            _vehicleReportService = new ReportService<Vehicle>(new JsonRepository<Vehicle>(), new XmlRepository<Vehicle>());
            _warehouseReportService = new ReportService<Warehouse>(new JsonRepository<Warehouse>(), new XmlRepository<Warehouse>());
        }
    }
}
