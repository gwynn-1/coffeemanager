using CoffeeHome.Model;
using CoffeeHome.Vendor.MD5Provider.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeeHome.ViewModel
{
    public class SignupViewModel:BaseViewModel
    {
        public ICommand signupCommand { get; set; }
        public Staff StaffViewObject
        { get => staffViewObject; set {
                staffViewObject = value;
                OnPropertyChanged("staffViewObject");
            }
        }

        private StaffModel staff = new StaffModel();
        private Staff staffViewObject = new Staff();

        public SignupViewModel()
        {
            this.signupCommand = new RelayCommand<Staff>( p=> true , OnSignupCommand);
        }

        private void OnSignupCommand(Staff obj)
        {
            obj.password = MD5Provider.MD5Encrypt(obj.password);
            if (staff.CreateStaff(obj))
            {

            }
        }
    }
}
