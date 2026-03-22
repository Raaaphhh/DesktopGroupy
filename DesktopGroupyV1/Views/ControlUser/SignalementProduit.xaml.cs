using DesktopGroupyV1.Models;
using DesktopGroupyV1.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DesktopGroupyV1.Views.ControlUser
{
    public partial class SignalementProduit : UserControl
    {
        private SignalementProduitViewModel _vm;

        private static readonly List<string> _statuts = new() { "Tous", "en_attente", "résolu", "rejeté" };

        public SignalementProduit()
        {
            _vm = new SignalementProduitViewModel();
            InitializeComponent();
            DataContext = _vm;

            FiltreStatut.ItemsSource = _statuts;
            FiltreStatut.SelectedIndex = 0;
            MettreAJourCompteur();
        }

        private void FiltreStatut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filtre = FiltreStatut.SelectedItem as string;
            _vm.ChargerSignalements(filtre);
            MettreAJourCompteur();
        }

        private void GridSignalements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var signalement = GridSignalements.SelectedItem as Signalement;
            bool hasSelection = signalement != null;

            BtnResoudre.IsEnabled = hasSelection && signalement!.Statut != "résolu";
            BtnRejeter.IsEnabled = hasSelection && signalement!.Statut != "rejeté";
            ActionLabel.Text = hasSelection
                ? $"Signalement de {signalement!.Client?.NomComplet} — {signalement.Produit?.Nom}"
                : "Sélectionner un signalement pour agir";
        }

        private void BtnResoudre_Click(object sender, RoutedEventArgs e)
        {
            var signalement = GridSignalements.SelectedItem as Signalement;
            if (signalement == null) return;

            bool ok = _vm.ChangerStatut(signalement, "résolu");
            if (ok)
            {
                var filtre = FiltreStatut.SelectedItem as string;
                _vm.ChargerSignalements(filtre);
                MettreAJourCompteur();
                ResetActionBar();
            }
            else
            {
                MessageBox.Show("Impossible de mettre à jour le statut.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRejeter_Click(object sender, RoutedEventArgs e)
        {
            var signalement = GridSignalements.SelectedItem as Signalement;
            if (signalement == null) return;

            bool ok = _vm.ChangerStatut(signalement, "rejeté");
            if (ok)
            {
                var filtre = FiltreStatut.SelectedItem as string;
                _vm.ChargerSignalements(filtre);
                MettreAJourCompteur();
                ResetActionBar();
            }
            else
            {
                MessageBox.Show("Impossible de mettre à jour le statut.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MettreAJourCompteur()
        {
            NbSignalementsLabel.Text = $"{_vm.Signalements.Count} signalement(s)";
        }

        private void ResetActionBar()
        {
            BtnResoudre.IsEnabled = false;
            BtnRejeter.IsEnabled = false;
            ActionLabel.Text = "Sélectionner un signalement pour agir";
        }
    }
}
