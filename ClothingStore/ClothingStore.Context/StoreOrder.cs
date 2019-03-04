using System;
using System.Collections.Generic;

namespace ClothingStore.Context
{
    public partial class StoreOrder
    {
        public StoreOrder()
        {
            OrderList = new HashSet<OrderList>();
        }

        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DatePurchased { get; set; }
        public decimal? Total { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Store { get; set; }
        public virtual ICollection<OrderList> OrderList { get; set; }
    }
}
