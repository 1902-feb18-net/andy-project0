using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Lib;

namespace ClothingStore.Context
{
    public class StoreRepo : IClothingStoreRepo
    {
        private readonly Project0Context _db;

        public StoreRepo(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Store GetStoreById(int storeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Store> GetStores()
        {
            return Mapper.Map(_db.Location);
        }
    }
}
