using System;
using System.Collections.Generic;
using System.Text;

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

        // mapping OrderList with orderList

        public static Library.Review Map(Review review) => new Library.Review
        {
            Id = review.Id,
            ReviewerName = review.ReviewerName,
            Score = review.Score,
            Text = review.Text
        };

        public static Review Map(Library.Review review) => new Review
        {
            Id = review.Id,
            ReviewerName = review.ReviewerName,
            Score = review.Score ?? throw new ArgumentException("review score cannot be null.", nameof(review)),
            Text = review.Text
        };

        public static IEnumerable<Library.Restaurant> Map(IEnumerable<Restaurant> restaurants) => restaurants.Select(Map);

        public static IEnumerable<Restaurant> Map(IEnumerable<Library.Restaurant> restaurants) => restaurants.Select(Map);

        public static IEnumerable<Library.Review> Map(IEnumerable<Review> reviews) => reviews.Select(Map);

        public static IEnumerable<Review> Map(IEnumerable<Library.Review> reviews) => reviews.Select(Map);
    }
}
