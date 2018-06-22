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
                var list = db.Bill_Online_Detail.Include("DrinkAndDessert").ToList();
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

        public Bill_Online_Detail getDetailByID(int id_bill, int id_food)
        {
            var result = (from detail in db.Bill_Online_Detail.Include("DrinkAndDessert")
                          where detail.id_bill == id_bill && detail.id_drink == id_food
                          select detail).SingleOrDefault();
            return (Bill_Online_Detail)result;
        }

        public bool delete(int id_bill, int id_food)
        {
            try
            {
                var item = db.Bill_Online_Detail.Where(bdt => bdt.id_bill == id_bill && bdt.id_drink == id_food).SingleOrDefault();
                db.Bill_Online_Detail.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(Bill_Online_Detail billdetail, int id_bill, int id_food)
        {
            try
            {
                billdetail.updated_at = DateTime.Now;
                (from billdt in db.Bill_Online_Detail
                 where billdt.id_bill == id_bill && billdt.id_drink == id_food
                 select billdt)
                              .ToList()
                              .ForEach((p) =>
                              {
                                  p.quantity = billdetail.quantity;
                                  p.price = billdetail.price;
                              });
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
