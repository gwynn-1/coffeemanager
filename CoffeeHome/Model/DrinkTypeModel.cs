using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    public class DrinkTypeModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<Drink_type> getList()
        {
            var list = db.Drink_type.ToList();
            return (list as List<Drink_type>);
        }

        public bool create(Drink_type drink_Type)
        {
            try
            {
                drink_Type.created_at = DateTime.Now;
                drink_Type.updated_at = DateTime.Now;
                db.Drink_type.Add(drink_Type);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Drink_type getDrinkTypeByID(int id)
        {
            var result = (from drink in db.Drink_type
                          where drink.id_type == id
                          select drink).SingleOrDefault();
            return (Drink_type)result;
        }

        public bool delete(int id)
        {
            try
            {
                var item = db.Drink_type.Where(drink => drink.id_type == id).SingleOrDefault();
                db.Drink_type.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(Drink_type drink_Type, int id)
        {
            try
            {
                drink_Type.updated_at = DateTime.Now;
                (from drink in db.Drink_type
                 where drink.id_type == id
                 select drink)
                              .ToList()
                              .ForEach((p) =>
                              {
                                  p.name = drink_Type.name;
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
