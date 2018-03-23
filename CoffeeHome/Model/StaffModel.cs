using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.Model
{
    class StaffModel
    {
        private string id;
        private string name;
        private string username;
        private string password;
        private string repassword;
        private string email;
        private int sdt;
        private string level;
        CoffeeHomeEntities db = new CoffeeHomeEntities();

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public int Sdt { get => sdt; set => sdt = value; }
        public string Id { get => id; set => id = value; }
        public string Level { get => level; set => level = value; }
        public string Name { get => name; set => name = value; }
        public string Repassword { get => repassword; set => repassword = value; }

        public bool CreateStaff()
        {
            try
            {
                Staff staff = new Staff
                {
                    name = this.name,
                    username = this.username,
                    password = this.password,
                    email = this.email,
                    sdt = this.sdt,
                    level = "Staff",
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
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
    }
}
