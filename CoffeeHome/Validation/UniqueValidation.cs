using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeHome.Model;
using System.Windows.Controls;
using System.Threading;

namespace CoffeeHome.Validation
{
    class UniqueValidation : ValidationRule
    {
        public string PropertyName { get; set; }
        public string Table { get; set; }
        public string Field { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value != null)
            {
                string message = PropertyName + " đã tồn tại trong hệ thống";
                switch (Table)
                {
                    case "Staff" :
                        if (this.checkStaffField(value.ToString()))
                            return new ValidationResult(false, message);
                        else
                            return new ValidationResult(true, null);
                    default: return new ValidationResult(true, null);
                }
            }
            return new ValidationResult(true, null);
        }

        public bool checkStaffField(string input)
        {
            bool isExist= false;
            Thread checkStaff = new Thread(()=> {
                switch (this.Field)
                {
                    case "username": isExist = StaffModel.checkUniqueStaffUsername(input); break;
                    case "email": isExist = StaffModel.checkUniqueStaffEmail(input); break;
                    default: isExist = false; break;
                }
            });
            checkStaff.SetApartmentState(ApartmentState.STA);
            checkStaff.Start();
            checkStaff.Join();
            return isExist;
        }
    }
}
