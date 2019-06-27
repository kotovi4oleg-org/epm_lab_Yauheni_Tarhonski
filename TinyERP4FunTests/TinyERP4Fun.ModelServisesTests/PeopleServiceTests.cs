using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Stock;

using TinyERP4Fun.ModelServises;
using TinyERP4Fun.Interfaces;
using TinyERP4Fun.Models.Common;

namespace TinyERP4FunTests.ModelServicesTests
{
    public class PeopleServiceTests
    {
        [Fact]
        public void EmptyTest()
        {
            var person = new Person { FirstName = "Nadzeya", LastName = "Pus" };
        }


    }
}
