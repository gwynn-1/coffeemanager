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

        private BillOnlineDetailCRUDialog CruDialog = new BillOnlineDetailCRUDialog();

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

        private ICommand createCommand;
        public ICommand CreateCommand { get => createCommand; set => createCommand = value; }

        public ICommand SelectionChangedCommand { get => selectionChangedCommand; set => selectionChangedCommand = value; }
        private ICommand selectionChangedCommand;

        private ICommand priceChangedCommand;
        public ICommand PriceChangedCommand { get => priceChangedCommand; set => priceChangedCommand = value; }
        #endregion

        public BillOnlineDetailViewModel()
        {
            CruDialog.DataContext = this;
            billOnlineDetaillList = new ObservableCollection<Bill_Online_Detail>(billOnlineDetailModel.getList());
            billOnlineList = new ObservableCollection<Bill_Online>(billOnlineModel.getList());
            drinkDessertList = new ObservableCollection<DrinkAndDessert>(drinkDessertModel.getList());
            BillDetailOnlineViewSource.Source = billOnlineDetaillList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            createCommand = new RelayCommand<Bill_Online_Detail>(p => true, create);
            selectionChangedCommand = new RelayCommand<object>(p => true, DrinkChanged);
            priceChangedCommand = new RelayCommand<object>(p => true, priceChanged);
        }

        private void refreshView()
        {
            BillOnlineDetailList = null;
            BillOnlineDetailList = new ObservableCollection<Bill_Online_Detail>(billOnlineDetailModel.getList());
            billDetailOnlineViewSource.Source = BillOnlineDetailList;
            billDetailOnlineViewSource.View.Refresh();
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

        private async void OpenCRUDialogEventAsync(object obj)
        {
            this.Action = "Thêm";
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }
    }
}
