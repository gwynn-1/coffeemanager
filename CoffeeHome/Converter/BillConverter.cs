using CoffeeHome.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoffeeHome.Converter
{
    class BillConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(item => item != null))
            {
                int _idtable;
                if (!int.TryParse((values[2].ToString() == "") ? "0" : values[2].ToString(), out _idtable))
                {
                    _idtable = 0;
                }
                int _idcustomer;
                if (!int.TryParse((values[1].ToString() == "") ? "0" : values[1].ToString(), out _idcustomer))
                {
                    _idcustomer = 0;
                }
                int _total;
                if (!int.TryParse((values[0].ToString() == "") ? "0" : values[0].ToString(), out _total))
                {
                    _total = 0;
                }

                return new Bill { id_customer = _idcustomer,id_table=_idtable,total_price = _total };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
