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
    class BillOnlineDetailConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.All(item => item != null))
            {
                int _idbill;
                if (!int.TryParse((values[0].ToString() == "") ? "0" : values[0].ToString(), out _idbill))
                {
                    _idbill = 0;
                }
                int _iddrink;
                if (!int.TryParse((values[1].ToString() == "") ? "0" : values[1].ToString(), out _iddrink))
                {
                    _iddrink = 0;
                }
                int _quantity;
                if (!int.TryParse((values[2].ToString() == "") ? "0" : values[2].ToString(), out _quantity))
                {
                    _quantity = 0;
                }
                int _price;
                if (!int.TryParse((values[3].ToString() == "") ? "0" : values[3].ToString(), out _price))
                {
                    _price = 0;
                }

                return new Bill_Online_Detail { id_bill = _idbill, id_drink = _iddrink, quantity = _quantity, price = _price };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
