using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Library.Models
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
        public int Id { get; set; }

        // name of customer
        public string CustomerName
        {
            get => _customerName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Customer name must not be empty", nameof(value));
                }
                _customerName = value;
            }
        }

        // order time when order was placed
        public DateTime OrderTime { get; set; }
    }
}
