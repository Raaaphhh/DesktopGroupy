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
            // faire une variable global constante pour stocker le user connecté.
        }

        private void Test_Connexion(object sender, RoutedEventArgs e)
        {
            bool reussi = vm.testConnexion();
            if (reussi)
            {
                MessageBox.Show("Test de connexion terminé.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Échec du test de connexion.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}