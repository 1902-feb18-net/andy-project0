using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    /// <summary>
    /// has a store location
    /// has a customer
    /// has an order time(when the order was placed)
    /// must have some additional business rules
    /// </summary>
    public class Order
    {
        private string _customerName;
        private string _sname;

        // the order id
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public decimal? Total { get; set; }
        public DateTime DatePurchased { get; set; }

        public string StoreName
        {
            get => _sname;
            set
            {
                CheckArgException(value);
                _sname = value;
            }
        }

        // name of customer
        public string CustomerName
        {
            get => _customerName;
            set
            {
                CheckArgException(value);
                _customerName = value;
            }
        }

        // order time when order was placed
        public DateTime OrderTime { get; set; }

        // rule for credit card usage: usage of credit card is only for purchases of over 20$
        public bool UseCreditCard
        {
            get
            {
                // fill in later
                return true;
            }
        }

        // instead of repeating myself, checking argument exception here
        public static void CheckArgException(string val)
        {
            if (val.Length == 0)
            {
                // throws error if there wasn't an input 
                throw new ArgumentException("String must not be empty.", nameof(val));
            }
        }
    }
}
