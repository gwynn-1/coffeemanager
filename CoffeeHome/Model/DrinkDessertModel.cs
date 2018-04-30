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
    }
}
