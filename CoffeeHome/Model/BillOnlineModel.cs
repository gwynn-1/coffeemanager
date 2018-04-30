using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    class BillOnlineModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<Bill_Online> getList()
        {
            var list = db.Bill_Online.ToList();
            return (list as List<Bill_Online>);
        }
    }
}
