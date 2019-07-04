using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Controllers;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Common;
using Xunit;
using TinyERP4Fun.Models;

namespace Tests.TinyERP4FunTests.CountriesControllerTests
{
    internal static class EntitiesMock
    {
        public static CountriesController ValidController { get; set; }
        public static CountriesController NotValidController { get; set; }
        public static Mock<ICountriesService> Mock { get; set; }
        public static readonly Country singleEntity = new Country { Id = 2, Name = "Name2" };
        public static readonly IQueryable<Country> testEntities =
            new Country[] {
                        new Country {Id=0, Name = "Name0" },
                        new Country {Id=1, Name = "Name1"},
                        new Country {Id=2, Name = "Name2"},
                        new Country {Id=3, Name = "Name3"},
                        new Country {Id=4, Name = "Name4"}
                        }.AsQueryable();
        static EntitiesMock()
        {
            long Id = singleEntity.Id;
            var mockSet = SetUpMock.SetUpFor(testEntities);
            var mock = new Mock<ICountriesService>();
            mock.Setup(c => c.GetIQueryable()).Returns(mockSet.Object);
            mock.Setup(c => c.GetListAsync()).Returns(Task.FromResult(testEntities.AsEnumerable()));
            mock.Setup(c => c.GetAsync(Id, It.IsAny<bool>()))
                .Returns(Task.FromResult(singleEntity));
            ValidController = new CountriesController(mock.Object);
            NotValidController = new CountriesController(mock.Object);
            NotValidController = (CountriesController)Activator.CreateInstance(typeof(CountriesController), new object[] { mock.Object });
            NotValidController.ModelState.AddModelError("Name", "Some Error");
            Mock = mock;
        }
    }
    public class CountriesControllerTests
    {
        readonly Mock<ICountriesService> mock;
        readonly CountriesController validController;
        readonly CountriesController notValidController;
        readonly Country entity;
        readonly Type indexResultType = typeof(PaginatedList<Country>);
        readonly string indexActionName = "Index";

        public CountriesControllerTests()
        {
            mock = EntitiesMock.Mock;
            validController = EntitiesMock.ValidController;
            notValidController = EntitiesMock.NotValidController;
            entity = EntitiesMock.singleEntity;
        }

        [Fact]
        public async Task IndexView_ResultNotNull()
        {
            // Arrange
            
            // Act
            IActionResult result = await validController.Index(null);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task IndexView_ReturnsRightModel()
        {
            // Arrange

            // Act
            var result = (ViewResult) await validController.Index(null);

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
