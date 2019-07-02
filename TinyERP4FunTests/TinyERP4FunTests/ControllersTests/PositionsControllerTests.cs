using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace Tests.TinyERP4FunTests
{
    public class PositionsControllerTests
    {
        [Fact]
        public async Task IndexViewResultNotNull()
        {
            // Arrange
            var mockSet = SetUpMock.SetUpFor(GetTestEntities());
            var mock = new Mock<IPositionsService>();
            mock.Setup(c => c.GetIQueryable()).Returns(mockSet.Object);
            mock.Setup(c => c.GetListAsync()).Returns(Task.FromResult(GetTestEntities().AsEnumerable()));
            PositionsController controller = new PositionsController(mock.Object);

            // Act
            IActionResult result = await controller.Index();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task IndexReturnsRightModel()
        {
            // Arrange
            var mockSet = SetUpMock.SetUpFor(GetTestEntities());
            var mock = new Mock<IPositionsService>();
            mock.Setup(c => c.GetIQueryable()).Returns(mockSet.Object);
            mock.Setup(c => c.GetListAsync()).Returns(Task.FromResult(GetTestEntities().AsEnumerable()));
            PositionsController controller = new PositionsController(mock.Object);

            // Act
            var result = (ViewResult)await controller.Index();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Position>>(result.Model);
        }
        private IQueryable<Position> GetTestEntities()
        {
            Position[] result = { new Position { Name = "Position1" },
                new Position { Name = "Position2" },
                new Position { Name = "Position3" },
                new Position { Name = "Position4" },
                new Position { Name = "Position5" }
            };
            return result.AsQueryable();
        }
    }
}
