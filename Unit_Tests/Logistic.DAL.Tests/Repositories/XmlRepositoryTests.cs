using Logistic.ConsoleClient.Repositories;
using Logistic.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Logistics.DAL.Tests.Repositories
{
    public class XmlRepositoryTests : IDisposable
    {
        private readonly XmlRepository<Vehicle> _xmlRepository;
        private readonly List<Vehicle> _vehicleList;
        public XmlRepositoryTests()
        {
            _xmlRepository = new XmlRepository<Vehicle>();
            _vehicleList = new List<Vehicle>() { new(), new(), new() };
        }

        [Fact]
        public void Create_WhenValidEntity_SerializeSuccessful()
        {
            //Arrange
            var testPath = Path.Combine("Resources", "write_Vehicle_test.xml");

            //Act
            _xmlRepository.Create(_vehicleList, testPath);
            var result = _xmlRepository.Read(testPath);

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeEquivalentTo(_vehicleList);
            }
        }

        [Fact]
        public void Read_WhenValidXml_DeserializeSuccessful()
        {
            //Arrange
            var testPath = Path.Combine("Resources", "read_Vehicle_test.xml");

            //Act
            var result = _xmlRepository.Read(testPath);

            //Assert
            using (new AssertionScope())
            {
                result.Should().HaveCount(3);
                result[0].Number.Should().Be("LW7437XL");
                result[0].MaxCargoWeightKg.Should().Be(776);
                result[1].Number.Should().Be("VL7754NS");
                result[1].MaxCargoWeightKg.Should().Be(196);
                result[2].Number.Should().Be("RX6661HS");
                result[2].MaxCargoWeightKg.Should().Be(7287);
            }
        }

        public void Dispose()
        {
            Directory.Delete("Resources", true);
        }
    }
}
