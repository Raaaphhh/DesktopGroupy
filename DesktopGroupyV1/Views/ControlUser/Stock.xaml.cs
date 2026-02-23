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

        public void ModifierSeuil(object sender, RoutedEventArgs e)
        {
            var produitSelectionne = ListeProduits.SelectedItem as Produit;
            var produitConvert = ConvertToProduit(produitSelectionne);

            if (produitConvert == null)
            {
                MessageBox.Show("Veuillez saisir un produit ou seuil valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string seuilSelectionne = SeuilInput.Text;
            try
            {
                var seuilSelectionneConvert = int.Parse(seuilSelectionne);
                if (seuilSelectionneConvert.GetType() == typeof(int) && vm.DefSeuilAlert(seuilSelectionneConvert, produitConvert) == true)
                {
                    SeuilInput.Text = string.Empty;
                    ListeProduits.SelectedItem = string.Empty;
                    vm = new ProduitsStocksViewModel();
                    DataContext = vm;
                    MessageBox.Show("Votre seuil : " + seuilSelectionneConvert + "a été modifier pour le produit : " + produitConvert.Nom+".", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    SeuilInput.Text = string.Empty;
                    ListeProduits.SelectedItem = string.Empty;
                    MessageBox.Show("Une erreur est survenue dans l'enregistrement des données ou dans la saisie a eu lieu.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                SeuilInput.Text = string.Empty;
                ListeProduits.SelectedItem = string.Empty;
                MessageBox.Show("Erreur : le seuil n'es pas dans un format valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Produit ConvertToProduit(Produit produitSelec)
        {
            if(produitSelec == null)
            {
                return produitSelec;
            }
            else
            {
                return new Produit
                {
                    IdProduit = produitSelec.IdProduit,
                    Nom = produitSelec.Nom
                };
            }
        }

        public void RechargerDatatable(object sender, RoutedEventArgs e)
        {
            vm = new ProduitsStocksViewModel();
            DataContext = vm; 
        }
    }
}
