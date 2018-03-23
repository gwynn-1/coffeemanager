using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeeHome.ViewModel
{
    class SignupViewModel:BaseViewModel
    {
        public ICommand signupCommand { get; set; }
        public StaffModel Staff { get => staff; set {
                staff = value;
                OnPropertyChanged("staff");
            }
        }

        private StaffModel staff = new StaffModel(); 

        public SignupViewModel()
        {
            this.signupCommand = new RelayCommand<StaffModel>( p=> true , OnSignupCommand);
        }

        private void OnSignupCommand(StaffModel obj)
        {
            Staff = obj;
            Staff.Password = MD5Provider.MD5Encrypt(Staff.Password);
            if (Staff.CreateStaff())
            {

            }
        }
    }
}
