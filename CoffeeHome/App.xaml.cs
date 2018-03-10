using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CoffeeHome
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            this.MainWindow.WindowState = WindowState.Minimized;
        }

        private void btnResize_Click(object sender, RoutedEventArgs e)
        {
            if (this.MainWindow.WindowState == WindowState.Normal)
                this.MainWindow.WindowState = WindowState.Maximized;
            else
                this.MainWindow.WindowState = WindowState.Normal;
        }

        private void Application_Activated(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => this.MainWindow.WindowStyle = WindowStyle.None));
        }

        private void Application_Deactivated(object sender, EventArgs e)
        {
            if(this.MainWindow.WindowState == WindowState.Normal || this.MainWindow.WindowState == WindowState.Maximized)
            {
                this.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }
    }
}
