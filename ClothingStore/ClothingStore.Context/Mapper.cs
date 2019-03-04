using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClothingStore.Context
{
    class Mapper
    {
        // mapping Store and Location
        public static Lib.Store Map(Location store) => new Lib.Store
        {
            Id = store.LocationId,
            Name = store.StoreName
        };

        public static Location Map(Lib.Store store) => new Location
        {
            LocationId = store.Id,
            StoreName = store.Name
        };

        //mapping Customer with Customer
        public static Lib.Customer Map(Customer customer) => new Lib.Customer
        {
            Id = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DefaultStoreId = customer.DefaultStoreId
        };

        public static Customer Map(Lib.Customer customer) => new Customer
        {
            CustomerId = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DefaultStoreId = customer.DefaultStoreId
        };

        // mapping Order with StoreOrder
        public static Lib.Order Map(StoreOrder order) => new Lib.Order
        {
            OrderId = order.OrderId,
            StoreId = order.StoreId,
            CustomerId = order.CustomerId,
            Total = order.Total,
            DatePurchased = order.DatePurchased
        };

        public static StoreOrder Map(Lib.Order order) => new StoreOrder
        {
            OrderId = order.OrderId,
            StoreId = order.StoreId,
            CustomerId = order.CustomerId,
            Total = order.Total,
            DatePurchased = order.DatePurchased
        };

        // mapping Products with ItemProducts
        public static Lib.Products Map(ItemProducts items) => new Lib.Products
        {
            ItemId = items.ItemId,
            ItemName = items.ItemName,
            ItemPrice = items.ItemPrice,
            ItemDescription = items.ItemDescription
        };

        public static ItemProducts Map(Lib.Products items) => new ItemProducts
        {
            ItemId = items.ItemId,
            ItemName = items.ItemName,
            ItemPrice = items.ItemPrice,
            ItemDescription = items.ItemDescription
        };

        // mapping Inventory with Inventory
        public static Lib.Inventory Map(Inventory inventory) => new Lib.Inventory
        {
            StoreId = inventory.StoreId,
            ItemId = inventory.ItemId,
            ItemRemaining = inventory.ItemRemaining,
            InventoryId = inventory.InventoryId

        };

        public static Inventory Map(Lib.Inventory inventory) => new Inventory
        {
            StoreId = inventory.StoreId,
            ItemId = inventory.ItemId,
            ItemRemaining = inventory.ItemRemaining,
            InventoryId = inventory.InventoryId
        };

        // mapping OrderList with orderList
        public static Lib.OrderList Map(OrderList orderList) => new Lib.OrderList
        {
            OrderId = orderList.OrderId,
            ItemId = orderList.ItemId,
            ItemBought = orderList.ItemBought,
            OrderListId = orderList.OrderListId

        };

        public static OrderList Map(Lib.OrderList orderList) => new OrderList
        {
            OrderId = orderList.OrderId,
            ItemId = orderList.ItemId,
            ItemBought = orderList.ItemBought,
            OrderListId = orderList.OrderListId
        };

        public static IEnumerable<Lib.Store> Map(IEnumerable<Location> stores) => stores.Select(Map);
        public static IEnumerable<Location> Map(IEnumerable<Lib.Store> stores) => stores.Select(Map);

        public static IEnumerable<Lib.Customer> Map(IEnumerable<Customer> customers) => customers.Select(Map);
        public static IEnumerable<Customer> Map(IEnumerable<Lib.Customer> customers) => customers.Select(Map);

        public static IEnumerable<Lib.Order> Map(IEnumerable<StoreOrder> orders) => orders.Select(Map);
        public static IEnumerable<StoreOrder> Map(IEnumerable<Lib.Order> orders) => orders.Select(Map);

        public static IEnumerable<Lib.Products> Map(IEnumerable<ItemProducts> items) => items.Select(Map);
        public static IEnumerable<ItemProducts> Map(IEnumerable<Lib.Products> items) => items.Select(Map);

        public static IEnumerable<Lib.Inventory> Map(IEnumerable<Inventory> inventories) => inventories.Select(Map);
        public static IEnumerable<Inventory> Map(IEnumerable<Lib.Inventory> inventories) => inventories.Select(Map);

        public static IEnumerable<Lib.OrderList> Map(IEnumerable<OrderList> orderLists) => orderLists.Select(Map);
        public static IEnumerable<OrderList> Map(IEnumerable<Lib.OrderList> orderLists) => orderLists.Select(Map);
    }
}
