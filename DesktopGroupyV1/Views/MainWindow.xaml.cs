using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
using DesktopGroupyV1.ViewModels;
using DesktopGroupyV1.Views;
using DesktopGroupyV1.Views.ControlUser;
using Microsoft.VisualBasic;

namespace DesktopGroupyV1.Views
{
    public partial class MainWindow : Window
    {
        BaseViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new BaseViewModel();
            DataContext = vm;
        }

        private void ConnexionApp(object sender, RoutedEventArgs e)
        {
            string email = EmailText.Text;
            string pwd = PasswordBox.Password;  
            bool connexion = vm.IsConnected(email, pwd); 
            if (!connexion)
            {
                MessageBox.Show("Échec de la connexion. Veuillez vérifier vos informations d'identification.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Dashboard dashboard = new Dashboard();
                this.Close();
                dashboard.Show();
            }
        }
    }
}