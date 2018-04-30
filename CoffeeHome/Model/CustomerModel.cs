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
    }
}
