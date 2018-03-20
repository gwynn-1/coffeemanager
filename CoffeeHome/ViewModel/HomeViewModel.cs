using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeHome.TemplateView;
using System.Collections.ObjectModel;

namespace CoffeeHome.ViewModel
{
    class HomeViewModel : BaseViewModel
    {
        //private ItemField[] Items { get; }
        public ObservableCollection<ItemField> Items { get; }
        public HomeViewModel()
        {
            this.Items = new ObservableCollection<ItemField>(){
                  new ItemField("Thức uống & Đồ ăn",new DrinkAndFoodTemplate(),"Images/drink-food.png"),
                  new ItemField("Loại thức uống",new DrinkTypeTemplate(),"Images/drink-type.png")
            };
        }
    }
}
