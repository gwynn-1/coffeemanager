using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeeHome.Validation
{
    public class RequiredValidation : ValidationRule
    {
        public string PropertyName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Length == 0)
            {
                return new ValidationResult(false, this.PropertyName + " bắt buộc phải nhập");
            }
            return new ValidationResult(true, null);
        }
    }
}
