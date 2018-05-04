using CoffeeHome.Converter;
using CoffeeHome.Model;
using CoffeeHome.Vendor.MD5Provider.Source;
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

        private Staff staffViewObject = new Staff();
        public Staff StaffViewObject
        {
            get => staffViewObject;
            set
            {
                staffViewObject = value;
                OnPropertyChanged("staffViewObject");
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
            this.loginCommand = new RelayCommand<Staff>((p) => { return true; }, OnLoginCommand);
            IsLoginFailed = false;
        }

        private void OnLoginCommand(Staff obj)
        {
            if(obj.username == "adminerteam6769" && obj.password == "6769")
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

        private void ChangeToHome(Staff obj)
        {
            obj.password = MD5Provider.MD5Encrypt(obj.password);
            if (staff.checkStaff(obj.username, obj.password))
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
