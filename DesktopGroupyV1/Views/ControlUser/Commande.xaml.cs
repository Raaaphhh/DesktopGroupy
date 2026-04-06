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
using Microsoft.EntityFrameworkCore;

namespace DesktopGroupyV1.Views.ControlUser
{
    public partial class Commande : UserControl
    {
        CommandeViewModel _cvm;
        public Commande()
        {
            InitializeComponent();
            _cvm = new CommandeViewModel(null);
            DataContext = _cvm;
        }

        private void AddFilter(object sender, RoutedEventArgs e)
        {
            if( FiltreExpedition.Text == "Tous"||FiltreNote.Text == "Tous")
            {
                FiltreExpedition.Text = null;
                FiltreNote.Text = null;
            }
              
            string? filtre = FiltreExpedition.Text;
            string? filtreNote = FiltreNote.Text;

            _cvm = new CommandeViewModel(filtre, filtreNote);
            DataContext = _cvm;
        }

        //private void AddFilterNote(object sender, RoutedEventArgs e)
        //{
        //    string? filtre = FiltreExpedition.Text;
        //    string? filtreNote = FiltreNote.Text;
        //    _cvm = new CommandeViewModel(filtre, filtreNote);
        //    DataContext = _cvm;
        //}

    }
}
