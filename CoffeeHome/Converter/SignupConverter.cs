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
    class SignupConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(item=>item!=null))
            {
                string name = values[0].ToString();
                string username = values[1].ToString();
                string password = values[2].ToString();
                string email = values[4].ToString();
                int sdt = int.Parse( (values[5].ToString() == "") ? "0" : values[5].ToString());

                return new Staff { name=name, username = username, password = password,email = email,sdt = sdt };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
