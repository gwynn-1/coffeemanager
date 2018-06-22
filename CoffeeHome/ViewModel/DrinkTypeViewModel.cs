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
using CoffeeHome.TemplateView.DeleteTemplate;

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
        private DeleteDialog deleteDialog = new DeleteDialog();

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

        private ICommand openDeleteDialogCommand;
        public ICommand OpenDeleteDialogCommand { get => openDeleteDialogCommand; set => openDeleteDialogCommand = value; }

        private ICommand submitCommand;
        public ICommand SubmitCommand { get => submitCommand; set => submitCommand = value; }

        #endregion

        public DrinkTypeViewModel()
        {
            drinkTypeList = new ObservableCollection<Drink_type>(drinkTypeModel.getList());
            drinkTypeViewSource.Source = drinkTypeList;

            OpenCruDialogCommand = new RelayCommand<object>(p=>true,OpenCRUDialogEventAsync);
            OpenDeleteDialogCommand = new RelayCommand<object>(p => true, openDeleteDialog);
            submitCommand = new RelayCommand<Drink_type>(p => true, submit);
            CruDialog.DataContext = this;
        }

        private async void openDeleteDialog(object obj)
        {
            deleteDialog.DataContext = this;
            this.Action = obj.ToString();
            var result = await DialogHost.Show(deleteDialog, "RootDialog");
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
            
            if (obj == null)
            {
                this.Action = "Thêm";
                DrinkTypeViewObject = new Drink_type();
            }
            else
            {
                DrinkTypeViewObject = null;
                this.Action = "Sửa";
                DrinkTypeViewObject = drinkTypeModel.getDrinkTypeByID((int)obj);
            }
            var result = await DialogHost.Show(CruDialog, "RootDialog");
        }

        private void submit(Drink_type drink_Type)
        {
            if (this.Action == "Thêm")
            {
                Create(drink_Type);
            }
            else if (this.Action == "Sửa")
            {
                update(drink_Type);
            }
            else
            {
                delete();
            }
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
                this.BindingMessage(false, "Không thêm được loại đồ uống");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void delete()
        {
            if (drinkTypeModel.delete(int.Parse(this.Action)))
            {
                this.BindingMessage(true, "Đã xóa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không xóa được loại đồ uống");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }

        private void update(Drink_type obj)
        {
            if (drinkTypeModel.update(obj,DrinkTypeViewObject.id_type))
            {
                this.BindingMessage(true, "Đã sửa thành công");
                refreshView();
            }
            else
            {
                this.BindingMessage(false, "Không sửa được loại đồ uống");
            }
            DialogHost.CloseDialogCommand.Execute(new object(), null);
        }
    }
}
