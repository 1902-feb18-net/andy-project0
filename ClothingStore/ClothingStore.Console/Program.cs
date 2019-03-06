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

                PrintInstructions();

                // 1. can view stores and select
                // 2. can add new customer with: fname, lname, defaultStore
                // 3. can search for customer and select customer to use
                // 4. Quit app
                
                var input = Console.ReadLine();
                switch(input)
                {
                    case "r":
                        Console.WriteLine("Here is a list of stores.");
                        //CaseR(clothingStoreRepo);
                        break;
                    case "a":
                        Console.WriteLine("Type in a your first name, last name, and default store");
                        Test();
                        break;
                    case "s":
                        Console.WriteLine("enter in a customer name then select the correct customer");
                        break;
                    case "q":
                        Console.WriteLine("Bye! Please come again!");
                        break;
                    default:
                        Console.WriteLine("\n[Please enter a valid command]");
                        break;
                }
                if (input == "q")
                {
                    break;
                }
                
            }
        }

        static void PrintInstructions()
        {
            Console.WriteLine();
            Console.WriteLine("1:\tDisplay Store Locations.");
            Console.WriteLine("2:\tAdd new Customer.");
            Console.WriteLine();
            Console.Write("Enter valid menu option, or \"q\" to quit: ");
        }

        // let's test something quick here
        static void Test()
        {
            Console.WriteLine("We are using Test");
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
