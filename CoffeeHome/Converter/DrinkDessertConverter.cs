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
    class DrinkDessertConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(item => item != null))
            {
                string _name = values[0].ToString();
                string _description = values[1].ToString();
                int _price;
                if(!int.TryParse((values[2].ToString() == "") ? "0" : values[2].ToString(), out _price))
                {
                    _price = 0;
                }
                int _idtype;
                if (!int.TryParse((values[3].ToString() == "") ? "0" : values[3].ToString(), out _idtype))
                {
                    _idtype = 0;
                }
                string _image = values[4].ToString();

                return new DrinkAndDessert { name = _name, description = _description, price = _price, id_type=_idtype, image = _image};
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
