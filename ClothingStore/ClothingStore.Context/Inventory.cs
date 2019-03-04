using System;
using System.Collections.Generic;

namespace ClothingStore.Context
{
    public partial class Inventory
    {
        public int StoreId { get; set; }
        public int ItemId { get; set; }
        public int ItemRemaining { get; set; }
        public int InventoryId { get; set; }

        public virtual ItemProducts Item { get; set; }
        public virtual Location Store { get; set; }
    }
}
