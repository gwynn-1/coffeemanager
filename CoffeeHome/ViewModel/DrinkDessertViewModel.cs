﻿using CoffeeHome.Model;
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

        private List<string> arr_field_filter = new List<string>(new string[] { "ID", "Tên Món Ăn" });
        public List<string> Arr_field_filter
        {
            get => arr_field_filter;
            set
            {
                arr_field_filter = value;
                OnPropertyChanged("arr_field_filter");
            }
        }

        private string[] textFilter;
        public string[] TextFilter
        {
            get => textFilter;
            set
            {
                textFilter = value;
                OnPropertyChanged("textFilter");
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

        private ICommand submitFilterCommand;
        public ICommand SubmitFilterCommand { get => submitFilterCommand; set => submitFilterCommand = value; }
        #endregion

        public DrinkDessertViewModel()
        {
            CruDialog.DataContext = this;
            DrinkDessertList = new ObservableCollection<DrinkAndDessert>(drinkDessertModel.getList());
            DrinkType = new ObservableCollection<Drink_type>( drinkTypeModel.getList());
            drinkDessertViewSource.Source = drinkDessertList;
            drinkDessertViewSource.View.Filter = Filter;

            OpenCruDialogCommand = new RelayCommand<object>(p => true, openCRUDialogEventAsync);
            submitCommand = new RelayCommand<DrinkAndDessert>(p=>true, submit);
            closeDialogCommand = new RelayCommand<object>(p=>true,closeDialog);
            OpenDeleteDialogCommand = new RelayCommand<object>(p => true,openDeleteDialog);

            submitFilterCommand = new RelayCommand<List<object>>(p => true, submitFilter);
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

        private bool Filter(object item)
        {
            if (TextFilter != null)
            {
                if (String.IsNullOrEmpty(TextFilter[0]))
                {
                    return true;
                }
                else
                {
                    if (TextFilter[1] == "Tên Món Ăn")
                        return ((item as DrinkAndDessert).name.IndexOf(TextFilter[0], StringComparison.OrdinalIgnoreCase) >= 0);
                    else if (TextFilter[1] == "ID")
                        return ((item as DrinkAndDessert).id_drink.ToString().IndexOf(TextFilter[0], StringComparison.OrdinalIgnoreCase) >= 0);
                    else
                        return true;
                }
            }
            else
                return true;
        }

        private void submitFilter(List<object> filter)
        {
            TextFilter = filter.Where(x => x != null)
                       .Select(x => x.ToString())
                       .ToArray();
           drinkDessertViewSource.View.Refresh();
        }

        private async void openCRUDialogEventAsync(object obj)
        {
            
            if (obj == null)
            {
                this.Action = "Thêm";
                DrinkDessertViewObject = new DrinkAndDessert();
            }
            else
            {
                this.Action = "Sửa";
                DrinkDessertViewObject = null;
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
