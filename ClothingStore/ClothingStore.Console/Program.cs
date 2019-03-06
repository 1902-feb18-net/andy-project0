using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Library;
using ClothingStore.Lib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
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
            optionsBuilder.UseLoggerFactory(AppLoggerFactory);
            var options = optionsBuilder.Options;

            var dbcontext = new CS.Project0Context(options);
            IClothingStoreRepo clothingStoreRepo = new CS.StoreRepo(dbcontext);

            Console.WriteLine("Welcome to the Clothing Store!");
            while(true)
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
                        CaseR(clothingStoreRepo);
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
            Console.WriteLine("r:\tDisplay Store Locations.");
            Console.WriteLine("a:\tAdd new Customer.");
            Console.WriteLine("s:\tSave data to disk.");
            Console.WriteLine("l:\tLoad data from disk.");
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
