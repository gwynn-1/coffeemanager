using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHome.ViewModel
{
    class TableViewModel:BaseViewModel
    {
        private ObservableCollection<Table> tableList;

        public ObservableCollection<Table> TableList
        {
            get => tableList;
            set
            {
                tableList = value;
                OnPropertyChanged("tableList");
            }
        }

        private TableModel tableModel = new TableModel();

        public TableViewModel()
        {
            List<Table> table = tableModel.getList();
            TableList = new ObservableCollection<Table>(table);
        }
    }
}
