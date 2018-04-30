using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    class TableModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public List<Table> getList()
        {
            var list = db.Tables.ToList();
            return (list as List<Table>);
        }
    }
}
