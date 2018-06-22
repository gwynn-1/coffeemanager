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

        public bool create(Table table)
        {
            try
            {
                table.created_at = DateTime.Now;
                table.updated_at = DateTime.Now;
                db.Tables.Add(table);
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
                var item = db.Tables.Where(cus => cus.id_table == id).SingleOrDefault();
                db.Tables.Remove(item);
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
