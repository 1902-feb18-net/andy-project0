using ClothingStore.Lib;
using System;
using System.Linq;
using Xunit;

namespace ClothingStore.Testing
{
    public class StoreTests
    {
        // the test class is instantiated again for each test, so it is safe to put common setup code
        // in the class constructor or members. this will be a new restaurant object for each test.
        // it's made readonly to satisfy static analysis warnings.
        readonly Store store = new Store();

        [Fact]
        public void Name_NonEmptyValue_StoresCorrectly()
        {
            const string randomNameValue = "Malwart";
            store.Name = randomNameValue;
            Assert.Equal(randomNameValue, store.Name);
        }

        [Fact]
        public void Name_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => store.Name = string.Empty);
        }
    }
}
