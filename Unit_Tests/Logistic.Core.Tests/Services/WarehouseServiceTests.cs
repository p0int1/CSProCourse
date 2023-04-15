using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Models;
using Moq;
using AutoFixture;
using Xunit;

namespace Logistic.Core.Tests.Services
{
    public class WarehouseServiceTest
    {
        private readonly WarehouseService _warehouseService;
        private readonly Mock<IRepository<Warehouse>> _mock;
        private readonly Warehouse _warehouse;

        public WarehouseServiceTest()
        {
            //Arrange
            _mock = new Mock<IRepository<Warehouse>>();
            _warehouseService = new WarehouseService(_mock.Object);
            var fixture = new Fixture();
            _warehouse = fixture.Create<Warehouse>();
        }

        [Fact]
        public void Create_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.Create(_warehouse);

            //Assert
            _mock.Verify(wr => wr.Create(It.IsAny<Warehouse>()), Times.Once);
        }

        [Fact]
        public void GetById_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.GetById(13);

            //Assert
            _mock.Verify(wr => wr.Read(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GetAll_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.GetAll();

            //Assert
            _mock.Verify(wr => wr.ReadAll(), Times.Once);
        }

        [Fact]
        public void DeleteById_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.DeleteById(1);

            //Assert
            _mock.Verify(wr => wr.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void DeleteAll_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.DeleteAll();

            //Assert
            _mock.Verify(wr => wr.DeleteAll(), Times.Once);
        }

        [Fact]
        public void LoadCargo_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.LoadCargo(_warehouse, 1);

            //Assert
            _mock.Verify(wr => wr.Update(It.IsAny<Warehouse>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void UnLoadCargo_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.UnloadCargo(_warehouse, 5);

            //Assert
            _mock.Verify(wr => wr.Update(It.IsAny<Warehouse>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void UnLoadAllCargos_WhenExecuted_ExpectedCallMethod()
        {
            //Act
            _warehouseService.UnloadAllCargos(_warehouse, 9);

            //Assert
            _mock.Verify(wr => wr.Update(It.IsAny<Warehouse>(), It.IsAny<int>()), Times.Once);
        }
    }
}
