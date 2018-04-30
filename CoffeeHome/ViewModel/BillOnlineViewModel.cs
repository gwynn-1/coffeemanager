using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    class BillOnlineViewModel:BaseViewModel
    {
        private ObservableCollection<Bill_Online> billOnlinelList;

        public ObservableCollection<Bill_Online> BillOnlineList
        {
            get => billOnlinelList;
            set
            {
                billOnlinelList = value;
                OnPropertyChanged("billOnlinelList");
            }
        }

        private BillOnlineModel billOnlineModel = new BillOnlineModel();

        public BillOnlineViewModel()
        {
            List<Bill_Online> billOnline = billOnlineModel.getList();
            BillOnlineList = new ObservableCollection<Bill_Online>(billOnline);
        }
    }
}
