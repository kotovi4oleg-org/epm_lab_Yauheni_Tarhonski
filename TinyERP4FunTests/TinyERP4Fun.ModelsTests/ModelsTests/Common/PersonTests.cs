using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.Models.Common;

namespace TinyERP4Fun.Tests.ModelsTests
{
    public class PersonTests
    {
        [Fact]
        public void LastName_And_Null_Sets_Same_FullName_And_Name()
        {


            // arrange
            var person = new Person();

            // act
            person.FirstName = "Test";
            var res = person.FullName;
            var res2 = person.Name;

            // assert
            Assert.Equal("Test", res);
            Assert.Equal("Test", res2);
        }
        [Fact]
        public void FirstName_And_Null_Sets_Same_FullName_And_Name()
        {
            // arrange
            var person = new Person();

            // act
            person.LastName = "Test";
            var res = person.FullName;
            var res2 = person.Name;

            // assert
            Assert.Equal("Test", res);
            Assert.Equal("Test", res2);
        }
        [Fact]
        public void FirstName_Are_Trimed()
        {
            // arrange
            var person = new Person();

            // act
            person.FirstName = " Test ";
            var res0 = person.FirstName;
            var res = person.FullName;
            var res2 = person.Name;

            // assert
            Assert.Equal("Test", res0);
            Assert.Equal("Test", res);
            Assert.Equal("Test", res2);
        }
        [Fact]
        public void LastName_Are_Trimed()
        {
            // arrange
            var person = new Person();

            // act
            person.LastName = " Test ";
            var res0 = person.LastName;
            var res = person.FullName;
            var res2 = person.Name;

            // assert
            Assert.Equal("Test", res0);
            Assert.Equal("Test", res);
            Assert.Equal("Test", res2);
        }

        [Fact]
        public void FullName_Are_Trimed()
        {
            // arrange
            Person person = new Person();

            // act
            person.FirstName = " First ";
            person.LastName = " Second ";
            var res = person.FullName;
            var res2 = person.Name;

            // assert
            Assert.Equal("First Second", res);
            Assert.Equal("First Second", res2);
        }

    }
}
