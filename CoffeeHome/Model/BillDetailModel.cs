using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    class BillDetailModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<Bill_details> getList()
        {
            var list = db.Bill_details.ToList();
            return (list as List<Bill_details>);
        }

        public bool create(Bill_details billDetail)
        {
            try
            {
                billDetail.created_at = DateTime.Now;
                billDetail.updated_at = DateTime.Now;
                db.Bill_details.Add(billDetail);
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
