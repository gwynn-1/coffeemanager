using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    public class StaffModel
    {
        private static CoffeeHomeEntities db = new CoffeeHomeEntities();

        public bool CreateStaff(Staff staff)
        {
            try
            {
                staff.level = "Staff";
                staff.created_at = DateTime.Now;
                staff.updated_at = DateTime.Now;
                db.Staffs.Add(staff);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool checkStaff(string username, string password)
        {
            try
            {
                var result = (from s
                             in db.Staffs
                              where s.username == username
                              where s.password == password
                              select s.id_staff).Count();
                if (result > 0)
                    return true;
                else
                    return false;
            }catch(Exception e)
            {
                return false;
            }
        }

        public static bool checkUniqueStaffEmail(string input)
        {
            try
            {
                var result = (from s in db.Staffs where s.email == input select s.id_staff).Count();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool checkUniqueStaffUsername(string input)
        {
            try
            {
                var result = (from s in db.Staffs where s.username == input select s.id_staff).Count();
                if (result > 0)
                    return true;
                else
                    return false;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
