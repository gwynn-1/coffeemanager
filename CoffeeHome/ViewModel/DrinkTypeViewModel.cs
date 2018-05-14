using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using CoffeeHome.TemplateView.CRUTemplate;
using System.Windows.Controls;
using System.Windows.Data;

namespace CoffeeHome.ViewModel
{
    public class DrinkTypeViewModel : BaseViewModel
    {
        #region VariableBinding
        private ObservableCollection<Drink_type> drinkTypeList;
        public ObservableCollection<Drink_type> DrinkTypeList {
            get => drinkTypeList;
            set
            {
                drinkTypeList = value;
                OnPropertyChanged("drinkTypeList");
            }
        }

        private CollectionViewSource drinkTypeViewSource =new CollectionViewSource();
        public CollectionViewSource DrinkTypeViewSource { get => drinkTypeViewSource;
            set
            {
                drinkTypeViewSource = value;
                OnPropertyChanged("drinkTypeViewSource");
            }
        }

        private DrinkTypeCRUDialog CruDialog = new DrinkTypeCRUDialog();

        private string action;
        public string Action { get => action;
            set
            {
                action = value;
                OnPropertyChanged("action");
            }
        }
        #endregion

        #region ViewObject
        private Drink_type drinkTypeViewObject =new Drink_type();
        public Drink_type DrinkTypeViewObject
        {
            
            get => drinkTypeViewObject;
            set
            {
                drinkTypeViewObject = value;
                OnPropertyChanged("drinkTypeViewObject");
            }
        }
        #endregion

        #region Model
        private DrinkTypeModel drinkTypeModel = new DrinkTypeModel();
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        private ICommand createCommand;
        public ICommand CreateCommand { get => createCommand; set => createCommand = value; }

        #endregion

        public DrinkTypeViewModel()
        {
            drinkTypeList = new ObservableCollection<Drink_type>(drinkTypeModel.getList());
            drinkTypeViewSource.Source = drinkTypeList;

            OpenCruDialogCommand = new RelayCommand<object>(p=>true,OpenCRUDialogEventAsync);
            CreateCommand = new RelayCommand<Drink_type>(p=>true,Create);
            CruDialog.DataContext = this;
        }

        private void refreshView()
        {
            drinkTypeList = null;
            drinkTypeList = new ObservableCollection<Drink_type>(drinkTypeModel.getList());
            drinkTypeViewSource.Source = drinkTypeList;
            drinkTypeViewSource.View.Refresh();
        }

        private async void OpenCRUDialogEventAsync(object obj)
        {
            this.Action = "Thêm";
            var result = await DialogHost.Show(CruDialog,"RootDialog");
        }

        private void Create(Drink_type obj)
        {
            if (drinkTypeModel.create(obj))
            {
                this.BindingMessage(true, "Đã thêm thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không thêm được loại món Ăn");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }
    }
}
