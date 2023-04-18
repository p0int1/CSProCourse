using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Models;
using NSubstitute;
using Xunit;
using AutoFixture.Xunit2;

namespace Logistic.Core.Tests.Services
{
    public class VehicleServiceTests
    {
        private readonly VehicleService _vehicleService;
        private readonly IRepository<Vehicle> _vehicleRepository;

        public VehicleServiceTests()
        {
            _vehicleRepository = Substitute.For<IRepository<Vehicle>>();
            _vehicleService = new VehicleService(_vehicleRepository);
        }

        [Theory, AutoData]
        public void Create_WhenExecuted_ShouldCallCreateMethod(Vehicle vehicle)
        {
            //Act
            _vehicleService.Create(vehicle);

            //Assert
            _vehicleRepository.Received(1).Create(vehicle);
        }

        [Theory, AutoData]
        public void GetById_WhenExecuted_ShouldCallReadMethod(int id)
        {
            //Act
            _vehicleService.GetById(id);

            //Assert
            _vehicleRepository.Received(1).Read(id);
        }

        [Fact]
        public void GetAll_WhenExecuted_ShouldCallReadAllMethod()
        {
            //Act
            _vehicleService.GetAll();

            //Assert
            _vehicleRepository.Received(1).ReadAll();
        }

        [Theory, AutoData]
        public void DeleteById_WhenExecuted_ShouldCallDeleteMethod(int id)
        {
            //Act
            _vehicleService.DeleteById(id);

            //Assert
            _vehicleRepository.Received(1).Delete(id);
        }

        [Fact]
        public void DeleteAll_WhenExecuted_ShouldCallDeleteAllMethod()
        {
            //Act
            _vehicleService.DeleteAll();

            //Assert
            _vehicleRepository.Received(1).DeleteAll();
        }

        [Theory, AutoData]
        public void LoadCargo_WhenCargoMeetLimits_ShouldCallUpdateMethodAndReturnTrue(Vehicle vehicle, int id)
        {
            //Arrange
            vehicle.MaxCargoVolume = 100;
            vehicle.MaxCargoWeightKg = 100;
            vehicle.Cargos.Clear();
            vehicle.Cargos.Add(new Cargo { Volume = 30, Weight = 30 });
            vehicle.Cargos.Add(new Cargo { Volume = 30, Weight = 30 });

            //Act
            var result = _vehicleService.LoadCargo(vehicle, id);

            //Assert
            _vehicleRepository.Received(1).Update(vehicle, id);
            Assert.True(result);
        }

        [Theory, AutoData]
        public void LoadCargo_WhenCargoNotMeetLimits_ShouldNotCallUpdateMethodAndReturnFalse(Vehicle vehicle, int id)
        {
            //Arrange
            vehicle.MaxCargoVolume = 100;
            vehicle.MaxCargoWeightKg = 100;
            vehicle.Cargos.Clear();
            vehicle.Cargos.Add(new Cargo { Volume = 80, Weight = 80 });
            vehicle.Cargos.Add(new Cargo { Volume = 30, Weight = 30 });

            //Act
            var result = _vehicleService.LoadCargo(vehicle, id);

            //Assert
            _vehicleRepository.Received(0).Update(vehicle, id);
            Assert.False(result);
        }

        [Theory, AutoData]
        public void UnLoadCargo_WhenExecuted_ShouldCallUpdateMethod(Vehicle vehicle, int id)
        {
            //Act
            _vehicleService.UnloadCargo(vehicle, id);

            //Assert
            _vehicleRepository.Received(1).Update(vehicle, id);
        }

        [Theory, AutoData]
        public void UnLoadAllCargos_WhenExecuted_ShouldCallUpdateMethod(Vehicle vehicle, int id)
        {
            //Act
            _vehicleService.UnloadAllCargos(vehicle, id);

            //Assert
            _vehicleRepository.Received(1).Update(vehicle, id);
        }
    }
}
