using System;
using System.Collections.Generic;

namespace ClothingStore.Context
{
    public partial class OrderList
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ItemBought { get; set; }
        public int OrderListId { get; set; }

        public virtual ItemProducts Item { get; set; }
        public virtual StoreOrder Order { get; set; }
    }
}
