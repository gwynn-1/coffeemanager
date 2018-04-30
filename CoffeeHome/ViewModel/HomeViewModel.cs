using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeHome.TemplateView;
using System.Collections.ObjectModel;

namespace CoffeeHome.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<ItemField> Items { get; }

        public AdminTemplate template = new AdminTemplate();
        public HomeViewModel()
        {
            this.Items = new ObservableCollection<ItemField>(){
                  new ItemField("Đồ uống",this.template,new FoodDrinkTemplate(),"Asset/Images/drink-food.png"),
                  new ItemField("Loại đồ uống",this.template,new DrinkTypeTemplate(),"Asset/Images/drink-type.png"),
                  new ItemField("Thành viên",this.template,new CustomerTemplate(),"Asset/Images/customer.png"),
                  new ItemField("Bàn",this.template,new TableTemplate(),"Asset/Images/table.png"),
                  new ItemField("Hóa đơn",this.template,new BillTemplate(),"Asset/Images/bill.png"),
                  new ItemField("Chi tiết Hóa đơn",this.template,new BillDetailTemplate(),"Asset/Images/bill_detail.png"),
                  new ItemField("Hóa đơn Online",this.template,new BillOnlineTemplate(),"Asset/Images/bill_online.png"),
                  new ItemField("Chi tiết HĐ Online",this.template,new BillOnlineDetailTemplate(),"Asset/Images/bill_online_detail.png")
            };
        }
    }
}
