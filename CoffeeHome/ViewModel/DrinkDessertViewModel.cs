using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    class DrinkDessertViewModel:BaseViewModel
    {
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

        private DrinkDessertModel drinkDessertModel = new DrinkDessertModel();

        public DrinkDessertViewModel()
        {
            List<DrinkAndDessert> drink_dessert = drinkDessertModel.getList();
            DrinkDessertList = new ObservableCollection<DrinkAndDessert>(drink_dessert);
        }
    }
}
