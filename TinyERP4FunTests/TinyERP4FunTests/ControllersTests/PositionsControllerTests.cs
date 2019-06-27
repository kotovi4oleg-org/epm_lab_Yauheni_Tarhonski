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
            var mock = new Mock<IPositionsService>();
            mock.Setup(repo => repo.GetListAsync()).Returns(Task.FromResult(GetTestPositions()));
            PositionsController controller = new PositionsController(mock.Object);

            // Act
            IActionResult result = await controller.Index();

            // Assert
            Assert.NotNull(result);
        }
        private IEnumerable<Position> GetTestPositions()
        {
            Position[] result = { new Position { Name = "Position1" },
                new Position { Name = "Position2" },
                new Position { Name = "Position3" },
                new Position { Name = "Position4" },
                new Position { Name = "Position5" }
            };
            return result;
        }
    }
}
