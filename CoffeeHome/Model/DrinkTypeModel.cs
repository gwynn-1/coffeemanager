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
    }
}
