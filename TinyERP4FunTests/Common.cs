using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun;

namespace TinyERP4FunTests
{
    public class FakeRoleManager : RoleManager<IdentityRole>
    {
        public FakeRoleManager()
                : base(new Mock<IRoleStore<IdentityRole>>().Object,
                     new IRoleValidator<IdentityRole>[0],
                     new Mock<ILookupNormalizer>().Object,
                     new Mock<IdentityErrorDescriber>().Object,
                     new Mock<ILogger<RoleManager<IdentityRole>>>().Object)
        { }
    }
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

    internal static class DefaultContextMock 
    {
        public static Mock<IDefaultContext> GetMock()
        {
            return new Mock<IDefaultContext>();
        }
    }
    internal class MockingEntities<T, Contr, IServ> where T : class, IHaveLongId, new()
                                        where Contr : Controller
                                        where IServ : class, IBaseService<T>
    {
        public Contr ValidController { get; set; }
        public Contr NotValidController { get; set; }
        public Mock<IServ> Mock { get; set; }
        public Mock<DbSet<T>> MockSet { get; set; }
        public T singleEntity = new T { Id = 2};
        public readonly IQueryable<T> testEntities =
            new T[] {
                        new T {Id=0},
                        new T {Id=1},
                        new T {Id=2},
                        new T {Id=3},
                        new T {Id=4}
                        }.AsQueryable();
        public MockingEntities()
        {
            long Id = singleEntity.Id;
            var mockSet = SetUpMock.SetUpFor(testEntities);
            var mock = new Mock<IServ>();
            mock.Setup(c => c.GetIQueryable()).Returns(mockSet.Object);
            mock.Setup(c => c.GetListAsync()).Returns(Task.FromResult(testEntities.AsEnumerable()));
            mock.Setup(c => c.GetAsync(Id, It.IsAny<bool>()))
                .Returns(Task.FromResult(singleEntity));
            ValidController = (Contr)Activator.CreateInstance(typeof(Contr), new object[] { mock.Object });
            NotValidController = (Contr)Activator.CreateInstance(typeof(Contr), new object[] { mock.Object });
            NotValidController.ModelState.AddModelError("Name", "Some Error");
            Mock = mock;
            MockSet = mockSet;
        }
    }
    
    
    internal static class SetUpMock
    {
        public static Mock<DbSet<T>> SetUpFor<T>(IQueryable<T> entityList) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IAsyncEnumerable<T>>()
                    .Setup(m => m.GetEnumerator())
                    .Returns(new TestAsyncEnumerator<T>(entityList.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(entityList.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entityList.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entityList.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => entityList.GetEnumerator());
            return mockSet;
        }
    }
    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }
        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }
        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }
        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }
        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestAsyncQueryProvider<T>(this); }
        }
    }
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }
        public void Dispose()
        {
            _inner.Dispose();
        }
        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }
        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }
    }
    /**/
}
