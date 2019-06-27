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

namespace Tests.TinyERP4FunTests
{
    public class CountriesControllerTests
    {
        [Fact]
        public async Task IndexViewResultNotNull()
        {
            // Arrange
 
            var countriesList = GetTestCountries();

            var mockSet = SetUpMock.SetUpFor(GetTestCountries());
            var mock = new Mock<ICountriesService>();
            mock.Setup(c => c.GetIQueryable()).Returns(mockSet.Object);
            mock.Setup(c => c.GetListAsync()).Returns(Task.FromResult(GetTestCountries().AsEnumerable()));

            CountriesController controller = new CountriesController(mock.Object);
            // Act
            IActionResult result = await controller.Index(null);

            // Assert
            Assert.NotNull(result);


        }
        private IQueryable<Country> GetTestCountries()
        {
            Country[] result = { new Country { Name = "Netherlands" },
                new Country { Name = "Poland"},
                new Country { Name = "Belarus"},
                new Country { Name = "USA"},
                new Country { Name = "Finland"}
            };
            return result.AsQueryable();

        }

        
    }
}
