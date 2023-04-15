using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Models;

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
            _vehicleService = new VehicleService(new InMemoryRepository<Vehicle>());
            _warehouseService = new WarehouseService(new InMemoryRepository<Warehouse>());
            _vehicleReportService = new ReportService<Vehicle>(new JsonRepository<Vehicle>(), new XmlRepository<Vehicle>());
            _warehouseReportService = new ReportService<Warehouse>(new JsonRepository<Warehouse>(), new XmlRepository<Warehouse>());
        }
    }
}
