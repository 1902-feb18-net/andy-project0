using System;
using System.Collections.Generic;

namespace ClothingStore.Context
{
    public partial class Location
    {
        public Location()
        {
            Customer = new HashSet<Customer>();
            Inventory = new HashSet<Inventory>();
            StoreOrder = new HashSet<StoreOrder>();
        }

        public int LocationId { get; set; }
        public string StoreName { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<StoreOrder> StoreOrder { get; set; }
    }
}
