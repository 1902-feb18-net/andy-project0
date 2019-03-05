using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingStore.Lib
{
    public interface ICustomerRepo
    {
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Customer> GetCustomerByName(string fName, string lName);
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        void SetDefaultStore(int storeId);
        void Save();
    }
}
