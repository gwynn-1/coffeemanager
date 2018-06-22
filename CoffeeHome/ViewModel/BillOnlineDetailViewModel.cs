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
    class BillOnlineDetailViewModel:BaseViewModel
    {
        #region VariableBinding
        private ObservableCollection<Bill_Online_Detail> billOnlineDetaillList;
        public ObservableCollection<Bill_Online_Detail> BillOnlineDetailList
        {
            get => billOnlineDetaillList;
            set
            {
                billOnlineDetaillList = value;
                OnPropertyChanged("billOnlineDetaillList");
            }
        }

        private ObservableCollection<Bill_Online> billOnlineList;
        public ObservableCollection<Bill_Online> BillOnlineList
        {
            get => billOnlineList;
            set
            {
                billOnlineList = value;
                OnPropertyChanged("billOnlineList");
            }
        }

        private ObservableCollection<DrinkAndDessert> drinkDessertList;
        public ObservableCollection<DrinkAndDessert> DrinkDessertList
        {
            get => drinkDessertList;
            set
            {
                drinkDessertList = value;
                OnPropertyChanged("drinkDessertList");
            }
        }

        private List<object> listId;

        private bool isDisableCombobox;
        public bool IsDisableCombobox
        {
            get => isDisableCombobox;
            set
            {
                isDisableCombobox = value;
                OnPropertyChanged("isDisableCombobox");
            }
        }

        private BillOnlineDetailCRUDialog CruDialog = new BillOnlineDetailCRUDialog();
        private DeleteDialog deleteDialog = new DeleteDialog();

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

        private CollectionViewSource billDetailOnlineViewSource = new CollectionViewSource();
        public CollectionViewSource BillDetailOnlineViewSource
        {
            get => billDetailOnlineViewSource;
            set
            {
                billDetailOnlineViewSource = value;
                OnPropertyChanged("billDetailOnlineViewSource");
            }
        }

        private string priceBinding = "";
        public string PriceBinding
        {
            get => priceBinding;
            set
            {
                priceBinding = value;
                OnPropertyChanged("priceBinding");
            }
        }

        private int DrinkIdBinding;
        #endregion

        #region ViewObject
        private Bill_Online_Detail billDetailOnlineViewObject = new Bill_Online_Detail();
        public Bill_Online_Detail BillDetailOnlineViewObject
        {
            get => billDetailOnlineViewObject; set
            {
                billDetailOnlineViewObject = value;
                OnPropertyChanged("billDetailOnlineViewObject");
            }
        }
        #endregion

        #region Model
        private BillOnlineDetailModel billOnlineDetailModel = new BillOnlineDetailModel();
        private BillOnlineModel billOnlineModel = new BillOnlineModel();
        private DrinkDessertModel drinkDessertModel = new DrinkDessertModel();
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        //private ICommand createCommand;
        //public ICommand CreateCommand { get => createCommand; set => createCommand = value; }

        private ICommand submitCommand;
        public ICommand SubmitCommand { get => submitCommand; set => submitCommand = value; }

        public ICommand SelectionChangedCommand { get => selectionChangedCommand; set => selectionChangedCommand = value; }
        private ICommand selectionChangedCommand;

        private ICommand priceChangedCommand;
        public ICommand PriceChangedCommand { get => priceChangedCommand; set => priceChangedCommand = value; }

        private ICommand openDeleteDialogCommand;
        public ICommand OpenDeleteDialogCommand { get => openDeleteDialogCommand; set => openDeleteDialogCommand = value; }
        #endregion

        public BillOnlineDetailViewModel()
        {
            CruDialog.DataContext = this;
            IsDisableCombobox = true;
            billOnlineDetaillList = new ObservableCollection<Bill_Online_Detail>(billOnlineDetailModel.getList());
            billOnlineList = new ObservableCollection<Bill_Online>(billOnlineModel.getList());
            drinkDessertList = new ObservableCollection<DrinkAndDessert>(drinkDessertModel.getList());
            BillDetailOnlineViewSource.Source = billOnlineDetaillList;

            OpenCruDialogCommand = new RelayCommand<List<object>>(p => true, OpenCRUDialogEventAsync);
            //createCommand = new RelayCommand<Bill_Online_Detail>(p => true, create);
            submitCommand = new RelayCommand<Bill_Online_Detail>(p => true, submit);
            selectionChangedCommand = new RelayCommand<object>(p => true, DrinkChanged);
            priceChangedCommand = new RelayCommand<object>(p => true, priceChanged);
            OpenDeleteDialogCommand = new RelayCommand<List<object>>(p => true, openDeleteDialog);
        }

        private void refreshView()
        {
            BillOnlineDetailList = null;
            BillOnlineDetailList = new ObservableCollection<Bill_Online_Detail>(billOnlineDetailModel.getList());
            BillDetailOnlineViewSource.Source = BillOnlineDetailList;
            BillDetailOnlineViewSource.View.Refresh();
        }

        private async void openDeleteDialog(List<object> obj)
        {
            deleteDialog.DataContext = this;
            this.Action = null;
            this.listId = obj;
            var result = await DialogHost.Show(deleteDialog, "RootDialog");
        }

        private void priceChanged(object obj)
        {
            if (obj != null && obj.ToString() != "")
                PriceBinding = (drinkDessertModel.getPriceDrink(DrinkIdBinding) * int.Parse(obj.ToString())).ToString();
            else
                PriceBinding = "";
        }

        private void submit(Bill_Online_Detail obj)
        {
            if (this.Action == "Thêm")
            {
                create(obj);
            }
            else if (this.Action == "Sửa")
            {
                update(obj);
            }
            else
            {
                delete();
            }
        }

        private void DrinkChanged(object obj)
        {
            if (obj != null)
                DrinkIdBinding = int.Parse(obj.ToString());
        }

        private void create(Bill_Online_Detail obj)
        {
            if (billOnlineDetailModel.create(obj))
            {
                this.BindingMessage(true, "Đã thêm thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không thêm được chi tiết hóa đơn");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void update(Bill_Online_Detail obj)
        {
            if (billOnlineDetailModel.update(obj, BillDetailOnlineViewObject.id_bill, BillDetailOnlineViewObject.id_drink))
            {
                this.BindingMessage(true, "Đã sửa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không sửa được chi tiết hóa đơn");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void delete()
        {
            if (billOnlineDetailModel.delete((int)this.listId[0], (int)this.listId[1]))
            {
                this.BindingMessage(true, "Đã xóa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không xóa được chi tiết hóa đơn");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private async void OpenCRUDialogEventAsync(List<object> obj)
        {
            BillDetailOnlineViewObject = null;
            if (obj == null)
            {
                this.Action = "Thêm";
                BillDetailOnlineViewObject = new Bill_Online_Detail();
                IsDisableCombobox = true;
            }
            else
            {
                this.Action = "Sửa";
                IsDisableCombobox = false;
                BillDetailOnlineViewObject = billOnlineDetailModel.getDetailByID((int)obj[0], (int)obj[1]);
                PriceBinding = BillDetailOnlineViewObject.price.ToString();
            }
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }
    }
}
