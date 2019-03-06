using ClothingStore.Lib;
using ClothingStore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using ClothingStore.Library;

namespace ClothingStore.Testing
{
    public class ProductTest
    {
        [Theory]
        [InlineData(1, 15)]
        [InlineData(2, 35)]
        [InlineData(3, 55)]
        public void ProductPriceIsAddedCorrectly(int productID, int expectedCost)
        {
            // arrange
            var optionsBuilder = new DbContextOptionsBuilder<Project0Context>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
            var options = optionsBuilder.Options;
            var dbContext = new Project0Context(options);
            ProductRepo productsRepo = new ProductRepo(dbContext);

            // act
            Products sutProduct = productsRepo.GetProductsById(productID);

            // assert
            Assert.Equal(expectedCost, sutProduct.ItemPrice);
        }
    }
}
