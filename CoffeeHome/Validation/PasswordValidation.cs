using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeeHome.Validation
{
    class PasswordValidation : ValidationRule
    {
        public string PropertyName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            int num;
            if (int.TryParse(value.ToString(), out num))
                result = new ValidationResult(false, this.PropertyName + " phải chứa kí tự chữ");
            return result;
        }
    }
}
