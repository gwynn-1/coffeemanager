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

        public bool create(Bill_Online bill_Online)
        {
            try
            {
                bill_Online.created_at = DateTime.Now;
                bill_Online.updated_at = DateTime.Now;
                db.Bill_Online.Add(bill_Online);
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
