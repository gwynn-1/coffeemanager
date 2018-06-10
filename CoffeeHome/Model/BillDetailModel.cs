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
            var list = db.Bill_details.Include("DrinkAndDessert").ToList();
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

        public Bill_details getDetailByID(int id_bill,int id_food)
        {
            var result = (from detail in db.Bill_details.Include("DrinkAndDessert")
                          where detail.id_bill == id_bill && detail.id_food == id_food
                          select detail).SingleOrDefault();
            return (Bill_details)result;
        }

        public bool delete(int id_bill,int id_food)
        {
            try
            {
                var item = db.Bill_details.Where(bdt => bdt.id_bill == id_bill && bdt.id_food ==id_food).SingleOrDefault();
                db.Bill_details.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(Bill_details billdetail, int id_bill, int id_food)
        {
            try
            {
                billdetail.updated_at = DateTime.Now;
                (from billdt in db.Bill_details
                 where billdt.id_bill == id_bill && billdt.id_food == id_food
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
