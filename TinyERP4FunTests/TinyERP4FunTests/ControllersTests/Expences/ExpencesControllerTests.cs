using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Controllers;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Common;
using Xunit;
using TinyERP4Fun;
using TinyERP4Fun.Models;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace Tests.TinyERP4FunTests.ExpencesTests
{
    public class FakeSignInManager : SignInManager<IdentityUser>
    {
        public FakeSignInManager()
                : base(new Mock<FakeUserManager>().Object,
                     new Mock<IHttpContextAccessor>().Object,
                     new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                     new Mock<IOptions<IdentityOptions>>().Object,
                     new Mock<ILogger<SignInManager<IdentityUser>>>().Object,
                     new Mock<IAuthenticationSchemeProvider>().Object)
        { }
    }
    public class FakeUserManager : UserManager<IdentityUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<IdentityUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<IdentityUser>>().Object,
              new IUserValidator<IdentityUser>[0],
              new IPasswordValidator<IdentityUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { }

        public override Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }

        public override Task<IdentityUser> GetUserAsync(ClaimsPrincipal user)
        {
            return Task.FromResult<IdentityUser>(null);
        }
        public override Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return Task.FromResult((IList<string>)Constants.rolesExpences_Admin.Split(','));
        }
        public override string GetUserId(ClaimsPrincipal user)
        {
            return null;
        }
        


    }
    internal class EntitiesMock
    {
        public ExpencesController ValidController { get; set; }
        public ExpencesController NotValidController { get; set; }
        public Mock<IExpencesService> Mock { get; set; }
        public readonly Expences singleEntity  
            = new Expences { Id = 2};
        public readonly IQueryable<Expences> testEntities =
            new Expences[] {
                        new Expences {Id=0},
                        new Expences {Id=1},
                        new Expences {Id=2},
                        new Expences {Id=3},
                        new Expences {Id=4}
                        }.AsQueryable();
        public EntitiesMock()
        {
            long Id = singleEntity.Id;
            var mockSet = SetUpMock.SetUpFor(testEntities);
            var mock = new Mock<IExpencesService>();
            mock.Setup(c => c.GetIQueryable()).Returns(mockSet.Object);
            mock.Setup(c => c.GetFilteredContentAsync(It.IsAny<int?>(), 
                                                      It.IsAny<ExpencesFiltredModel>(), 
                                                      It.IsAny<string>(), 
                                                      It.IsAny<bool>()))
                .Returns(Task.FromResult(new ExpencesFiltredModel()));
            mock.Setup(c => c.GetListAsync()).Returns(Task.FromResult(testEntities.AsEnumerable()));
            mock.Setup(c => c.GetAsync(Id, It.IsAny<bool>()))
                .Returns(Task.FromResult(singleEntity));
            ValidController = new ExpencesController(new FakeUserManager(), mock.Object);
            NotValidController = new ExpencesController(new FakeUserManager(), mock.Object);
            NotValidController.ModelState.AddModelError("Name", "Some Error");
            Mock = mock;
        }
    }
    public class ExpencesControllerTests
    {
        readonly Mock<IExpencesService> mock;
        readonly ExpencesController validController;
        readonly ExpencesController notValidController;
        readonly Expences entity;
        readonly Type indexResultType = typeof(ExpencesFiltredModel);
        readonly string indexActionName = "Index";

        public ExpencesControllerTests()
        {
            var mockingEntities = new EntitiesMock();
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
            IActionResult result =  await validController.Index(null, new ExpencesFiltredModel());

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task IndexView_ReturnsRightModel()
        {
            // Arrange

            // Act
            var result = (ViewResult) await validController.Index(null, new ExpencesFiltredModel());

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
