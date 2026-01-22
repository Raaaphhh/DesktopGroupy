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

namespace DesktopGroupyV1
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

        private void Open_Page_Produit(object sender, RoutedEventArgs e)
        {
            ViewProduits produitPage = new ViewProduits();

            produitPage.Show();
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