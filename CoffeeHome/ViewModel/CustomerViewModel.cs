using CoffeeHome.Model;
using CoffeeHome.TemplateView.CRUTemplate;
using CoffeeHome.TemplateView.DeleteTemplate;
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
        private DeleteDialog deleteDialog = new DeleteDialog();

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

        private ICommand openDeleteDialogCommand;
        public ICommand OpenDeleteDialogCommand { get => openDeleteDialogCommand; set => openDeleteDialogCommand = value; }

        private ICommand submitCommand;
        public ICommand SubmitCommand { get => submitCommand; set => submitCommand = value; }
        #endregion
        public CustomerViewModel()
        {
            CruDialog.DataContext = this;
            customerList = new ObservableCollection<Customer>(customerModel.getList());
            customerViewSource.Source = customerList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            OpenDeleteDialogCommand = new RelayCommand<object>(p => true, openDeleteDialog);
            submitCommand = new RelayCommand<Customer>(p => true, submit);
        }

        private async void openDeleteDialog(object obj)
        {
            deleteDialog.DataContext = this;
            this.Action = obj.ToString();
            var result = await DialogHost.Show(deleteDialog, "RootDialog");
        }

        private void refreshView()
        {
            customerList = null;
            customerList = new ObservableCollection<Customer>(customerModel.getList());
            customerViewSource.Source = customerList;
            customerViewSource.View.Refresh();
        }

        private void submit(Customer customer)
        {
            if (this.Action == "Thêm")
            {
                create(customer);
            }
            else if (this.Action == "Sửa")
            {
                update(customer);
            }
            else
            {
                delete();
            }
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

        private void update(Customer obj)
        {
            if (customerModel.update(obj, CustomerViewObject.id_customer))
            {
                this.BindingMessage(true, "Đã sửa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không sửa được loại món Ăn");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void delete()
        {
            if (customerModel.delete(int.Parse(this.Action)))
            {
                this.BindingMessage(true, "Đã xóa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không xóa được khách hàng");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private async void OpenCRUDialogEventAsync(object obj)
        {
            
            if (obj == null)
            {
                this.Action = "Thêm";
                CustomerViewObject = new Customer();
            }
            else
            {
                this.Action = "Sửa";
                CustomerViewObject = null;
                CustomerViewObject = customerModel.getCustomerByID((int)obj);
            }
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }
    }
}
