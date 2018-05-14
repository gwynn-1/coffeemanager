using CoffeeHome.Model;
using CoffeeHome.TemplateView.CRUTemplate;
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
        private Bill_details billDetailtViewObject = new Bill_details();
        public Bill_details BillDetailViewObject
        {
            get => billDetailtViewObject; set
            {
                billDetailtViewObject = value;
                OnPropertyChanged("billDetailtViewObject");
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

        private ICommand createCommand;
        public ICommand CreateCommand { get => createCommand; set => createCommand = value; }

        public ICommand SelectionChangedCommand { get => selectionChangedCommand; set => selectionChangedCommand = value; }
        private ICommand selectionChangedCommand;

        private ICommand priceChangedCommand;
        public ICommand PriceChangedCommand { get => priceChangedCommand; set => priceChangedCommand = value; }
        #endregion

        public BillDetailViewModel()
        {
            CruDialog.DataContext = this;
            BillDetailList = new ObservableCollection<Bill_details>(billDetailModel.getList());
            drinkDessertList = new ObservableCollection<DrinkAndDessert>(drinkDessertModel.getList());
            billList = new ObservableCollection<Bill>(billModel.getList());
            BillDetailViewSource.Source = billDetailList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            createCommand = new RelayCommand<Bill_details>(p => true, create);
            selectionChangedCommand = new RelayCommand<object>(p=>true,DrinkChanged);
            priceChangedCommand = new RelayCommand<object>(p => true,priceChanged);
        }

        private void refreshView()
        {
            BillDetailList = null;
            BillDetailList = new ObservableCollection<Bill_details>(billDetailModel.getList());
            BillDetailViewSource.Source = BillDetailList;
            BillDetailViewSource.View.Refresh();
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
            DrinkIdBinding = int.Parse(obj.ToString());
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

        private async void OpenCRUDialogEventAsync(object obj)
        {
            this.Action = "Thêm";
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }
    }
}
