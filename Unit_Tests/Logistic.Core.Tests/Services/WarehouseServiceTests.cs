using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;
using Logistic.Models;
using Moq;
using Xunit;
using AutoFixture.Xunit2;

namespace Logistic.Core.Tests.Services
{
    public class WarehouseServiceTest
    {
        private readonly WarehouseService _warehouseService;
        private readonly Mock<IRepository<Warehouse>> _mock;

        public WarehouseServiceTest()
        {
            _mock = new Mock<IRepository<Warehouse>>();
            _warehouseService = new WarehouseService(_mock.Object);
        }

        [Theory, AutoData]
        public void Create_WhenExecuted_ShouldCallCreateMethod(Warehouse warehouse)
        {
            //Act
            _warehouseService.Create(warehouse);

            //Assert
            _mock.Verify(wr => wr.Create(warehouse), Times.Once);
        }

        [Theory, AutoData]
        public void GetById_WhenExecuted_ShouldCallReadMethod(int id)
        {
            //Act
            _warehouseService.GetById(id);

            //Assert
            _mock.Verify(wr => wr.Read(id), Times.Once);
        }

        [Fact]
        public void GetAll_WhenExecuted_ShouldCallReadAllMethod()
        {
            //Act
            _warehouseService.GetAll();

            //Assert
            _mock.Verify(wr => wr.ReadAll(), Times.Once);
        }

        [Theory, AutoData]
        public void DeleteById_WhenExecuted_ShouldCallDeleteMethod(int id)
        {
            //Act
            _warehouseService.DeleteById(id);

            //Assert
            _mock.Verify(wr => wr.Delete(id), Times.Once);
        }

        [Fact]
        public void DeleteAll_WhenExecuted_ShouldCallDeleteAllMethod()
        {
            //Act
            _warehouseService.DeleteAll();

            //Assert
            _mock.Verify(wr => wr.DeleteAll(), Times.Once);
        }

        [Theory, AutoData]
        public void LoadCargo_WhenExecuted_ShouldCallUpdateMethod(Warehouse warehouse, int id)
        {
            //Act
            _warehouseService.LoadCargo(warehouse, id);

            //Assert
            _mock.Verify(wr => wr.Update(warehouse, id), Times.Once);
        }

        [Theory, AutoData]
        public void UnLoadCargo_WhenExecuted_ShouldCallUpdateMethod(Warehouse warehouse, int id)
        {
            //Act
            _warehouseService.UnloadCargo(warehouse, id);

            //Assert
            _mock.Verify(wr => wr.Update(warehouse, id), Times.Once);
        }

        [Theory, AutoData]
        public void UnLoadAllCargos_WhenExecuted_ShouldCallUpdateMethod(Warehouse warehouse, int id)
        {
            //Act
            _warehouseService.UnloadAllCargos(warehouse, id);

            //Assert
            _mock.Verify(wr => wr.Update(warehouse, id), Times.Once);
        }
    }
}
