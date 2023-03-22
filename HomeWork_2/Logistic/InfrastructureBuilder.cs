using Logistic.ConsoleClient.Services;

namespace Logistic.ConsoleClient
{
    public static class InfrastructureBuilder
    {
        public static VehicleService vehicleService;
        public static WarehouseService warehouseService;
        public static ReportService reportService;

        public static void Builder()
        {
            vehicleService = new VehicleService();
            warehouseService = new WarehouseService();
            reportService = new ReportService();
        }
    }
}
