using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    class BillModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<Bill> getList()
        {
            var list = db.Bills.ToList();
            return (list as List<Bill>);
        }
    }
}
