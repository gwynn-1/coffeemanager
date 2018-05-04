using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    class BillViewModel:BaseViewModel
    {
        private ObservableCollection<Bill> billList;

        public ObservableCollection<Bill> BillList
        {
            get => billList;
            set
            {
                billList = value;
                OnPropertyChanged("billList");
            }
        }

        private BillModel tableModel = new BillModel();

        public BillViewModel()
        {
            List<Bill> bill = tableModel.getList();
            BillList = new ObservableCollection<Bill>(bill);
        }
    }
}
