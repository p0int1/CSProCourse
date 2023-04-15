using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Enums;
using Logistic.Models;
using NSubstitute;
using AutoFixture;
using Xunit;

namespace Logistic.Core.Tests.Services
{
    public class ReportServiceTest
    {
        private readonly ReportService<Vehicle> _reportService;
        private readonly IReportRepository<Vehicle> jsonRepository;
        private readonly IReportRepository<Vehicle> xmlRepository;
        private readonly List<Vehicle> _vehicleList;

        public ReportServiceTest()
        {
            jsonRepository = Substitute.For<IReportRepository<Vehicle>>();
            xmlRepository = Substitute.For<IReportRepository<Vehicle>>();
            _reportService = new ReportService<Vehicle>(jsonRepository, xmlRepository);
            var fixture = new Fixture();
            _vehicleList = fixture.Create<List<Vehicle>>();
        }

        [Theory]
        [InlineData(ReportType.json)]
        [InlineData(ReportType.xml)]
        public void CreateReport_WhenExecuted_ExpectedCallMethod(ReportType reportType)
        {
            //Act
            _reportService.CreateReport(_vehicleList, reportType);

            //Assert
            switch (reportType)
            {
                case ReportType.json:
                    jsonRepository.Received(1).Create(Arg.Any<List<Vehicle>>(), Arg.Any<string>());
                    break;
                case ReportType.xml:
                    xmlRepository.Received(1).Create(Arg.Any<List<Vehicle>>(), Arg.Any<string>());
                    break;
            }
        }

        [Theory]
        [InlineData("json")]
        [InlineData("xml")]
        public void LoadReport_WhenExecuted_ExpectedCallMethod(string fileType)
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
