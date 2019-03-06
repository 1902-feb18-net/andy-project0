using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Lib;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClothingStore.Context
{
    public class OrderRepo : IOrdersRepo
    {
        private readonly Project0Context _db;

        public OrderRepo(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        //public void DeleteOrder(int orderId)
        //{
        //    _db.Remove(_db.StoreOrder.Find(orderId));
        //}

        public void DisplayOrderDetails(int orderId)
        {
            Order orderToDisplay = GetOrderByOrderId(orderId);
            Console.WriteLine($"Order Id is {orderToDisplay.OrderId} ");
            Console.WriteLine($"User Id is {orderToDisplay.CustomerId} ");
            Console.WriteLine($"Store order location is {orderToDisplay.StoreId} ");
            Console.WriteLine($"Total cost is {orderToDisplay.Total } ");
            Console.WriteLine($"Order date is {orderToDisplay.OrderTime } ");
            List<Products> listProducts = GetProductsOfOrders(orderId).ToList();
            Console.WriteLine("Products in the order");
            foreach (var product in listProducts)
            {
                Console.WriteLine($"Product: {product.ItemName} price is {product.ItemPrice}");
            }

        }

        public IEnumerable<Order> DisplayOrderHistory(string sortOrder)
        {
            if (sortOrder.ToLower() == "e") //e for earliest
            {
                IEnumerable<Order> orderHistory = Mapper.Map(_db.StoreOrder);
                return orderHistory.OrderBy(x => x.OrderTime);
            }
            else if (sortOrder.ToLower() == "l") //e for latest
            {
                IEnumerable<Order> orderHistory = Mapper.Map(_db.StoreOrder);
                return orderHistory.OrderByDescending(x => x.OrderTime);
            }
            else if (sortOrder.ToLower() == "c") //c for cheapest
            {
                IEnumerable<Order> orderHistory = Mapper.Map(_db.StoreOrder);
                return orderHistory.OrderBy(x => x.Total);
            }
            else if (sortOrder.ToLower() == "x") //x for most expensive
            {
                IEnumerable<Order> orderHistory = Mapper.Map(_db.StoreOrder);
                return orderHistory.OrderByDescending(x => x.Total);
            }
            else
            {
                // default latest orders
                IEnumerable<Order> orderHistory = Mapper.Map(_db.StoreOrder);
                return orderHistory.OrderByDescending(x => x.OrderTime);
            }
        }

        public IEnumerable<Order> DisplayOrderHistoryCustomer(int customer)
        {
            IEnumerable<Order> orderHistory = Mapper.Map(_db.StoreOrder);
            return orderHistory.OrderBy(c => c.OrderTime).Where(c => c.CustomerId == customer);
        }

        public IEnumerable<Order> DisplayOrderHistoryStore(int store)
        {
            IEnumerable<Order> orderHistory = Mapper.Map(_db.StoreOrder);
            return orderHistory.OrderBy(c => c.StoreId).Where(c => c.StoreId == store);
        }

        public Order GetOrderByOrderId(int orderId)
        {
            return Mapper.Map(_db.StoreOrder.Find(orderId));
        }

        public IEnumerable<Order> GetOrders()
        {
            return Mapper.Map(_db.StoreOrder);
        }

        public IEnumerable<Products> GetProductsOfOrders(int OrderId)
        {
            List<OrderList> orderList = _db.OrderList.Where(x => x.OrderId == OrderId).ToList();
            List<Products> orderProduct = new List<Products>();
            ProductRepo productRepo = new ProductRepo(_db);
            foreach (var item in orderList)
            {
                Products product = productRepo.GetProductsById(item.OrderId);
                orderProduct.Add(product);
            }
            return orderProduct;
        }

        public void InsertOrder(Order order)
        {
            _db.StoreOrder.Include(o => o.OrderList);
            _db.Add(Mapper.Map(order));
        }

        public int lastId()
        {
            Order x = Mapper.Map(_db.StoreOrder.OrderByDescending(o => o.OrderId).First());
            return x.OrderId;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _db.Entry(_db.StoreOrder.Find(order.OrderId)).CurrentValues.SetValues(Mapper.Map(order));
        }
    }
}
