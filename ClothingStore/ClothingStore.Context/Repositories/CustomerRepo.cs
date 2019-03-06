using System;
using System.Collections.Generic;
using System.Text;
using ClothingStore.Lib;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClothingStore.Context
{
    public class CustomerRepo : ICustomerRepo, IDisposable
    {
        private readonly Project0Context _db;

        public CustomerRepo(Project0Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Lib.Customer> GetCustomerByName(string fName, string lName)
        {
            return Mapper.Map(_db.Customer.Where(c => c.FirstName == fName && c.LastName == lName));
        }

        public IEnumerable<Lib.Customer> GetCustomers()
        {
            return Mapper.Map(_db.Customer);
        }

        public void InsertCustomer(Lib.Customer customer)
        {
            _db.Add(Mapper.Map(customer));
        }

        public void UpdateCustomer(Lib.Customer customer)
        {
            //_db.Entry(customer).State = EntityState.Modified;
            // calling Update would mark every property as Modified.
            // this way will only mark the changed properties as Modified.
            _db.Entry(_db.Customer.Find(customer.Id)).CurrentValues.SetValues(Mapper.Map(customer));
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void SetDefaultStore(int storeId)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool d)
        {
            if (!this.disposed)
            {
                if (d)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true; 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
