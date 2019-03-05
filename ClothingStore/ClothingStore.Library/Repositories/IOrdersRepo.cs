using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    interface IOrdersRepo
    {
        IEnumerable<Products> GetProductsOfOrders(int OrderId);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> DisplayOrderHistoryStore(int store);
        IEnumerable<Order> DisplayOrderHistoryCustomer(int customer);
        IEnumerable<Order> DisplayOrderHistory(string sortOrder);
        void InsertOrder(Order order);
        void DeleteOrder(int orderId);
        void UpdateOrder(Order order);
        void DisplayOrderDetails(int orderId);
        void Save();
        Order GetOrderByOrderId(int orderId);
    
    }
}
