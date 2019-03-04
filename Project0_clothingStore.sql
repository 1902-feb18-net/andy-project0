-- create our DB
CREATE DATABASE Project0;
GO

-- create a schema
CREATE SCHEMA Project0;
GO

-- create tables
CREATE TABLE Project0.Location (
	LocationID INT NOT NULL UNIQUE,
	StoreName NVARCHAR(100) NOT NULL,
);

CREATE TABLE Project0.Customer (
	CustomerID INT NOT NULL UNIQUE,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	DefaultStoreID INT NOT NULL
);

CREATE TABLE Project0.StoreOrder (
	OrderID INT NOT NULL UNIQUE,
	StoreID INT NOT NULL,
	CustomerID INT NOT NULL,
	DatePurchased DATETIME2 NOT NULL,
	Total MONEY
);

CREATE TABLE Project0.OrderList (
	OrderID INT NOT NULL,
	ItemID INT NOT NULL,
	ItemBought INT NOT NULL,
	OrderListID INT NOT NULL UNIQUE
);

CREATE TABLE Project0.Inventory (
	StoreID INT NOT NULL,
	ItemID INT NOT NULL, -- should FK to ItemProducts
	ItemRemaining INT NOT NULL,
	InventoryID INT NOT NULL UNIQUE
);

CREATE TABLE Project0.ItemProducts (
	ItemID INT NOT NULL UNIQUE,
	ItemName NVARCHAR(100) NOT NULL,
	ItemPrice MONEY NOT NULL,
	ItemDescription NVARCHAR(200),
)

-- add primary keys
ALTER TABLE Project0.Location   
	ADD CONSTRAINT PK_Location_ID PRIMARY KEY CLUSTERED (LocationID);
ALTER TABLE Project0.Customer   
	ADD CONSTRAINT PK_Customer_ID PRIMARY KEY CLUSTERED (CustomerID);
ALTER TABLE Project0.StoreOrder   
	ADD CONSTRAINT PK_Order_ID PRIMARY KEY CLUSTERED (OrderID);
ALTER TABLE Project0.ItemProducts   
	ADD CONSTRAINT PK_Item_ID PRIMARY KEY CLUSTERED (ItemID);

-- add primary keys to my junction table
ALTER TABLE Project0.Inventory
	ADD CONSTRAINT PK_Inventory_ID PRIMARY KEY CLUSTERED (InventoryID);
ALTER TABLE Project0.OrderList
	ADD CONSTRAINT PK_OrderList_ID PRIMARY KEY CLUSTERED (OrderListID);


-- add FK
-- store to location
ALTER TABLE Project0.Inventory 
	ADD CONSTRAINT FK_Store_Location_ID FOREIGN KEY (StoreID) REFERENCES Project0.Location (LocationID);

-- inventory to itemProduct
ALTER TABLE Project0.Inventory 
	ADD CONSTRAINT FK_Inventory_Item_ID FOREIGN KEY (ItemID) REFERENCES Project0.ItemProducts (ItemID);

-- Order to Customer
ALTER TABLE Project0.StoreOrder 
	ADD CONSTRAINT FK_Order_Customer_ID FOREIGN KEY (CustomerID) REFERENCES Project0.Customer (CustomerID);
-- Order to Location
ALTER TABLE Project0.StoreOrder 
	ADD CONSTRAINT FK_Order_Location_ID FOREIGN KEY (StoreID) REFERENCES Project0.Location (LocationID);

-- customer to location
ALTER TABLE Project0.Customer 
	ADD CONSTRAINT FK_Customer_Location_ID FOREIGN KEY (DefaultStoreID) REFERENCES Project0.Location (LocationID);

-- orderlist to order
ALTER TABLE Project0.OrderList 
	ADD CONSTRAINT FK_Orderlist_Order_ID FOREIGN KEY (OrderID) REFERENCES Project0.StoreOrder (OrderID);
-- orderlist to Item
ALTER TABLE Project0.OrderList 
	ADD CONSTRAINT FK_Orderlist_Item_ID FOREIGN KEY (ItemID) REFERENCES Project0.ItemProducts (ItemID);

-- add some things into the DB now

-- inserts for store
INSERT Project0.Location(LocationID, StoreName)
	VALUES(1, 'MallWart')
INSERT Project0.Location(LocationID, StoreName)
	VALUES(2, 'CJNickel')
INSERT Project0.Location(LocationID, StoreName)
	VALUES(3, 'Apple Republic')

SELECT * FROM Project0.Location;

-- inserts for customer
INSERT Project0.Customer(CustomerID, FirstName, LastName, DefaultStoreID)
	VALUES(1, 'John', 'Lenin', 1);
INSERT Project0.Customer(CustomerID, FirstName, LastName, DefaultStoreID)
	VALUES(2, 'Steve', 'Gates', 2);
INSERT Project0.Customer(CustomerID, FirstName, LastName, DefaultStoreID)
	VALUES(3, 'Chew', 'Baka', 3);

SELECT * FROM Project0.Customer;

-- insert ItemProducts
Insert Project0.ItemProducts(ItemID, ItemName, ItemDescription, ItemPrice)
	VALUES(1, 'shirt', 'short sleeves t-shirt', 15.00)
Insert Project0.ItemProducts(ItemID, ItemName, ItemDescription, ItemPrice)
	VALUES(2, 'pants', 'shorts up to the knees', 35.00)
Insert Project0.ItemProducts(ItemID, ItemName, ItemDescription, ItemPrice)
	VALUES(3, 'shoes', 'standard shoes, not Wide', 55.00)

SELECT * FROM Project0.ItemProducts;

-- insert into StoreOrder
INSERT Project0.StoreOrder(OrderID, StoreID, CustomerID, DatePurchased, Total)
	VALUES (1, 1, 1, '2019-03-01 12:30:30', 185.00)
INSERT Project0.StoreOrder(OrderID, StoreID, CustomerID, DatePurchased, Total)
	VALUES (2, 2, 2, '2019-03-02 14:30:30', 115.00)
INSERT Project0.StoreOrder(OrderID, StoreID, CustomerID, DatePurchased, Total)
	VALUES (3, 3, 3, '2019-03-02 15:30:30', 55.00)

SELECT * FROM Project0.StoreOrder;

-- insert into OrderList
-- add OrderListID
Insert Project0.OrderList(OrderID, ItemID, ItemBought, OrderListID)
	VALUES(1, 1, 4, 1)
Insert Project0.OrderList(OrderID, ItemID, ItemBought, OrderListID)
	VALUES(1, 2, 2, 2)
Insert Project0.OrderList(OrderID, ItemID, ItemBought, OrderListID)
	VALUES(1, 3, 1, 3)
Insert Project0.OrderList(OrderID, ItemID, ItemBought, OrderListID)
	VALUES(2, 1, 3, 4)
Insert Project0.OrderList(OrderID, ItemID, ItemBought, OrderListID)
	VALUES(2, 2, 2, 5)
Insert Project0.OrderList(OrderID, ItemID, ItemBought, OrderListID)
	VALUES(3, 3, 1, 6)

SELECT * FROM Project0.OrderList;

-- insert into inventory
-- add InventoryID
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(1, 1, 50, 1)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(1, 2, 20, 2)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(1, 2, 10, 3)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(2, 1, 50, 4)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(2, 2, 20, 5)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(2, 2, 10, 6)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(3, 1, 50, 7)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(3, 2, 20, 8)
INSERT Project0.Inventory(StoreID, ItemID, ItemRemaining, InventoryID)
	VALUES(3, 2, 10, 9)

Select * FROM Project0.Inventory;