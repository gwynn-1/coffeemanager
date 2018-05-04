using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    class BillDetailViewModel:BaseViewModel
    {
        private ObservableCollection<Bill_details> billDetailList;

        public ObservableCollection<Bill_details> BillDetailList
        {
            get => billDetailList;
            set
            {
                billDetailList = value;
                OnPropertyChanged("billDetailList");
            }
        }

        private BillDetailModel billDetailModel = new BillDetailModel();

        public BillDetailViewModel()
        {
            List<Bill_details> billDetail = billDetailModel.getList();
            BillDetailList = new ObservableCollection<Bill_details>(billDetail);
        }
    }
}
