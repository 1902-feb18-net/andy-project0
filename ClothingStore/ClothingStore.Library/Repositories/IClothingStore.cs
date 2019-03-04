using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    public interface IClothingStore
    {
        IEnumerable<Store> GetClothingStores();
    }
}
