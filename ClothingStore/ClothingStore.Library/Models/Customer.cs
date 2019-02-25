using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Library.Models
{
    /// <summary>
    /// has first name, last name, etc.
    /// has a default store location to order from
    /// cannot place more than one order from the same location within two hours
    /// </summary>
    public class Customer
    {
        // backing field for the name property
        private string _fname;
        private string _lname;

        public string FirstName
        {
            get => _fname;
            set
            {
                CheckArgException(value);
                _fname = value;
            }
        }
        public string LastName
        {
            get => _lname;
            set
            {
                CheckArgException(value);
                _lname = value;
            }
        }

        // instead of repeating myself, checking argument exception here
        public static void CheckArgException(string val)
        {
            if (val.Length == 0)
            {
                // throws error if there wasn't an input 
                throw new ArgumentException("Name must not be empty.", nameof(val));
            }
        }
    }
}
