using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    public class DrinkTypeViewModel : BaseViewModel
    {
        private ObservableCollection<Drink_type> drinkTypeList;

        public ObservableCollection<Drink_type> DrinkTypeList {
            get => drinkTypeList;
            set {
                drinkTypeList = value;
                OnPropertyChanged("drinkTypeList");
            }
        }

        private DrinkTypeModel drinkTypeModel = new DrinkTypeModel();

        public DrinkTypeViewModel()
        {
            List<Drink_type> drink_s = drinkTypeModel.getList();
            DrinkTypeList = new ObservableCollection<Drink_type>(drink_s);
        }

    }
}
