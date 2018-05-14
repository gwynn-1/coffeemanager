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
    class TableConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(item => item != null))
            {
                string _name = values[0].ToString();
                byte _status;
                if ((bool)values[1])
                    _status = 1;
                else
                    _status = 0;

                return new Table { name = _name, status=_status };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
