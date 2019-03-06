using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Lib;
using System.Linq;
using Xunit;

namespace ClothingStore.Testing
{
    public class CustomerTests
    {
        // the test class is instantiated again for each test, so it is safe to put common setup code
        // in the class constructor or members. this will be a new store object for each test.
        // it's made readonly to satisfy static analysis warnings.
        readonly Customer customer = new Customer();

        [Fact]
        public void fName_NonEmptyValue_StoresCorrectly()
        {
            const string fName = "Sue";
            customer.FirstName = fName;
            Assert.Equal(fName, customer.FirstName);
        }

        [Fact]
        public void lName_NonEmptyValue_StoresCorrectly()
        {
            const string lName = "Sally";
            customer.LastName = lName;
            Assert.Equal(lName, customer.LastName);
        }

        [Fact]
        public void fName_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.FirstName = string.Empty);
        }

        [Fact]
        public void lName_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.LastName = string.Empty);
        }
    }
}
