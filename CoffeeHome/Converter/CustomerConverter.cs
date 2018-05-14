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
    class CustomerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(item => item != null))
            {
                string _name = values[0].ToString();
                string _email = values[1].ToString();
                int _sdt;
                if (!int.TryParse((values[2].ToString() == "") ? "0" : values[2].ToString(), out _sdt))
                {
                    _sdt = 0;
                }
                int _cmnd;
                if (!int.TryParse((values[3].ToString() == "") ? "0" : values[3].ToString(), out _cmnd))
                {
                    _cmnd = 0;
                }
                int _points;
                if (!int.TryParse((values[4].ToString() == "") ? "0" : values[4].ToString(), out _points))
                {
                    _points = 0;
                }

                return new Customer { name = _name, email = _email, cmnd = _cmnd, sdt = _sdt, points = _points };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
