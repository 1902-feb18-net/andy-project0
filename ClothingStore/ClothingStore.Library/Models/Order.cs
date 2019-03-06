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
        private decimal? _total;

        // the order id
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }

        public decimal? Total
        {
            get => _total;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Total ${value} must be 0 or above.",
                        nameof(value));
                }
                _total = value;
            }
        }
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

        public List<OrderList> orderLists { get; set; } = new List<OrderList>();
        public List<Products> products { get; set; } = new List<Products>();

        // rule for credit card usage: usage of credit card is only for purchases of over 20$
        // only shirt is the only item under 20
        public void AboveTwenty(Products product)
        {
            if(orderLists.Count < 2)
            {
                this.products.Add(product);
                this.Total += product.ItemPrice;

                if(Total < 20)
                {
                    Console.WriteLine("You need to buy 20$ or more in this store (assumption that only credit card is allowed)");
                    this.products.Remove(product);
                    this.Total -= product.ItemPrice;
                }
                else
                {
                    bool insert = true;
                    foreach (var item in orderLists)
                    {
                        if (item.ItemId == product.ItemId)
                        {
                            insert = false;
                            item.ItemBought++;
                        }
                    }
                    if(insert == true)
                    {
                        orderLists.Add(new OrderList() { ItemId = product.ItemId, ItemBought = 1 });
                    }
                }
                Console.WriteLine("product added to order");
            }
            else
            {
                Console.WriteLine("Sorry, but you did not purchase enough to use your credit card. Product not added");
            }
        }

        // as you make purchases, if total is 200$ or more you get 20$ off
        public void AddOrder(Products product)
        {
            if (orderLists.Count >= 0)
            {
                this.products.Add(product);
                this.Total += product.ItemPrice;

                if (Total >= 200)
                {
                    Console.WriteLine("You get 20$ off for purchases of 200$ or more!");
                    this.Total -= product.ItemPrice - 20;
                }
                
                bool insert = true;
                foreach (var item in orderLists)
                {
                    if (item.ItemId == product.ItemId)
                    {
                        insert = false;
                        item.ItemBought++;
                    }
                }
                if (insert == true)
                {
                    orderLists.Add(new OrderList() { ItemId = product.ItemId, ItemBought = 1 });
                }
                
                Console.WriteLine("product added to order");
            }
            else
            {
                Console.WriteLine("Sorry, some error has occured");
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
