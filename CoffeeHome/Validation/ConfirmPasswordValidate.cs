using CoffeeHome.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoffeeHome.Validation
{
    class ConfirmPasswordValidate : ValidationRule
    {
        public Repassword Repassword { get; set; }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (Repassword != null)
            {
                if (value.ToString()==null || value.ToString().Length == 0)
                {
                    result = new ValidationResult(false, "Xin hãy nhập lại Password");
                }
                if (value.ToString() != Repassword.Password)
                {
                    result = new ValidationResult(false, "Password nhập lại không khớp");
                }
            }
            return result;
        }
    }

    class Repassword : DependencyObject
    {
        public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string),
        typeof(Repassword), new FrameworkPropertyMetadata(string.Empty));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set {
                SetValue(PasswordProperty, value);
            }
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            sender.SetValue(PasswordProperty, e.NewValue);
        }
    }

    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy), new UIPropertyMetadata(string.Empty));
    }
}
