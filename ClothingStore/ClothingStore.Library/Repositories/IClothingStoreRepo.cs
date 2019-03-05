using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    public interface IClothingStoreRepo
    {
        IEnumerable<Store> GetStores();
        Store GetStoreById (int storeId);
    }
}
