/**/
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Controllers;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Common;
using Xunit;
using TinyERP4Fun.Models.Stock;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using TinyERP4Fun.ViewModels;
using TinyERP4Fun.Models;
using Microsoft.EntityFrameworkCore;

namespace TinyERP4Fun.Tests.Controllers.StockTests
{
    public class StockControllerTests
    {
        readonly Mock<IStockService> mock;
        readonly Mock<DbSet<Stock>> mockSet;
        readonly StockController validController;
        readonly StockController notValidController;
        readonly Stock entity;
        readonly Type indexResultType = typeof(StockPaginatedViewModel);
        readonly string indexActionName = "Index";
        
        public readonly IQueryable<Stock> testEntities =
            new Stock[] {
                        new Stock { Id = 5,
                            Item =new Item(){ Id = 1 },
                            ItemId = 1,
                            Quantity = 1,
                            Warehouse =new Warehouse(){ Id = 2 },
                            WarehouseId = 2}
                }.AsQueryable();

        public StockControllerTests()
        {
            var mockingEntities = new MockingEntities<Stock,
                                           StockController,
                                           IStockService>();
            mock = mockingEntities.Mock;
            mockSet = mockingEntities.MockSet;
            mock.Setup(c => c.GetFiltredContent(It.IsAny<DateTime?>(),
                                                It.IsAny<DateTime?>(),
                                                It.IsAny<IEnumerable<long?>>(),
                                                It.IsAny<IEnumerable<long?>>(),
                                                It.IsAny<IEnumerable<string>>())).Returns(mockSet.Object);
            validController = mockingEntities.ValidController;
            notValidController = mockingEntities.NotValidController;
            entity = mockingEntities.singleEntity;
        }

        [Fact]
        public async Task IndexView_ResultNotNull()
        {
            // Arrange

            // Act
            IActionResult result = await validController.Index(null,null,null,null,null);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task IndexView_ReturnsRightModel()
        {
            // Arrange

            // Act
            var result = (ViewResult)await validController.Index(null, null, null, null, null);

            // Assert
            Assert.Equal(indexResultType, result.Model.GetType());
        }
        [Fact]
        public async Task CreatePostAction_ModelError_ReturnsSameModel()
        {
            // Arrange


            // Act
            var result = await notValidController.Create(entity) as ViewResult;

            // Assert
            Assert.Equal(entity, result.Model);
        }
        [Fact]
        public async Task CreatePostAction_AddModelToService()
        {
            // Arrange

            // Act
            var result = await validController.Create(entity);

            // Assert
            mock.Verify(a => a.AddAsync(entity));
        }
        [Fact]
        public async Task CreatePostAction_ModelOk_RedirectsToIndex()
        {
            // Arrange

            // Act
            var result = await validController.Create(entity) as RedirectToActionResult;

            // Assert
            Assert.Equal(indexActionName, result.ActionName);
        }
        [Fact]
        public async Task EditGetAction_UpdateModelFromService()
        {
            // Arrange

            // Act
            var result = await validController.Edit(entity.Id);

            // Assert
            mock.Verify(a => a.GetAsync(entity.Id, true));
        }
        
        [Fact]
        public async Task EditPostAction_ModelError_ReturnsSameModel()
        {
            // Arrange

            // Act
            var result = await notValidController.Edit(entity.Id, entity) as ViewResult;

            // Assert
            Assert.Equal(entity, result.Model);
        }
        [Fact]
        public async Task EditPostAction_UpdateModelFromService()
        {
            // Arrange

            // Act
            var result = await validController.Edit(entity.Id, entity);

            // Assert
            mock.Verify(a => a.UpdateAsync(entity));
        }
        
        [Fact]
        public async Task DeleteGetActionWithId2_ReturnsModelFromService()
        {
            // Arrange

            // Act
            var result = await validController.Delete(entity.Id) as ViewResult;

            // Assert
            Assert.Equal(entity, result.Model);
        }
        [Fact]
        public async Task DeleteGetAction_GetModelFromService()
        {
            // Arrange

            // Act
            var result = await validController.Delete(entity.Id);

            // Assert
            mock.Verify(a => a.GetAsync(entity.Id, false));
        }
        [Fact]
        public async Task DeletePostAction_DeleteModelFromService()
        {
            // Arrange

            // Act
            var result = await validController.DeleteConfirmed(entity.Id);

            // Assert
            mock.Verify(a => a.DeleteAsync(entity.Id));
        }
        [Fact]
        public async Task DetailsGetAction_GetModelFromService()
        {
            // Arrange

            // Act
            var result = await validController.Details(entity.Id);

            // Assert
            mock.Verify(a => a.GetAsync(entity.Id, false));
        }
        [Fact]
        public async Task DetailsGetActionWithId2_ReturnsModelFromService()
        {
            // Arrange

            // Act
            var result = await validController.Details(entity.Id) as ViewResult;

            // Assert
            Assert.Equal(entity, result.Model);
        }

    }
}
/**/