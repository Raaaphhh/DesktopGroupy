using DesktopGroupyV1.Models;
using DesktopGroupyV1.Views.ControlUser;
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
using System.Windows.Shapes;

namespace DesktopGroupyV1.Views
{
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
            OpenUserControlFirst();
        }
        private void OpenUserControlFirst()
        {
            ContentArea.Visibility = Visibility.Visible;
            ContentArea.Content = new Dashboard();
        }
        public void OpenStockView(object sender, RoutedEventArgs e)
        {
            ContentArea.Visibility = Visibility.Visible;
            ContentArea.Content = new ControlUser.Stock();
        }

        public void OpenDashboard(object sender, RoutedEventArgs e)
        {
            ContentArea.Visibility = Visibility.Visible;
            ContentArea.Content = new Dashboard();
        }

        public void OpenGestionCE(object sender, RoutedEventArgs e)
        {
            ContentArea.Visibility = Visibility.Visible;
            ContentArea.Content = new GestionCE();
        }
    }
}
