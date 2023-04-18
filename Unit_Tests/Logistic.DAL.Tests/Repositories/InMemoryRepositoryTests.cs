using Logistic.ConsoleClient.Repositories;
using Logistic.Models;
using AutoFixture;
using Xunit;
using AutoFixture.Xunit2;

namespace Logistic.DAL.Tests.Repositories
{
    public class InMemoryRepositoryTests
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public InMemoryRepositoryTests()
        {
            _vehicleRepository = new InMemoryRepository<Vehicle>();
        }

        [Theory, AutoData]
        public void Create_WhenExecuted_ExpectedResult(Vehicle vehicle)
        {
            //Arrange
            _vehicleRepository.Create(vehicle);
            int idCreatedVehicle = 1;

            //Act
            var result = _vehicleRepository.Read(idCreatedVehicle);

            //Assert
            Assert.Equal(1, result.Id);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Read_WhenExecuted_ExpectedResultOrNull(int id)
        {
            //Arrange
            var vehicle = new Fixture().Create<Vehicle>();
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
        public void ReadAll_WhenExecuted_ExpectedResult(int numberOfEntitiesCreated)
        {
            //Arrange
            for (int i = 0; i < numberOfEntitiesCreated; i++)
            {
                _vehicleRepository.Create(new Fixture().Create<Vehicle>());
            }

            //Act
            var result = _vehicleRepository.ReadAll();

            //Assert
            Assert.Equal(numberOfEntitiesCreated, result.Count);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(15, 15)]
        public void Delete_WhenExecuted_ExpectedTrueOrFalse(int numberOfEntitiesCreated, int id)
        {
            //Arrange
            for (int i = 0; i < numberOfEntitiesCreated; i++)
            {
                _vehicleRepository.Create(new Fixture().Create<Vehicle>());
            }

            //Act
            var result = _vehicleRepository.Delete(id);

            //Assert
            if (numberOfEntitiesCreated >= id)
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
        public void DeleteAll_WhenExecuted_ExpectedResult(int numberOfEntitiesCreated)
        {
            //Arrange
            for (int i = 0; i < numberOfEntitiesCreated; i++)
            {
                _vehicleRepository.Create(new Fixture().Create<Vehicle>());
            }

            //Act
            _vehicleRepository.DeleteAll();
            var result = _vehicleRepository.ReadAll();

            //Assert
            Assert.Empty(result);
        }

    }
}