using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Enums;
using Logistic.Models;
using NSubstitute;
using Xunit;
using AutoFixture.Xunit2;

namespace Logistic.Core.Tests.Services
{
    public class ReportServiceTest
    {
        private readonly ReportService<Vehicle> _reportService;
        private readonly IReportRepository<Vehicle> jsonRepository;
        private readonly IReportRepository<Vehicle> xmlRepository;

        public ReportServiceTest()
        {
            jsonRepository = Substitute.For<IReportRepository<Vehicle>>();
            xmlRepository = Substitute.For<IReportRepository<Vehicle>>();
            _reportService = new ReportService<Vehicle>(jsonRepository, xmlRepository);
        }

        [Theory, AutoData]
        public void CreateReport_WhenExecuted_ShouldCallCreateMethod(List<Vehicle> vehicleList, ReportType reportType)
        {
            //Act
            _reportService.CreateReport(vehicleList, reportType);

            //Assert
            switch (reportType)
            {
                case ReportType.json:
                    jsonRepository.Received(1).Create(vehicleList, Arg.Any<string>());
                    break;
                case ReportType.xml:
                    xmlRepository.Received(1).Create(vehicleList, Arg.Any<string>());
                    break;
            }
        }

        [Theory]
        [InlineData("json")]
        [InlineData("xml")]
        public void LoadReport_WhenExecuted_ShouldCallReadMethod(string fileType)
        {
            //Act
            _reportService.LoadReport(fileType);

            //Assert
            switch (fileType)
            {
                case "json":
                    jsonRepository.Received(1).Read(Arg.Any<string>());
                    break;
                case "xml":
                    xmlRepository.Received(1).Read(Arg.Any<string>());
                    break;
            }
        }
    }
}
