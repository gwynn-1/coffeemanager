using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    class BillOnlineDetailModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<Bill_Online_Detail> getList()
        {
            var list = db.Bill_Online_Detail.ToList();
            return (list as List<Bill_Online_Detail>);
        }

        public bool create(Bill_Online_Detail bill_Online_Detail)
        {
            try
            {
                bill_Online_Detail.created_at = DateTime.Now;
                bill_Online_Detail.updated_at = DateTime.Now;
                db.Bill_Online_Detail.Add(bill_Online_Detail);
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
