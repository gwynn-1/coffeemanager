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

        private List<string> arr_field_filter = new List<string>(new string[] { "ID", "Tên Khách hàng" });
        public List<string> Arr_field_filter
        {
            get => arr_field_filter;
            set
            {
                arr_field_filter = value;
                OnPropertyChanged("arr_field_filter");
            }
        }

        private string[] textFilter;
        public string[] TextFilter
        {
            get => textFilter;
            set
            {
                textFilter = value;
                OnPropertyChanged("textFilter");
            }
        }

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

        private ICommand submitFilterCommand;
        public ICommand SubmitFilterCommand { get => submitFilterCommand; set => submitFilterCommand = value; }
        #endregion
        public CustomerViewModel()
        {
            CruDialog.DataContext = this;
            customerList = new ObservableCollection<Customer>(customerModel.getList());
            customerViewSource.Source = customerList;
            customerViewSource.View.Filter = Filter;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            OpenDeleteDialogCommand = new RelayCommand<object>(p => true, openDeleteDialog);
            submitCommand = new RelayCommand<Customer>(p => true, submit);
            submitFilterCommand = new RelayCommand<List<object>>(p => true, submitFilter);
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

        private bool Filter(object item)
        {
            if (TextFilter != null)
            {
                if (String.IsNullOrEmpty(TextFilter[0]))
                {
                    return true;
                }
                else
                {
                    if (TextFilter[1] == "Tên Khách hàng")
                        return ((item as Customer).name.IndexOf(TextFilter[0], StringComparison.OrdinalIgnoreCase) >= 0);
                    else if (TextFilter[1] == "ID")
                        return ((item as Customer).id_customer.ToString().IndexOf(TextFilter[0], StringComparison.OrdinalIgnoreCase) >= 0);
                    else
                        return true;
                }
            }
            else
                return true;
        }

        private void submitFilter(List<object> filter)
        {
            TextFilter = filter.Where(x => x != null)
                       .Select(x => x.ToString())
                       .ToArray();
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
