using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Lib;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClothingStore.Context
{
    public class ProductRepo : IProducts
    {
        private readonly Project0Context _db;

        public ProductRepo(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Products> GetProducts()
        {
            return Mapper.Map(_db.ItemProducts);
        }

        public Products GetProductsById(int productId)
        {
            return Mapper.Map(_db.ItemProducts.Find(productId));
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
