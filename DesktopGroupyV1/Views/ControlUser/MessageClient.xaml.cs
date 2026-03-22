using DesktopGroupyV1.Models;
using DesktopGroupyV1.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DesktopGroupyV1.Views.ControlUser
{
    public partial class MessageClient : UserControl
    {
        private MessageClientViewModel _vm;

        public MessageClient()
        {
            _vm = new MessageClientViewModel();
            InitializeComponent();
            DataContext = _vm;
        }

        private void ListeProduits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var produit = ListeProduits.SelectedItem as Produit;
            if (produit == null)
                return;

            _vm.ChargerClientsCibles(produit);
            NbClientsLabel.Text = $"{_vm.ClientsCibles.Count} client(s) concerné(s)";
        }

        private void EnvoyerMessage(object sender, RoutedEventArgs e)
        {
            var produit = ListeProduits.SelectedItem as Produit;
            if (produit == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ObjetMessage.Text))
            {
                MessageBox.Show("Veuillez renseigner l'objet du message.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(CorpsMessage.Text))
            {
                MessageBox.Show("Veuillez renseigner le corps du message.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_vm.ClientsCibles.Count == 0)
            {
                MessageBox.Show("Aucun client n'a commandé ce produit.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            bool succes = _vm.EnvoyerMessage(ObjetMessage.Text, CorpsMessage.Text);

            if (succes)
            {
                MessageBox.Show(
                    $"Message envoyé à {_vm.ClientsCibles.Count} client(s) pour le produit \"{produit.Nom}\".",
                    "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                ObjetMessage.Text = string.Empty;
                CorpsMessage.Text = string.Empty;
                ListeProduits.SelectedItem = null;
                NbClientsLabel.Text = "0 client(s) concerné(s)";
            }
            else
            {
                MessageBox.Show(
                    "Une erreur est survenue lors de l'envoi du message.",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
