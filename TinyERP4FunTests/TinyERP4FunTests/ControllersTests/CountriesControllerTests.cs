using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyERP4Fun.Controllers;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Common;
using Xunit;
using Tests;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Models;
using TinyERP4Fun.Data;

namespace Tests.TinyERP4FunTests
{
    public class CountriesControllerTests
    {
        readonly Type indexResultType = typeof(PaginatedList<Country>);
        readonly string indexActionName = "Index";
        private Country GetSingleEntity()
        {
            return new Country { Name = "Belarus" };
        }
        private IQueryable<Country> GetTestEntities()
        {
            Country[] result = { new Country { Name = "Netherlands" },
                new Country { Name = "Poland"},
                new Country { Name = "Belarus"},
                new Country { Name = "USA"},
                new Country { Name = "Finland"}
            };
            return result.AsQueryable();
        }

        internal Mock<ICountriesService> GetMock(out CountriesController controller)
        {
            var mockSet = SetUpMock.SetUpFor(GetTestEntities());
            var mock = new Mock<ICountriesService>();
            mock.Setup(c => c.GetIQueryable()).Returns(mockSet.Object);
            mock.Setup(c => c.GetListAsync()).Returns(Task.FromResult(GetTestEntities().AsEnumerable()));
            controller = new CountriesController(mock.Object);
            return mock;
        }


        [Fact]
        public async Task IndexView_ResultNotNull()
        {
            // Arrange
            GetMock(out var controller);

            // Act
            IActionResult result = await controller.Index(null);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task IndexView_ReturnsRightModel()
        {
            // Arrange
            GetMock(out var controller);

            // Act
            var result = (ViewResult) await controller.Index(null);

            // Assert
            Assert.Equal(indexResultType, result.Model.GetType());
        }
        [Fact]
        public async Task CreatePostAction_ModelError_ReturnsSameModel()
        {
            // Arrange
            GetMock(out var controller);
            var entity = GetSingleEntity();
            controller.ModelState.AddModelError("Name", "Название модели не установлено");

            // Act
            ViewResult result = await controller.Create(entity) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity, result.Model);
        }
        [Fact]
        public async Task CreatePostAction_AddModelInService()
        {
            // Arrange
            var mock = GetMock(out var controller);
            var entity = GetSingleEntity();

            // Act
            var result = await controller.Create(entity); 

            // Assert
            mock.Verify(a => a.AddAsync(entity));
        }
        [Fact]
        public async Task CreatePostAction_ModelOk_RedirectsToIndex()
        {
            // Arrange
            GetMock(out var controller);
            var entity = GetSingleEntity();

            // Act
            var result = await controller.Create(entity) as RedirectToActionResult;

            // Assert
            Assert.Equal(indexActionName, result.ActionName);
        }
       
    }
}
