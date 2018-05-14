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
    }
}
