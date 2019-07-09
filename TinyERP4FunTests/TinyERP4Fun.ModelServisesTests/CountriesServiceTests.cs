using System;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyERP4Fun.ModelServises;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using TinyERP4Fun.Interfaces;
using Moq;
using System.Linq;

namespace TinyERP4FunTests.ModelServicesTests
{
    public class CountriesServiceTests
    {
        //public Contr ValidController { get; set; }
        //public Contr NotValidController { get; set; }
        public Mock<DefaultContext> Mock { get; set; }
        public Mock<DbSet<Country>> MockSet { get; set; }
        public Country singleEntity = new Country { Id = 2 };
        public readonly IQueryable<Country> testEntities =
            new Country[] {
                        new Country {Id=0},
                        new Country {Id=1},
                        new Country {Id=2},
                        new Country {Id=3},
                        new Country {Id=4}
                        }.AsQueryable();
        public CountriesServiceTests()
        {
            long Id = singleEntity.Id;
            var mockSet = Tests.SetUpMock.SetUpFor(testEntities);

            var options = new DbContextOptionsBuilder<DefaultContext>()
                                .UseInMemoryDatabase(databaseName: "TestData")
                                .Options;
            var mock = new Mock<DefaultContext>(options);
            mock.Setup(c => c.Set<Country>()).Returns(mockSet.Object);

            Mock = mock;
            MockSet = mockSet;
        }
        [Fact]
        public async Task TryToTestService()
        {
            //Assert

            CountriesService countriesServiceTests = new CountriesService(Mock.Object);

            //Act
            var result = await countriesServiceTests.GetAsync(2);

            //Assert
            Assert.Equal(result, testEntities.ToArray()[2]);
        }
    }
}
