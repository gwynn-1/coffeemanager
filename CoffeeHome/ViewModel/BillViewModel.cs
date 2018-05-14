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
    class BillViewModel:BaseViewModel
    {
        #region BindingVariable
        private ObservableCollection<Bill> billList;
        public ObservableCollection<Bill> BillList
        {
            get => billList;
            set
            {
                billList = value;
                OnPropertyChanged("billList");
            }
        }

        private ObservableCollection<Customer> customerList;
        public ObservableCollection<Customer> CustomerList { get => customerList;
            set
            {
                customerList = value;
                OnPropertyChanged("customerList");
            }
        }

        private ObservableCollection<Table> tableList;
        public ObservableCollection<Table> TableList
        {
            get => tableList;
            set
            {
                tableList = value;
                OnPropertyChanged("tableList");
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

        private BillCRUDialog CruDialog = new BillCRUDialog();

        private CollectionViewSource billViewSource = new CollectionViewSource();
        public CollectionViewSource BillViewSource
        {
            get => billViewSource;
            set
            {
                billViewSource = value;
                OnPropertyChanged("billViewSource");
            }
        }
        #endregion

        #region ViewObject
        private Bill billViewObject = new Bill();
        public Bill BillViewObject
        {
            get => billViewObject; set
            {
                billViewObject = value;
                OnPropertyChanged("billViewObject");
            }
        }
        #endregion

        #region Model
        private BillModel billModel = new BillModel();
        private CustomerModel customerModel = new CustomerModel();
        private TableModel tableModel = new TableModel();
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        private ICommand createCommand;
        public ICommand CreateCommand { get => createCommand; set => createCommand = value; }
        
        #endregion

        public BillViewModel()
        {
            CruDialog.DataContext = this;
            billList = new ObservableCollection<Bill>(billModel.getList());
            customerList = new ObservableCollection<Customer>(customerModel.getList());
            tableList = new ObservableCollection<Table>(tableModel.getList());
            billViewSource.Source = billList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            createCommand = new RelayCommand<Bill>(p => true, create);
        }

        private void refreshView()
        {
            billList = null;
            billList = new ObservableCollection<Bill>(billModel.getList());
            billViewSource.Source = BillList;
            billViewSource.View.Refresh();
        }

        private void create(Bill obj)
        {
            if (billModel.create(obj))
            {
                this.BindingMessage(true, "Đã thêm thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không thêm được đồ uống");
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
