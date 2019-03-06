using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    public class Products
    {
        // for now making basics, so add to it later
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        //public decimal? ItemPrice { get; set; }
        public string ItemDescription { get; set; }

        private decimal _price;

        public decimal ItemPrice
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Item Price ${value} must be above 0.",
                        nameof(value));
                }
                _price = value;
            }
        }
        
        public ICollection<OrderList> orderList { get; set; }

    }
}
