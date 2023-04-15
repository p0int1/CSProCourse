using Logistic.ConsoleClient.Repositories;
using Logistic.Models;
using AutoFixture;
using Xunit;

namespace Logistic.DAL.Tests.Repositories
{
    public class InMemoryRepositoryTests
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly Fixture _fixture;

        public InMemoryRepositoryTests()
        {
            //Arrange
            _vehicleRepository = new InMemoryRepository<Vehicle>();
            _warehouseRepository = new InMemoryRepository<Warehouse>();
            _fixture = new Fixture();
        }

        [Fact]
        public void Create_WhenExecuted_ExpectedResult()
        {
            //Arrange
            _vehicleRepository.Create(_fixture.Create<Vehicle>());
            _warehouseRepository.Create(_fixture.Create<Warehouse>());

            //Act
            var result1 = _vehicleRepository.Read(1);
            var result2 = _warehouseRepository.Read(1);

            //Assert
            Assert.Equal(1, result1.Id);
            Assert.Equal(1, result2.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Read_WhenExecuted_ExpectedResultOrNull(int id)
        {
            //Arrange
            var vehicle = _fixture.Create<Vehicle>();
            _vehicleRepository.Create(vehicle);

            //Act
            var result = _vehicleRepository.Read(id);

            //Assert
            if (id == 1)
            {
                Assert.Equal(vehicle.MaxCargoVolume, result.MaxCargoVolume);
                Assert.Equal(vehicle.MaxCargoWeightKg, result.MaxCargoWeightKg);
                Assert.Equal(vehicle.Number, result.Number);
            }
            else
            {
                Assert.Null(result);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(30)]
        public void ReadAll_WhenExecuted_ExpectedResult(int numberOfTimes)
        {
            //Arrange
            for (int i = 0; i < numberOfTimes; i++)
            {
                _vehicleRepository.Create(_fixture.Create<Vehicle>());
            }

            //Act
            var result = _vehicleRepository.ReadAll();

            //Assert
            Assert.Equal(numberOfTimes, result.Count);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(15, 15)]
        public void Delete_WhenExecuted_ExpectedTrueOrFalse(int numberOfTimes, int id)
        {
            //Arrange
            for (int i = 0; i < numberOfTimes; i++)
            {
                _vehicleRepository.Create(_fixture.Create<Vehicle>());
            }

            //Act
            var result = _vehicleRepository.Delete(id);

            //Assert
            if (numberOfTimes >= id)
            {
                Assert.True(result);
            }
            else
            {
                Assert.False(result);
            }
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        public void DeleteAll_WhenExecuted_ExpectedResult(int numberOfTimes)
        {
            //Arrange
            for (int i = 0; i < numberOfTimes; i++)
            {
                _vehicleRepository.Create(_fixture.Create<Vehicle>());
            }

            //Act
            _vehicleRepository.DeleteAll();
            var result = _vehicleRepository.ReadAll();

            //Assert
            Assert.Empty(result);
        }

    }
}