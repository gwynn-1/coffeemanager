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
    class BillOnlineConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(item => item != null))
            {
                string _name = values[2].ToString();
                string _address = values[1].ToString();
                int _total;
                if (!int.TryParse((values[0].ToString() == "") ? "0" : values[0].ToString(), out _total))
                {
                    _total = 0;
                }
                int _sdt;
                if (!int.TryParse((values[3].ToString() == "") ? "0" : values[3].ToString(), out _sdt))
                {
                    _sdt = 0;
                }

                return new Bill_Online { name_customer = _name,address = _address,total_price=_total,sdt=_sdt };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
