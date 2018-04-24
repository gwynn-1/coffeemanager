using CoffeeHome.Converter;
using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeeHome.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand loginCommand { get; set; }
        private StaffModel staff = new StaffModel();
        public StaffModel Staff
        {
            get => staff; set
            {
                staff = value;
                OnPropertyChanged("staff");
            }
        }
        public bool IsLoginFailed {
            get => isLoginFailed;
            set {
                isLoginFailed = value;
                OnPropertyChanged("isLoginFailed");
            }
        }
        
        private bool isLoginFailed;

        public MainViewModel()
        {
            this.loginCommand = new RelayCommand<StaffModel>((p) => { return true; }, OnLoginCommand);
            IsLoginFailed = false;
        }

        private void OnLoginCommand(StaffModel obj)
        {
            if(obj.Username == "adminerteam6769" && obj.Password == "6769")
            {
                SignupWindow signupwindow = new SignupWindow();
                Application.Current.MainWindow.Close();
                signupwindow.Show();
                Application.Current.MainWindow = signupwindow;
            }
            else
            {
                Thread changeWindowThread = new Thread(() =>
                {
                    ChangeToHome(obj);
                });
                changeWindowThread.SetApartmentState(ApartmentState.STA);
                changeWindowThread.Start();
            }
        }

        private void ChangeToHome(StaffModel obj)
        {
            Staff = obj;
            Staff.Password = MD5Provider.MD5Encrypt(Staff.Password);
            if (staff.checkStaff(staff.Username, staff.Password))
            {
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate () {
                    HomeWindow home = new HomeWindow();
                    Application.Current.MainWindow.Close();
                    home.Show();
                    Application.Current.MainWindow = home;
                }));
            }
            else
            {
                IsLoginFailed = true;
            }
        }
    }
}
