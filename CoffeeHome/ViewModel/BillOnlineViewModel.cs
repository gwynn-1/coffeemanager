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
    class BillOnlineViewModel:BaseViewModel
    {
        #region VariableBinding
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

        private BillOnlineCRUDialog CruDialog = new BillOnlineCRUDialog();

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

        private CollectionViewSource billOnlineViewSource = new CollectionViewSource();
        public CollectionViewSource BillOnlineViewSource
        {
            get => billOnlineViewSource;
            set
            {
                billOnlineViewSource = value;
                OnPropertyChanged("billOnlineViewSource");
            }
        }
        #endregion

        #region ViewObject
        private Bill_Online billOnlineViewObject = new Bill_Online();
        public Bill_Online BillOnlineViewObject
        {
            get => billOnlineViewObject; set
            {
                billOnlineViewObject = value;
                OnPropertyChanged("billOnlineViewObject");
            }
        }
        #endregion

        #region Model
        private BillOnlineModel billOnlineModel = new BillOnlineModel();
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        private ICommand createCommand;
        public ICommand CreateCommand { get => createCommand; set => createCommand = value; }
        #endregion

        public BillOnlineViewModel()
        {
            CruDialog.DataContext = this;
            billOnlineList = new ObservableCollection<Bill_Online>(billOnlineModel.getList());
            billOnlineViewSource.Source = BillOnlineList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            createCommand = new RelayCommand<Bill_Online>(p => true, create);
        }

        private void refreshView()
        {
            billOnlineList = null;
            billOnlineList = new ObservableCollection<Bill_Online>(billOnlineModel.getList());
            billOnlineViewSource.Source = BillOnlineList;
            billOnlineViewSource.View.Refresh();
        }

        private void create(Bill_Online obj)
        {
            if (billOnlineModel.create(obj))
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
