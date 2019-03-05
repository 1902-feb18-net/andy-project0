using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Lib;

namespace ClothingStore.Context
{
    public class OrderRepo : IOrdersRepo
    {
        private readonly Project0Context _db;

        public OrderRepo(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void DeleteOrder(int orderId)
        {
            _db.Remove(_db.StoreOrder.Find(orderId));
        }

        public void DisplayOrderDetails(int orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> DisplayOrderHistory(string sortOrder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> DisplayOrderHistoryCustomer(int customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> DisplayOrderHistoryStore(int store)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void InsertOrder(Order order)
        {
            _db.Add(Mapper.Map(order));
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
