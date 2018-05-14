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
    class TableViewModel:BaseViewModel
    {
        #region VariableBinding
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

        private CollectionViewSource tableViewSource = new CollectionViewSource();
        public CollectionViewSource TableViewSource
        {
            get => tableViewSource;
            set
            {
                tableViewSource = value;
                OnPropertyChanged("tableViewSource");
            }
        }
        private TableCRUDialog CruDialog = new TableCRUDialog();
        
        #endregion

        #region Model
        private TableModel tableModel = new TableModel();
        #endregion

        #region ViewObject
        private Table tableViewObject = new Table();
        public Table TableViewObject
        {
            get => tableViewObject; set
            {
                tableViewObject = value;
                OnPropertyChanged("tableViewObject");
            }
        }
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        private ICommand createCommand;
        public ICommand CreateCommand { get => createCommand; set => createCommand = value; }
        #endregion

        public TableViewModel()
        {
            CruDialog.DataContext = this;
            tableList = new ObservableCollection<Table>(tableModel.getList());
            tableViewSource.Source = tableList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, OpenCRUDialogEventAsync);
            createCommand = new RelayCommand<Table>(p => true, create);
        }

        private void create(Table obj)
        {
            if (tableModel.create(obj))
            {
                this.BindingMessage(true, "Đã thêm thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không thêm được bàn");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void refreshView()
        {
            tableList = null;
            tableList = new ObservableCollection<Table>(tableModel.getList());
            tableViewSource.Source = tableList;
            tableViewSource.View.Refresh();
        }

        private async void OpenCRUDialogEventAsync(object obj)
        {
            this.Action = "Thêm";
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }
    }
}
