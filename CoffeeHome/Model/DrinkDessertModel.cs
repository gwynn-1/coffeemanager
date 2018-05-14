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

        public int getPriceDrink(int id)
        {
            if(id == null || id ==0)
                id = 1;
            var result = (from drink in db.DrinkAndDesserts
                         where drink.id_drink == id
                         select drink.price).SingleOrDefault().ToString();
            return int.Parse(result);
        }
    }
}
