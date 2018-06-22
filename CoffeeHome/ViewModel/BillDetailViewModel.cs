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
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CoffeeHome.ViewModel
{
    class BillDetailViewModel:BaseViewModel
    {
        #region BindingVariable
        private ObservableCollection<Bill_details> billDetailList;
        public ObservableCollection<Bill_details> BillDetailList
        {
            get => billDetailList;
            set
            {
                billDetailList = value;
                OnPropertyChanged("billDetailList");
            }
        }

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

        private BillDetailCRUDialog CruDialog = new BillDetailCRUDialog();

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

        private List<object> listId;

        private bool isDisableCombobox;
        public bool IsDisableCombobox {
            get => isDisableCombobox;
            set
            {
                isDisableCombobox = value;
                OnPropertyChanged("isDisableCombobox");
            }
        }

        private CollectionViewSource billDetailViewSource = new CollectionViewSource();
        public CollectionViewSource BillDetailViewSource
        {
            get => billDetailViewSource;
            set
            {
                billDetailViewSource = value;
                OnPropertyChanged("billDetailViewSource");
            }
        }
        private DeleteDialog deleteDialog = new DeleteDialog();

        private string priceBinding = "";
        public string PriceBinding { get => priceBinding;
            set {
                priceBinding = value;
                OnPropertyChanged("priceBinding");
            }
        }

        private int DrinkIdBinding;
        #endregion

        #region ViewObject
        private Bill_details billDetailViewObject = new Bill_details();
        public Bill_details BillDetailViewObject
        {
            get => billDetailViewObject; set
            {
                billDetailViewObject = value;
                OnPropertyChanged("billDetailViewObject");
            }
        }
        #endregion

        #region Model
        private BillDetailModel billDetailModel = new BillDetailModel();
        private BillModel billModel = new BillModel();
        private DrinkDessertModel drinkDessertModel = new DrinkDessertModel();
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        private ICommand submitCommand;
        public ICommand SubmitCommand { get => submitCommand; set => submitCommand = value; }

        public ICommand SelectionChangedCommand { get => selectionChangedCommand; set => selectionChangedCommand = value; }
        private ICommand selectionChangedCommand;

        private ICommand priceChangedCommand;
        public ICommand PriceChangedCommand { get => priceChangedCommand; set => priceChangedCommand = value; }
        private ICommand openDeleteDialogCommand;
        public ICommand OpenDeleteDialogCommand { get => openDeleteDialogCommand; set => openDeleteDialogCommand = value; }
        
        #endregion

        public BillDetailViewModel()
        {
            CruDialog.DataContext = this;
            IsDisableCombobox = true;
            BillDetailList = new ObservableCollection<Bill_details>(billDetailModel.getList());
            drinkDessertList = new ObservableCollection<DrinkAndDessert>(drinkDessertModel.getList());
            billList = new ObservableCollection<Bill>(billModel.getList());
            BillDetailViewSource.Source = billDetailList;

            OpenCruDialogCommand = new RelayCommand<List<object>>(p => true, OpenCRUDialogEventAsync);
            submitCommand = new RelayCommand<Bill_details>(p => true, submit);
            selectionChangedCommand = new RelayCommand<object>(p=>true,DrinkChanged);
            priceChangedCommand = new RelayCommand<object>(p => true,priceChanged);
            OpenDeleteDialogCommand = new RelayCommand<List<object>>(p => true, openDeleteDialog);
        }

        private void refreshView()
        {
            BillDetailList = null;
            BillDetailList = new ObservableCollection<Bill_details>(billDetailModel.getList());
            BillDetailViewSource.Source = BillDetailList;
            BillDetailViewSource.View.Refresh();
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

        private void DrinkChanged(object obj)
        {
            if(obj!=null)
                DrinkIdBinding = int.Parse(obj.ToString());
        }

        private void submit(Bill_details obj)
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

        private void create(Bill_details obj)
        {
            if (billDetailModel.create(obj))
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

        private void update(Bill_details obj)
        {
            if (billDetailModel.update(obj, BillDetailViewObject.id_bill, BillDetailViewObject.id_food))
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
            if (billDetailModel.delete((int)this.listId[0],(int) this.listId[1]))
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
            
            if (obj == null)
            {
                this.Action = "Thêm";
                BillDetailViewObject = new Bill_details();
                IsDisableCombobox = true;
            }
            else
            {
                this.Action = "Sửa";
                IsDisableCombobox = false;
                BillDetailViewObject = null;
                BillDetailViewObject = billDetailModel.getDetailByID((int)obj[0],(int)obj[1]);
                PriceBinding = BillDetailViewObject.price.ToString();
            }
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }
    }
}
