using Xunit;
using System.Threading.Tasks;
using TinyERP4Fun.ModelServises;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Stock;
using System;

namespace TinyERP4Fun.Tests.Services.StockTests
{
    public class StockServiceTests
    {
        public Mock<IDefaultContext> Mock { get; set; }
        public Mock<DbSet<Stock>> MockSet { get; set; }

        public Stock singleEntity10 = new Stock { Id = 1, Quantity = 1, ItemId = 1, WarehouseId = 1, OperDate = DateTime.Parse("2019-07-10T00:00:00") };
        public Stock singleEntity11 = new Stock { Id = 1, Quantity = -13, ItemId = 1, WarehouseId = 1, OperDate = DateTime.Parse("2019-07-10T00:00:00") };
        

        public Stock singleEntity20 = new Stock { Id = 2, Quantity = 1, ItemId = 1, WarehouseId = 1, OperDate = DateTime.Parse("2019-07-11T00:00:00") };
        public Stock singleEntity21 = new Stock { Id = 2, Quantity = -13, ItemId = 1, WarehouseId = 1, OperDate = DateTime.Parse("2019-07-11T00:00:00") };

        public Stock singleEntity30 = new Stock { Id = 2, Quantity = -2, ItemId = 1, WarehouseId = 1, OperDate = DateTime.Parse("2019-07-11T00:00:00") };

        public Stock singleEntity40 = new Stock { Id = 4, Quantity = 13, ItemId = 1, WarehouseId = 1, OperDate = DateTime.Parse("2019-07-11T00:00:00") };
        public Stock singleEntity41 = new Stock { Id = 4, Quantity = 13, ItemId = 2, WarehouseId = 2, OperDate = DateTime.Parse("2019-07-11T00:00:00") };

        public Stock newEntity = new Stock { Id = 15, Quantity = 1, ItemId = 1, WarehouseId = 1, OperDate = DateTime.Parse("2019-07-11T00:00:00") };
        public Stock anyEntity = It.IsAny<Stock>();
        public StockService testedService;
        public readonly IQueryable<Stock> testEntities =
            new Stock[] {
                        new Stock {Id=1, Quantity=1, ItemId=1, WarehouseId=1, OperDate=DateTime.Parse("2019-07-10T00:00:00")},
                        new Stock {Id=2, Quantity=1, ItemId=1, WarehouseId=1, OperDate=DateTime.Parse("2019-07-11T00:00:00")},
                        new Stock {Id=3, Quantity=-2, ItemId=1, WarehouseId=1, OperDate=DateTime.Parse("2019-07-12T00:00:00")},
                        new Stock {Id=4, Quantity=13, ItemId=1, WarehouseId=1, OperDate=DateTime.Parse("2019-07-13T00:00:00")},
                        new Stock {Id=5, Quantity=-10, ItemId=1, WarehouseId=1, OperDate=DateTime.Parse("2019-07-14T00:00:00")}
                        }.AsQueryable();
        public StockServiceTests()
        {

            Mock = DefaultContextMock.GetMock();
            MockSet = SetUpMock.SetUpFor(testEntities);
            Mock.Setup(c => c.Set<Stock>()).Returns(MockSet.Object);
            Mock.Setup(c => c.Stock).Returns(MockSet.Object);
            testedService = new StockService(Mock.Object);
        }
        [Fact]
        public async Task GetAsync2_ReturnsRecordWithId2()
        {
            //Assert

            //Act
            var result = await testedService.GetAsync(2);

            //Assert
            Assert.Equal(testEntities.ToArray()[1], result);
        }
        [Fact]
        public async Task GetAsync51_ReturnsNull()
        {
            //Assert

            //Act
            var result = await testedService.GetAsync(51);

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetAsync2Tracking_ReturnsRecordWithId2()
        {
            //Assert

            //Act
            var result = await testedService.GetAsync(2, true);
            //Assert
            Assert.Equal(testEntities.ToArray()[1], result);
        }
        [Fact]
        public async Task GetAsync51Tracking_ReturnsNull()
        {
            //Assert

            //Act
            var result = await testedService.GetAsync(51, true);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetIQueryable_ReturnsRightValues()
        {
            //Assert

            //Act
            var result = testedService.GetIQueryable();

            //Assert
            Assert.Equal(testEntities.ToList(), result.ToList());
        }
        
        [Fact]
        public void GetFiltredContent_ReturnsRightValues()
        {
            //Assert

            //Act
            var result = testedService.GetFiltredContent(null, 
                                                         null, 
                                                         new long?[] { }, 
                                                         new long?[] { }, 
                                                         new string[] { });

            //Assert
            Assert.Equal(testEntities.ToList(), result.ToList());
        }
        [Fact]
        public async Task GetListAsync_ReturnsRightValues()
        {
            //Assert

            //Act
            var result = await testedService.GetListAsync();

            //Assert
            Assert.Equal(testEntities.ToList(), result);
        }
        [Fact]
        public async Task AddAsync_AddsEntity()
        {
            //Assert

            //Act
            await testedService.AddAsync(newEntity);

            //Assert
            Mock.Verify(c => c.Add(newEntity), Times.Once());
            Mock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task DeleteAsync_ThrowsException_on_FirstRecord()
        {
            //Assert
            var currentEntity = singleEntity10;
            Mock.Setup(c => c.Stock.FindAsync(It.IsAny<long>())).Returns(Task.FromResult(currentEntity));


            //Act
            

            //Assert
            var result = await Assert.ThrowsAsync<ArgumentException>(() => testedService.DeleteAsync(currentEntity.Id));
            Assert.Contains("st02", result.Message);
            Assert.Contains("Wrong balance", result.Message);
        }
        [Fact]
        public async Task DeleteAsync_ThrowsException_on_SecondRecord()
        {
            //Assert
            var currentEntity = singleEntity20;
            Mock.Setup(c => c.Stock.FindAsync(It.IsAny<long>())).Returns(Task.FromResult(currentEntity));


            //Act


            //Assert
            var result = await Assert.ThrowsAsync<ArgumentException>(() => testedService.DeleteAsync(currentEntity.Id));
            Assert.Contains("st02", result.Message);
            Assert.Contains("Wrong balance", result.Message);
        }
        [Fact]
        public async Task DeleteAsync_ThrowsException_on_FourthRecord()
        {
            //Assert
            var currentEntity = singleEntity40;
            Mock.Setup(c => c.Stock.FindAsync(It.IsAny<long>())).Returns(Task.FromResult(currentEntity));


            //Act


            //Assert
            var result = await Assert.ThrowsAsync<ArgumentException>(() => testedService.DeleteAsync(currentEntity.Id));
            Assert.Contains("st02", result.Message);
            Assert.Contains("Wrong balance", result.Message);
        }
        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            //Assert
            var currentEntity = singleEntity30;
            Mock.Setup(c => c.Stock.FindAsync(It.IsAny<long>())).Returns(Task.FromResult(currentEntity));


            //Act
            await testedService.DeleteAsync(currentEntity.Id);

            //Assert
            Mock.Verify(c => c.Remove(currentEntity), Times.Once());
            Mock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            //Assert
            var currentEntity = singleEntity10;
            

            //Act
            await testedService.UpdateAsync(currentEntity);

            //Assert
            Mock.Verify(c => c.Update(currentEntity), Times.Once());
            Mock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task UpdateAsync_UpdatesEntity2()
        {
            //Assert
            var currentEntity = singleEntity11;


            //Act

            //Assert
            var result = await Assert.ThrowsAsync<ArgumentException>(() => testedService.UpdateAsync(currentEntity));
            Assert.Contains("st01", result.Message);
            Assert.Contains("Wrong balance", result.Message);
        }
        [Fact]
        public async Task UpdateAsync_UpdatesEntity3()
        {
            //Assert
            var currentEntity = singleEntity21;


            //Act


            //Assert
            var result = await Assert.ThrowsAsync<ArgumentException>(() => testedService.UpdateAsync(currentEntity));
            Assert.Contains("st01", result.Message);
            Assert.Contains("Wrong balance", result.Message);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntity4()
        {
            //Assert
            var currentEntity = singleEntity40;


            //Act
            await testedService.UpdateAsync(currentEntity);

            //Assert
            Mock.Verify(c => c.Update(currentEntity), Times.Once());
            Mock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task UpdateAsync_UpdatesEntity5()
        {
            //Assert
            var currentEntity = singleEntity41;


            //Act

            //Assert
            var result = await Assert.ThrowsAsync<ArgumentException>(() => testedService.UpdateAsync(currentEntity));
            Assert.Contains("st02", result.Message);
            Assert.Contains("Wrong balance", result.Message);
        }

    }
}
