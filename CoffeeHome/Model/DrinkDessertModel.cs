using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    public class DrinkDessertModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<DrinkAndDessert> getList()
        {
            var list = db.DrinkAndDesserts.ToList();
            return (list as List<DrinkAndDessert>);
        }

        public bool delete(int id)
        {
            try
            {
                var item = db.DrinkAndDesserts.Where(drink => drink.id_drink == id).SingleOrDefault();
                db.DrinkAndDesserts.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool create(DrinkAndDessert drinkAndDessert)
        {
            try
            {
                drinkAndDessert.created_at = DateTime.Now;
                drinkAndDessert.updated_at = DateTime.Now;
                db.DrinkAndDesserts.Add(drinkAndDessert);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(DrinkAndDessert drinkAndDessert, int id)
        {
            try
            {
                drinkAndDessert.updated_at = DateTime.Now;
                (from drink in db.DrinkAndDesserts
                 where drink.id_drink == id
                 select drink)
                              .ToList()
                              .ForEach((p) =>
                              {
                                  p.id_type = drinkAndDessert.id_type;
                                  p.name = drinkAndDessert.name;
                                  p.price = drinkAndDessert.price;
                                  p.updated_at = drinkAndDessert.updated_at;
                                  p.image = drinkAndDessert.image;
                                  p.description = drinkAndDessert.description;
                              });
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int getPriceDrink(int id)
        {
            if (id == null || id == 0)
                id = 1;
            var result = (from drink in db.DrinkAndDesserts
                          where drink.id_drink == id
                          select drink.price).SingleOrDefault().ToString();
            return int.Parse(result);
        }

        public DrinkAndDessert getDrinkByID(int id)
        {
            var result = (from drink in db.DrinkAndDesserts
                          where drink.id_drink == id
                          select drink).SingleOrDefault();
            return (DrinkAndDessert)result;
        }
    }
}
