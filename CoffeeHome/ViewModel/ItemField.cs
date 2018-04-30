using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoffeeHome.ViewModel
{
    public class ItemField: INotifyPropertyChanged
    {
        private string _name;
        private object _content;
        private object _datatableTemplate;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement;
        private Thickness _marginRequirement = new Thickness(16);
        private string _menuImage;

        public ItemField(string name, object content,object datatable,string menuImage)
        {
            _name = name;
            _content = content;
            _datatableTemplate = datatable;
            _menuImage = menuImage;
        }

        public string Name
        {
            get { return _name; }
            set { this.MutateVerbose(ref _name, value, RaisePropertyChanged()); }
        }

        public object Content
        {
            get { return _content; }
            set { this.MutateVerbose(ref _content, value, RaisePropertyChanged()); }
        }

        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get { return _horizontalScrollBarVisibilityRequirement; }
            set { this.MutateVerbose(ref _horizontalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get { return _verticalScrollBarVisibilityRequirement; }
            set { this.MutateVerbose(ref _verticalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        }

        public Thickness MarginRequirement
        {
            get { return _marginRequirement; }
            set { this.MutateVerbose(ref _marginRequirement, value, RaisePropertyChanged()); }
        }

        public string MenuImage { get { return _menuImage; } set { this.MutateVerbose(ref _menuImage, value, RaisePropertyChanged()); } }

        public object DatatableTemplate { get { return _datatableTemplate; } set { this.MutateVerbose(ref _datatableTemplate, value, RaisePropertyChanged()); } }

        private void MutateVerbose<ItemField>(ref ItemField control, ItemField value, Action<PropertyChangedEventArgs> action,[CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<ItemField>.Default.Equals(control, value)) return;
            control = value;
            action?.Invoke(new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
