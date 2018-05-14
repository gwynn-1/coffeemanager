using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoffeeHome.TemplateView.CRUTemplate
{
    /// <summary>
    /// Interaction logic for FoodDrinkCRUDialog.xaml
    /// </summary>
    public partial class FoodDrinkCRUDialog : UserControl
    {
        public FoodDrinkCRUDialog()
        {
            InitializeComponent();
        }

        private void button_file_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension

            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            // Display OpenFileDialog by calling ShowDialog method

            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document

                string filename = dlg.FileName;

                txbImage.Text = filename;
            }

        }
    }
}
