﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Controllers;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Common;
using Xunit;
using TinyERP4Fun.Models;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Models.Expenses;

namespace Tests.TinyERP4FunTests.Expences
{
    public class BusinessDirectionsControllerTests
    {
        readonly Mock<IBusinessDirectionsService> mock;
        readonly BusinessDirectionsController validController;
        readonly BusinessDirectionsController notValidController;
        readonly BusinessDirection entity;
        readonly Type indexResultType = typeof(EnumerableQuery<BusinessDirection>);
        readonly string indexActionName = "Index";

        public BusinessDirectionsControllerTests()
        {
            var mockingEntities = new MockingEntities<BusinessDirection,
                                           BusinessDirectionsController,
                                           IBusinessDirectionsService>();
            mock = mockingEntities.Mock;
            validController = mockingEntities.ValidController;
            notValidController = mockingEntities.NotValidController;
            entity = mockingEntities.singleEntity;
        }

        [Fact]
        public async Task IndexView_ResultNotNull()
        {
            // Arrange
            
            // Act
            IActionResult result =  await validController.Index();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task IndexView_ReturnsRightModel()
        {
            // Arrange

            // Act
            var result = (ViewResult) await validController.Index();

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
