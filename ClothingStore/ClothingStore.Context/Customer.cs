using System;
using System.Collections.Generic;

namespace ClothingStore.Context
{
    public partial class Customer
    {
        public Customer()
        {
            StoreOrder = new HashSet<StoreOrder>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DefaultStoreId { get; set; }

        public virtual Location DefaultStore { get; set; }
        public virtual ICollection<StoreOrder> StoreOrder { get; set; }
    }
}
