using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Library;
using ClothingStore.Lib;
using CL = ClothingStore.Lib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using ClothingStore.Context;
using CS = ClothingStore.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ClothingStore.ConsoleApp
{
    class Program
    {
        public static readonly LoggerFactory AppLoggerFactory =
        #pragma warning disable CS0618 // Type or member is obsolete
        new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
        #pragma warning restore CS0618 // Type or member is obsolete

        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CS.Project0Context>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);

            //let's debug conflicting key values
            optionsBuilder.EnableSensitiveDataLogging();

            // uncomment this if you want to use the logger
            //optionsBuilder.UseLoggerFactory(AppLoggerFactory);
            var options = optionsBuilder.Options;

            var dbcontext = new CS.Project0Context(options);

            // create repos to use
            CustomerRepo customerRepository = new CustomerRepo(dbcontext);
            OrderRepo orderRepository = new OrderRepo(dbcontext);
            ProductRepo productRepository = new ProductRepo(dbcontext);
            StoreRepo storeRepository = new StoreRepo(dbcontext);

            List<CL.Customer> customerList = customerRepository.GetCustomers().ToList();
            List<CL.Store> storeList = storeRepository.GetStores().ToList();

            Console.WriteLine(storeList);
            Console.WriteLine("Current stores to shop from");
            foreach(Store store in storeList)
            {
                Console.WriteLine($" {store.Name}");
            }

            Console.WriteLine("Welcome to the Clothing Store!");

            bool loop = true;
            while(loop == true)
            {
                for(int i = 1; i < customerList.Count + 1; i++)
                {
                    CL.Customer cList = customerList[i - 1];
                    string userFirstNameString = $"{i}: \"{cList.FirstName}\"";
                    string userLastNameString = $"\"{cList.LastName}\"";
                    Console.Write(userFirstNameString + " ");
                    Console.Write(userLastNameString);
                    Console.WriteLine();
                }

                Console.WriteLine("Please sign in, or 'q' to quit");
                Console.Write("First Name: ");
                
                string fName = Console.ReadLine();
                fName = checkIfInt(fName);
                Console.Write("Last Name: ");
                string lName = Console.ReadLine();
                lName = checkIfInt(lName);
                
                // check to see if user typed in q to quit
                if(fName.ToLower() == "q" || lName.ToLower() == "q")
                {
                    break;
                }

                List<CL.Customer> signedInUser = customerRepository.GetCustomerByName(fName, lName).ToList();
                CL.Customer signedIn = signedInUser[0];
                Console.WriteLine($"{fName} {lName} signed in");

                bool innerLoop = true;
                // loopception!
                while (innerLoop == true)
                {
                    PrintInstructions1();
                    var input = Console.ReadLine();

                    // was thinking of using a switch, but let's get ugly instead
                    if (input == "1")
                    {
                        List<CL.Order> orderSuggestions = orderRepository.DisplayOrderHistoryCustomer(signedIn.Id).
                            OrderByDescending(o => o.OrderTime).ToList();
                        CL.Order orderSuggest = orderSuggestions[0];

                        Console.WriteLine("Would you like to view you orders?");
                        Console.WriteLine("(y)es or (n)o?");
                        var viewOrders = Console.ReadLine();
                        if (viewOrders.ToLower() == "y")
                        {
                            // let's list out some of the orders
                            // add in a readLine option for this later
                            Console.WriteLine("Here is your most recent order on record:");
                            string orderSuggestStoreId = $"Store Id: {orderSuggest.StoreId}";
                            string orderSuggestId = $"Order Id: {orderSuggest.OrderId}";
                            string orderSuggestTotal = $"Total Cost: {orderSuggest.Total}";
                            Console.WriteLine(orderSuggestId);
                            Console.Write("Items ");

                            List<Products> printProductList = orderRepository.GetProductsOfOrders(orderSuggest.OrderId).ToList();
                            foreach (var item in printProductList)
                            {
                                // didn't include the number of items bought from each yet
                                Console.Write($"{item.ItemName}");
                            }
                            Console.WriteLine(orderSuggestTotal);
                            Console.WriteLine(orderSuggestStoreId);
                        }

                        Console.WriteLine("Alright, let's start ordering");
                        //initializing new order
                        CL.Order newOrder = new CL.Order();
                        newOrder.StoreId = orderSuggest.StoreId;
                        newOrder.CustomerId = orderSuggest.CustomerId;
                        newOrder.Total = 0;
                        newOrder.DatePurchased = DateTime.Now;
                        newOrder.OrderId = orderSuggest.OrderId;

                        //display available stores
                        Console.WriteLine("Available stores");
                        List<CL.Store> storesList = storeRepository.GetStores().ToList();
                        for (int i = 1; i < storesList.Count() + 1; i++)
                        {
                            CL.Store sl = storesList[i - 1];
                            string storeIdString = $"{i}: {sl.Id}";
                            string storeNameString = $"{i}: {sl.Name}";
                            Console.WriteLine(storeIdString + " ");
                            Console.WriteLine(storeNameString);
                        }

                        // getting user input again
                        Console.WriteLine("Please enter in the number for the store you wish to shop at");
                        string storeChoice = Console.ReadLine();
                        bool parseStore = Int32.TryParse(storeChoice, out int storeChoiceInt);
                        while (parseStore == false || (parseStore == true && storeChoiceInt > storesList.Count))
                        {
                            Console.WriteLine("Not valid input, please enter in the number for the store you wish to shop at");
                            storeChoice = Console.ReadLine();
                            parseStore = Int32.TryParse(storeChoice, out storeChoiceInt);
                        }

                        // adding your choice to order
                        newOrder.StoreId = storeChoiceInt;

                        // -----------------
                        // display available products
                        Console.WriteLine("Here are the available products");
                        List<CL.Products> productsList = productRepository.GetProducts().ToList();
                        for (int i = 1; i < productsList.Count + 1; i++)
                        {
                            CL.Products pl = productsList[i - 1];
                            string productIdString = $"{i}: {pl.ItemId}";
                            string productNameString = $"{i}: {pl.ItemName}";
                            Console.WriteLine(productIdString + " ");
                            Console.WriteLine(productNameString);
                        }

                        // now for user to decide if they want to add an item to their order
                        Console.WriteLine("Do you want to buy anything? (y)es or (n)o?");
                        string addProduct = Console.ReadLine();
                        while (!(addProduct.ToLower() == "y" || addProduct.ToLower() == "n"))
                        {
                            Console.WriteLine("not an available option, type in 'y' or 'n'");
                            addProduct = Console.ReadLine();
                        }

                        // adding products to order
                        if(addProduct.ToLower() == "y")
                        {
                            while(addProduct.ToLower() == "y")
                            {
                                Console.WriteLine("please type the Product id of the product you would like to add to your order.");
                                string productChoice = Console.ReadLine();
                                bool parseProduct = Int32.TryParse(productChoice, out int productChoiceInt);
                                while (parseProduct == false || (parseProduct == true && productChoiceInt > productsList.Count))
                                {
                                    Console.WriteLine("Not valid input, please enter a valid product ID of product you would like to your order");
                                    productChoice = Console.ReadLine();
                                    parseProduct = Int32.TryParse(productChoice, out productChoiceInt);
                                }
                                CL.Products productToBeAdded = productRepository.GetProductsById(productChoiceInt);
                                newOrder.AddOrder(productToBeAdded);

                                // enable as much product adding as you want
                                Console.WriteLine("Would you like to add another product to your order? Type (y)es or (n)o?");
                                addProduct = Console.ReadLine();
                                while (!(addProduct.ToLower() == "y" || addProduct.ToLower() == "n"))
                                {
                                    Console.WriteLine("not an available option, type in 'y' or 'n'");
                                    addProduct = Console.ReadLine();
                                }
                            }

                            // insert, save, display order
                            orderRepository.InsertOrder(newOrder);
                            orderRepository.Save();
                            orderRepository.DisplayOrderDetails(orderRepository.lastId());
                        }
                    }
                    else if (input == "2")
                    {
                        Console.WriteLine("Looking up a order history");
                        Console.WriteLine("How do you want to look up orders?");
                        Console.WriteLine("1 : location");
                        Console.WriteLine("2 : user");
                        Console.WriteLine("3 : sort all");
                        string lookChoice = Console.ReadLine();
                        while (!(lookChoice.ToLower() == "1" || lookChoice.ToLower() == "2" || lookChoice.ToLower() == "3" || lookChoice.ToLower() == "q"))
                        {
                            Console.WriteLine("Not a valid choice. Please type either '1', '2', '3' or 'q'.");
                            Console.WriteLine("How do you want to look up orders?");
                            Console.WriteLine("1 : location");
                            Console.WriteLine("2 : user");
                            Console.WriteLine("3 : sort all");
                            lookChoice = Console.ReadLine();
                        }
                        if (lookChoice.ToLower() == "1")
                        {
                            Console.WriteLine("Please provide a store Id");
                            Console.Write("Store Id: ");
                            string storeIdInput = Console.ReadLine();
                            bool parseSuccess = Int32.TryParse(storeIdInput, out int storeIdLookInt);
                            while (parseSuccess == false)
                            {
                                Console.WriteLine("Not a valid choice. Please enter a valid Store ID.");
                                Console.Write("Store ID: ");
                                lookChoice = Console.ReadLine();
                                parseSuccess = Int32.TryParse(storeIdInput, out storeIdLookInt);
                            }
                            List<Order> orderHistory = orderRepository.DisplayOrderHistoryStore(storeIdLookInt).ToList();
                            for (int i = 1; i < orderHistory.Count() + 1; i++)
                            {
                                Order orderhistory = orderHistory[i - 1];
                                string orderIdString = $"{i}: {orderhistory.OrderId}";
                                string orderStoreString = $"{i}: {orderhistory.StoreId}";
                                string orderTotalString = $"{i}: {orderhistory.Total}";
                                string orderDateTimeString = $"{i}: {orderhistory.DatePurchased}";
                                Console.Write(orderIdString + " ");
                                Console.Write(orderStoreString + " ");
                                Console.Write(orderTotalString + " ");
                                Console.Write(orderDateTimeString + " ");
                                Console.WriteLine();

                            }
                        }
                        else if (lookChoice.ToLower() == "2")
                        {
                            Console.WriteLine("Provide Customer Id");
                            string userIdLookUp = Console.ReadLine();
                            bool parseSuccess = Int32.TryParse(userIdLookUp, out int userIdLookUpInt);
                            while (parseSuccess == false)
                            {
                                Console.WriteLine("Not a valid choice. Please enter a valid Customer ID.");
                                Console.Write("Customer ID: ");
                                userIdLookUp = Console.ReadLine();
                                parseSuccess = Int32.TryParse(userIdLookUp, out userIdLookUpInt);
                            }
                            List<Order> orderHistory = orderRepository.DisplayOrderHistoryCustomer(userIdLookUpInt).ToList();
                            for (int i = 1; i < orderHistory.Count() + 1; i++)
                            {
                                Order orderhistory = orderHistory[i - 1];
                                string orderIdString = $"{i}: {orderhistory.OrderId}";
                                string orderStoreString = $"{i}: {orderhistory.StoreId}";
                                string orderTotalString = $"{i}: {orderhistory.Total}";
                                string orderDateTimeString = $"{i}: {orderhistory.DatePurchased}";
                                Console.Write(orderIdString + " ");
                                Console.Write(orderStoreString + " ");
                                Console.Write(orderTotalString + " ");
                                Console.Write(orderDateTimeString + " ");
                                Console.WriteLine();

                            }
                        }
                        else if (lookChoice.ToLower() == "3")
                        {
                            Console.WriteLine("Choose one of the below options to sort");
                            Console.WriteLine("e : earliest first");
                            Console.WriteLine("l : latest first");
                            Console.WriteLine("c : cheapest first");
                            Console.WriteLine("x : expensive first");

                            string sortChoice = Console.ReadLine();
                            while (!(sortChoice.ToLower() == "e" || sortChoice.ToLower() == "l" || sortChoice.ToLower() == "c" || sortChoice.ToLower() == "x"))
                            {
                                Console.WriteLine("Invalid choice. Please type either 'e', 'l' 'c', or 'x'.");
                                Console.WriteLine("Choose one of the below options to sort");
                                Console.WriteLine("e : earliest first");
                                Console.WriteLine("l : latest first");
                                Console.WriteLine("c : cheapest first");
                                Console.WriteLine("x : expensive first");
                                sortChoice = Console.ReadLine();
                            }
                            List<Order> collectionOrders = orderRepository.DisplayOrderHistory(sortChoice).ToList();
                            foreach (var item in collectionOrders)
                            {
                                Console.Write($"Order Id: {item.OrderId}");
                                Console.Write($"User Id: {item.CustomerId}");
                                Console.Write($"Store Id: {item.StoreId}");
                                Console.Write($"Total Cost: {item.Total}");
                                Console.Write($"Date time; {item.OrderTime}");
                                Console.WriteLine();

                            }


                        } else if (lookChoice == "3")
                        {
                            innerLoop = false;
                        }
                    }
                        
                }
                
            }
        }

        static void PrintInstructions1()
        {
            Console.WriteLine("Here are your options:");
            Console.WriteLine("1:\tPlace an order.");
            Console.WriteLine("2:\tDisplay order history.");
            Console.WriteLine("3:\tGo back to login screen");
        }

        // let's test something quick here
        static string checkIfInt(string name)
        {
            string nameInput = name;
            while (nameInput.Any(Char.IsDigit))
            {
                Console.WriteLine("No numbers in input allowed");
                nameInput = Console.ReadLine();             
            }
            if (nameInput.ToLower() == "q")
            {
                Environment.Exit(0);
            }
            return nameInput;
        }

        static void CaseR(IClothingStoreRepo storeRepo)
        {
            var stores = storeRepo.GetStores().ToList();
            Console.WriteLine();
            if(stores.Count == 0)
            {
                Console.WriteLine("There are no stores, some weird error?");
            }
            Console.WriteLine("woohoo, no bugs yet?");
            Console.ReadLine();
        }
    }
}
