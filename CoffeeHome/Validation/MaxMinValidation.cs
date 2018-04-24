using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeeHome.Validation
{
    class MaxMinValidation : ValidationRule
    {
        public string PropertyName { get; set; }

        public int Max { get; set; }

        public int Min { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (value.ToString().Length > Max || value.ToString().Length < Min)
            {
                result = new ValidationResult(false, PropertyName + " phải có độ dài từ " + Min + " và " + Max);
            }
            return result;
        }
    }
}
