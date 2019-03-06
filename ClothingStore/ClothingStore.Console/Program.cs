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

            // uncomment this if you want to use the logger
            //optionsBuilder.UseLoggerFactory(AppLoggerFactory);
            var options = optionsBuilder.Options;

            var dbcontext = new CS.Project0Context(options);

            //IClothingStoreRepo clothingStoreRepo = new CS.StoreRepo(dbcontext);
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
                Console.WriteLine("SIGNED IN!!!" + signedIn);

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
