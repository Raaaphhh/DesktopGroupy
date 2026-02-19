using DesktopGroupyV1.Models;
using DesktopGroupyV1.ViewModels;
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

namespace DesktopGroupyV1.Views.ControlUser
{
    public partial class Stock : UserControl
    {
        ProduitsStocksViewModel vm;
        public Stock()
        {
            InitializeComponent();
            vm = new ProduitsStocksViewModel(); 
            DataContext = vm;
        }

        // a DEV
        public bool ModifierSeuil(object sender, RoutedEventArgs e)
        {
            var produitSelectionne = ListeProduits.SelectedItem as Produit;
            var produitConvert = ConvertToProduit(produitSelectionne);

            try
            {
                var seuilSelectionne = int.Parse(SeuilInput.Text);
            }
            catch (Exception ex)
            {
                return false;
            }

            vm.DefSeuilAlert(seuilSelectionne, produitConvert);
            return true;
        }

        private Produit ConvertToProduit(Produit produitSelec)
        {
            return new Produit
            {
                IdProduit = produitSelec.IdProduit,
            }; 
        }
    }
}
