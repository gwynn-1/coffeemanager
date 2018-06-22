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

        public bool create(Bill bill)
        {
            try
            {
                bill.created_at = DateTime.Now;
                bill.updated_at = DateTime.Now;
                db.Bills.Add(bill);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(Bill bills, int id)
        {
            try
            {
                bills.updated_at = DateTime.Now;
                (from bill in db.Bills
                 where bill.id_bill == id
                 select bill)
                              .ToList()
                              .ForEach((p) =>
                              {
                                  p.total_price = bills.total_price;
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
                var item = db.Bills.Where(bill => bill.id_bill == id).SingleOrDefault();
                db.Bills.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Bill getBillByID(int id)
        {
            var result = (from bill in db.Bills
                          where bill.id_bill == id
                          select bill).SingleOrDefault();
            return (Bill)result;
        }
    }
}
