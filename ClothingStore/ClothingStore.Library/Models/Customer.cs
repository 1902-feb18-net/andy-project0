using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
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
        private string _sname;

        //customer id
        public int Id { get; set; }
        public int DefaultStoreId { get; set; }

        // getting a customer's first name and last name
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

        public string StoreName
        {
            get => _sname;
            set
            {
                CheckArgException(value);
                _sname = value; 
            }
        }

        public DateTime OrderTime { get; set; }

        // cannot place more than one order from the same location within two hours
        // might need to work on this some more, need to make sure right location
        public bool WithinTwoHoursRule
        {
            get
            {
                if (OrderTime != null)
                {
                    DateTime timeNow = DateTime.Now;
                    // consider turning into minutes and dividing by 60. or possibly % 120
                    double timeLeft = (OrderTime - timeNow).TotalHours;
                    if (timeLeft < 2)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        // instead of repeating myself, checking argument exception here
        public static void CheckArgException(string val)
        {
            if (val.Length == 0)
            {
                // throws error if there wasn't an input 
                throw new ArgumentException("String must not be empty.", nameof(val));
            }
        }
    }
}
