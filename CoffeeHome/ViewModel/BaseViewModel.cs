using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeeHome.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool isError;
        public bool IsError
        {
            get => isError;
            set
            {
                isError = value;
                OnPropertyChanged("isError");
            }
        }

        protected string errorContent;
        public string ErrorContent
        {
            get => errorContent;
            set
            {
                errorContent = value;
                OnPropertyChanged("errorContent");
            }
        }

        protected bool isSuccess;
        public bool IsSuccess
        {
            get => isSuccess;
            set
            {
                isSuccess = value;
                OnPropertyChanged("isSuccess");
            }
        }

        protected string successContent;
        public string SuccessContent
        {
            get => successContent;
            set
            {
                successContent = value;
                OnPropertyChanged("successContent");
            }
        }

        protected void BindingMessage(bool status, string message)
        {
            if (status)
            {
                IsSuccess = true;
                IsError = false;
                SuccessContent = message;
            }
            else
            {

                IsSuccess = false;
                IsError = true;
                ErrorContent = message;
            }
        }
    }
    class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
