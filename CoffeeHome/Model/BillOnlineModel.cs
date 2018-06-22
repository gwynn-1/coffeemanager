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

        public bool update(Bill_Online bills, int id)
        {
            try
            {
                bills.updated_at = DateTime.Now;
                (from bill in db.Bill_Online
                 where bill.id_bill_online == id
                 select bill)
                              .ToList()
                              .ForEach((p) =>
                              {
                                  p.name_customer = bills.name_customer;
                                  p.sdt = bills.sdt;
                                  p.address = bills.address;
                                  p.updated_at = bills.updated_at;
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
                var item = db.Bill_Online.Where(bill => bill.id_bill_online == id).SingleOrDefault();
                db.Bill_Online.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Bill_Online getBillByID(int id)
        {
            var result = (from bill in db.Bill_Online
                          where bill.id_bill_online == id
                          select bill).SingleOrDefault();
            return (Bill_Online)result;
        }
    }
}
