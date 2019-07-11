using Xunit;
using System.Threading.Tasks;
using TinyERP4Fun.ModelServises;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;

using System.Threading;

namespace TinyERP4FunTests.ModelServicesTests
{
    public class CountriesServiceTests
    {
        public Mock<DefaultContext> Mock { get; set; }
        public Mock<DbSet<Country>> MockSet { get; set; }
        public Country singleEntity = new Country { Id = 2 };
        public Country newEntity = new Country { Id = 15 };
        public CountriesService testedService;
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
            Mock = DefaultContextMock.GetMock();
            MockSet = SetUpMock.SetUpFor(testEntities);
            Mock.Setup(c => c.Set<Country>()).Returns(MockSet.Object);
            testedService = new CountriesService(Mock.Object);
        }
        [Fact]
        public async Task GetAsync2_ReturnsRecordWithId2()
        {
            //Assert

            //Act
            var result = await testedService.GetAsync(2);

            //Assert
            Assert.Equal(testEntities.ToArray()[2], result);
        }
        [Fact]
        public async Task GetAsync5_ReturnsNull()
        {
            //Assert

            //Act
            var result = await testedService.GetAsync(5);

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
            Assert.Equal(testEntities.ToArray()[2], result);
        }
        [Fact]
        public async Task GetAsync5Tracking_ReturnsNull()
        {
            //Assert

            //Act
            var result = await testedService.GetAsync(5, true);

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
        public async Task DeleteAsync_RemovesEntity()
        {
            //Assert

            //Act
            await testedService.DeleteAsync(singleEntity.Id);

            //Assert
            Mock.Verify(c => c.Remove(It.IsAny<Country>()), Times.Once());
            Mock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            //Assert

            //Act
            await testedService.UpdateAsync(newEntity);

            //Assert
            Mock.Verify(c => c.Update(newEntity), Times.Once());
            Mock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
