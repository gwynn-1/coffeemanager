using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeeHome.Validation
{
    public class ImageValidation : ValidationRule
    {
        public string PropertyName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (value != null &&!Regex.IsMatch(value.ToString(), @"([a-zA-Z].*?)\.(jpg|JPG|jpeg|JPEG|bmp|BMP|gif|GIF)$"))
            {
                result = new ValidationResult(false, PropertyName + " không phải định dạng hình");
            }
            return result;
        }
    }
}
