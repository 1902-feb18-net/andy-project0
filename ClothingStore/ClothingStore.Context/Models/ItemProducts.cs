using System;
using System.Collections.Generic;

namespace ClothingStore.Context
{
    public partial class ItemProducts
    {
        public ItemProducts()
        {
            Inventory = new HashSet<Inventory>();
            OrderList = new HashSet<OrderList>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemDescription { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<OrderList> OrderList { get; set; }
    }
}
