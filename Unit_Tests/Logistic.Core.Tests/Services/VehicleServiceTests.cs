using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Models;
using NSubstitute;
using AutoFixture;
using Xunit;

namespace Logistic.Core.Tests.Services
{
    public class VehicleServiceTests
    {
        private readonly VehicleService _vehicleService;
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly Vehicle _vehicle;

        public VehicleServiceTests()
        {
            //Arrange
            _vehicleRepository = Substitute.For<IRepository<Vehicle>>();
            _vehicleService = new VehicleService(_vehicleRepository);
            var fixture = new Fixture();
            _vehicle = fixture.Create<Vehicle>();
            _vehicle.MaxCargoVolume = 100;
            _vehicle.MaxCargoWeightKg = 100;
        }

        [Fact]
        public void Create_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _vehicleService.Create(_vehicle);

            //Assert
            _vehicleRepository.Received(1).Create(Arg.Any<Vehicle>());
        }

        [Fact]
        public void GetById_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _vehicleService.GetById(13);

            //Assert
            _vehicleRepository.Received(1).Read(Arg.Any<int>());
        }

        [Fact]
        public void GetAll_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _vehicleService.GetAll();

            //Assert
            _vehicleRepository.Received(1).ReadAll();
        }

        [Fact]
        public void DeleteById_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _vehicleService.DeleteById(1);

            //Assert
            _vehicleRepository.Received(1).Delete(Arg.Any<int>());
        }

        [Fact]
        public void DeleteAll_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _vehicleService.DeleteAll();

            //Assert
            _vehicleRepository.Received(1).DeleteAll();
        }

        [Fact]
        public void LoadCargo_WhenCargoMeetLimits_ExpectedCallMethodAndTrue()
        {
            //Arrange
            _vehicle.Cargos.Clear();
            _vehicle.Cargos.Add(new Cargo { Volume = 30, Weight = 30 });
            _vehicle.Cargos.Add(new Cargo { Volume = 30, Weight = 30 });

            //Act
            var result = _vehicleService.LoadCargo(_vehicle, 1);

            //Assert
            _vehicleRepository.Received(1).Update(Arg.Any<Vehicle>(), Arg.Any<int>());
            Assert.True(result);
        }

        [Fact]
        public void LoadCargo_WhenCargoNotMeetLimits_ExpectedCallMethodAndFalse()
        {
            //Arrange
            _vehicle.Cargos.Clear();
            _vehicle.Cargos.Add(new Cargo { Volume = 80, Weight = 80 });
            _vehicle.Cargos.Add(new Cargo { Volume = 30, Weight = 30 });

            //Act
            var result = _vehicleService.LoadCargo(_vehicle, 1);

            //Assert
            _vehicleRepository.Received(0).Update(Arg.Any<Vehicle>(), Arg.Any<int>());
            Assert.False(result);
        }

        [Fact]
        public void UnLoadCargo_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _vehicleService.UnloadCargo(_vehicle, 5);

            //Assert
            _vehicleRepository.Received(1).Update(Arg.Any<Vehicle>(), Arg.Any<int>());
        }

        [Fact]
        public void UnLoadAllCargos_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _vehicleService.UnloadAllCargos(_vehicle, 9);

            //Assert
            _vehicleRepository.Received(1).Update(Arg.Any<Vehicle>(), Arg.Any<int>());
        }
    }
}
