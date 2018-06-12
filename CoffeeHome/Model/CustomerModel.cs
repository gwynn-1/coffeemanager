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

        public Customer getCustomerByID(int id)
        {
            var result = (from cus in db.Customers
                          where cus.id_customer == id
                          select cus).SingleOrDefault();
            return (Customer)result;
        }

        public bool update(Customer customer, int id)
        {
            try
            {
                customer.updated_at = DateTime.Now;
                (from cus in db.Customers
                 where cus.id_customer == id
                 select cus)
                              .ToList()
                              .ForEach((p) =>
                              {
                                  p.name = customer.name;
                                  p.email = customer.email;
                                  p.cmnd = customer.cmnd;
                                  p.points = customer.points;
                                  p.sdt = customer.sdt;
                              });
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool delete(int id)
        {
            try
            {
                var item = db.Customers.Where(cus => cus.id_customer == id).SingleOrDefault();
                db.Customers.Remove(item);
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
