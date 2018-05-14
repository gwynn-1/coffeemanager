using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    class CustomerModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<Customer> getList()
        {
            var list = db.Customers.ToList();
            return (list as List<Customer>);
        }

        public bool create(Customer customer)
        {
            try
            {
                customer.created_at = DateTime.Now;
                customer.updated_at = DateTime.Now;
                db.Customers.Add(customer);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
