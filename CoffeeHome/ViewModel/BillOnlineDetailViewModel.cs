using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    class BillOnlineDetailViewModel:BaseViewModel
    {
        private ObservableCollection<Bill_Online_Detail> billOnlineDetaillList;

        public ObservableCollection<Bill_Online_Detail> BillOnlineDetailList
        {
            get => billOnlineDetaillList;
            set
            {
                billOnlineDetaillList = value;
                OnPropertyChanged("billOnlineDetaillList");
            }
        }

        private BillOnlineDetailModel billOnlineDetailModel = new BillOnlineDetailModel();

        public BillOnlineDetailViewModel()
        {
            List<Bill_Online_Detail> billOnline = billOnlineDetailModel.getList();
            BillOnlineDetailList = new ObservableCollection<Bill_Online_Detail>(billOnline);
        }
    }
}
