using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    public class CustomerViewModel:BaseViewModel
    {
        private ObservableCollection<Customer> customerList;

        public ObservableCollection<Customer> CustomerList
        {
            get => customerList;
            set
            {
                customerList = value;
                OnPropertyChanged("customerList");
            }
        }

        private CustomerModel customerModel = new CustomerModel();

        public CustomerViewModel()
        {
            List<Customer> customer = customerModel.getList();
            CustomerList = new ObservableCollection<Customer>(customer);
        }
    }
}
