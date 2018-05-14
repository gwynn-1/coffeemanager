using CoffeeHome.Model;
using CoffeeHome.TemplateView.CRUTemplate;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CoffeeHome.ViewModel
{
    public class CustomerViewModel:BaseViewModel
    {
        #region VariableBinding
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

        private string action;
        public string Action
        {
            get => action;
            set
            {
                action = value;
                OnPropertyChanged("action");
            }
        }

        private CollectionViewSource customerViewSource = new CollectionViewSource();
        public CollectionViewSource CustomerViewSource
        {
            get => customerViewSource;
            set
            {
                customerViewSource = value;
                OnPropertyChanged("customerViewSource");
            }
        }
        private CustomerCRUDialog CruDialog = new CustomerCRUDialog();
        
        #endregion

        #region Model
        private CustomerModel customerModel = new CustomerModel();
        #endregion

        #region ViewObject
        private Customer customerViewObject = new Customer();
        public Customer CustomerViewObject
        {
            get => customerViewObject; set
            {
                customerViewObject = value;
                OnPropertyChanged("customerViewObject");
            }
        }
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        private ICommand createCommand;
        public ICommand CreateCommand { get => createCommand; set => createCommand = value; }
        #endregion
        public CustomerViewModel()
        {
            CruDialog.DataContext = this;
            customerList = new ObservableCollection<Customer>(customerModel.getList());
            customerViewSource.Source = customerList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            createCommand = new RelayCommand<Customer>(p => true, create);
        }

        private void refreshView()
        {
            customerList = null;
            customerList = new ObservableCollection<Customer>(customerModel.getList());
            customerViewSource.Source = customerList;
            customerViewSource.View.Refresh();
        }

        private void create(Customer customer)
        {
            if (customerModel.create(customer))
            {
                this.BindingMessage(true, "Đã thêm thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không thêm được khách hàng");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private async void OpenCRUDialogEventAsync(object obj)
        {
            this.Action = "Thêm";
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }
    }
}
