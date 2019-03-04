using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClothingStore.Context
{
    public partial class Project0Context : DbContext
    {
        public Project0Context()
        {
        }

        public Project0Context(DbContextOptions<Project0Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<ItemProducts> ItemProducts { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<OrderList> OrderList { get; set; }
        public virtual DbSet<StoreOrder> StoreOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Project0");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("UQ__Customer__A4AE64B92DCC6A73")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DefaultStoreId).HasColumnName("DefaultStoreID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DefaultStore)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.DefaultStoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Location_ID");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory", "Project0");

                entity.HasIndex(e => e.InventoryId)
                    .HasName("UQ__Inventor__F5FDE6D2AD5A0ADE")
                    .IsUnique();

                entity.Property(e => e.InventoryId)
                    .HasColumnName("InventoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Item_ID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Location_ID");
            });

            modelBuilder.Entity<ItemProducts>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK_Item_ID");

                entity.ToTable("ItemProducts", "Project0");

                entity.HasIndex(e => e.ItemId)
                    .HasName("UQ__ItemProd__727E83EA76A4B669")
                    .IsUnique();

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemDescription).HasMaxLength(200);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ItemPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "Project0");

                entity.HasIndex(e => e.LocationId)
                    .HasName("UQ__Location__E7FEA476009E789D")
                    .IsUnique();

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<OrderList>(entity =>
            {
                entity.ToTable("OrderList", "Project0");

                entity.HasIndex(e => e.OrderListId)
                    .HasName("UQ__OrderLis__BFE8AA5A3C68E6BE")
                    .IsUnique();

                entity.Property(e => e.OrderListId)
                    .HasColumnName("OrderListID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderList)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orderlist_Item_ID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderList)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orderlist_Order_ID");
            });

            modelBuilder.Entity<StoreOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Order_ID");

                entity.ToTable("StoreOrder", "Project0");

                entity.HasIndex(e => e.OrderId)
                    .HasName("UQ__StoreOrd__C3905BAEC08A84D3")
                    .IsUnique();

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.StoreOrder)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer_ID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreOrder)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Location_ID");
            });
        }
    }
}
