using System;
using LinqStatistics;
using System.Linq;
using System.Collections.Generic;

namespace ClothingStore.Lib
{
    /// <summary>
    /// a store's name, collection of orders
    /// </summary>
    public class Store
    {
        // backing field for the name property
        private string _name;
        //store id
        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (value.Length == 0)
                {
                    // throws error if there wasn't an input 
                    throw new ArgumentException("Name must not be empty.", nameof(value));
                }
                _name = value;
            }
        }

        // all of the order purchases
        public List<Order> Order { get; set; } = new List<Order>();



        // Examples of Linq things I can use
        //IEnumerable<int> data = new int[] { 1, 2, 5, 6, 6, 8, 9, 9, 9 };
        // sample of some of the linq statistics I can use
        //Console.WriteLine("Count = {0}", data.Count());
        //Console.WriteLine("Average = {0}", data.Average());
        //Console.WriteLine("Median = {0}", data.Median());
        //Console.WriteLine("Mode = {0}", data.Mode());
        //Console.WriteLine("Sample Variance = {0}", data.Variance());
        //Console.WriteLine("Sample Standard Deviation = {0}", data.StandardDeviation());
        //Console.WriteLine("Population Variance = {0}", data.VarianceP());
        //Console.WriteLine("Population Standard Deviation = {0}", data.StandardDeviationP());
        //Console.WriteLine("Range = {0}", data.Range());
    }
}
