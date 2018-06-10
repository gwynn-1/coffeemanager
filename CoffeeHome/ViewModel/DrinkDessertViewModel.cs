using CoffeeHome.Model;
using CoffeeHome.TemplateView.CRUTemplate;
using CoffeeHome.TemplateView.DeleteTemplate;
using CoffeeHome.Vendor.UploadFile.Source;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CoffeeHome.ViewModel
{
    class DrinkDessertViewModel:BaseViewModel
    {
        #region VariableBinding
        public string pathProject { get; set; }

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

        private ObservableCollection<Drink_type> drinkType;
        public ObservableCollection<Drink_type> DrinkType { get => drinkType;
            set
            {
                drinkType = value;
                OnPropertyChanged("drinkType");
            }
        }

        private FoodDrinkCRUDialog CruDialog = new FoodDrinkCRUDialog();
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

        private CollectionViewSource drinkDessertViewSource = new CollectionViewSource();
        public CollectionViewSource DrinkDessertViewSource
        {
            get => drinkDessertViewSource;
            set
            {
                drinkDessertViewSource = value;
                OnPropertyChanged("drinkDessertViewSource");
            }
        }
        
        #endregion

        #region Model
        private DrinkDessertModel drinkDessertModel = new DrinkDessertModel();
        private DrinkTypeModel drinkTypeModel = new DrinkTypeModel();
        #endregion

        #region ViewObject
        private DrinkAndDessert drinkDessertViewObject = new DrinkAndDessert();
        public DrinkAndDessert DrinkDessertViewObject
        {
            get => drinkDessertViewObject; set
            {
                drinkDessertViewObject = value;
                OnPropertyChanged("drinkDessertViewObject");
            }
        }
        #endregion

        #region Command
        public ICommand OpenCruDialogCommand { get => openCruDialogCommand; set => openCruDialogCommand = value; }
        private ICommand openCruDialogCommand;

        private ICommand submitCommand;
        public ICommand SubmitCommand { get => submitCommand; set => submitCommand = value; }
        
        private ICommand closeDialogCommand;
        public ICommand CloseDialogCommand { get => closeDialogCommand; set => closeDialogCommand = value; }

        private ICommand openDeleteDialogCommand;
        public ICommand OpenDeleteDialogCommand { get => openDeleteDialogCommand; set => openDeleteDialogCommand = value; }
        #endregion

        public DrinkDessertViewModel()
        {
            CruDialog.DataContext = this;
            DrinkDessertList = new ObservableCollection<DrinkAndDessert>(drinkDessertModel.getList());
            DrinkType = new ObservableCollection<Drink_type>( drinkTypeModel.getList());
            drinkDessertViewSource.Source = drinkDessertList;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, openCRUDialogEventAsync);
            submitCommand = new RelayCommand<DrinkAndDessert>(p=>true, submit);
            closeDialogCommand = new RelayCommand<object>(p=>true,closeDialog);
            OpenDeleteDialogCommand = new RelayCommand<object>(p => true,openDeleteDialog);
            pathProject = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        }

        private async void openDeleteDialog(object obj)
        {
            deleteDialog.DataContext = this;
            this.Action = obj.ToString();
            var result = await DialogHost.Show(deleteDialog, "RootDialog");
        }

        private void closeDialog(object obj)
        {
            
            DrinkDessertViewObject = new DrinkAndDessert();
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void refreshView()
        {
            drinkDessertList = null;
            drinkDessertList = new ObservableCollection<DrinkAndDessert>(drinkDessertModel.getList());
            drinkDessertViewSource.Source = drinkDessertList;
            drinkDessertViewSource.View.Refresh();
        }

        private async void openCRUDialogEventAsync(object obj)
        {
            DrinkDessertViewObject = null;
            if (obj == null)
            {
                this.Action = "Thêm";
            }
            else
            {
                this.Action = "Sửa";
                DrinkDessertViewObject = drinkDessertModel.getDrinkByID((int)obj);
            }
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }

        private void submit(DrinkAndDessert drinkAndDessert)
        {
            if(this.Action == "Thêm")
            {
                create(drinkAndDessert);
            }
            else if(this.Action == "Sửa")
            {
                update(drinkAndDessert);
            }
            else
            {
                delete();
            }
        }

        private void delete()
        {
            if (drinkDessertModel.delete(int.Parse(this.Action)))
            {
                this.BindingMessage(true, "Đã xóa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không xóa được đồ uống");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void update(DrinkAndDessert drinkAndDessert)
        {
            drinkAndDessert.image = UploadFile.UploadFileToUploads(drinkAndDessert.image, drinkAndDessert.name);
            if (drinkDessertModel.update(drinkAndDessert, DrinkDessertViewObject.id_drink))
            {
                this.BindingMessage(true, "Đã sửa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không sửa được đồ uống");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void create(DrinkAndDessert drinkAndDessert)
        {
            drinkAndDessert.image= UploadFile.UploadFileToUploads(drinkAndDessert.image,drinkAndDessert.name);
            if (drinkDessertModel.create(drinkAndDessert))
            {
                this.BindingMessage(true,"Đã thêm thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false,"Không thêm được đồ uống");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }
    }
}
