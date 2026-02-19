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
    /// <summary>
    /// Logique d'interaction pour Historique.xaml
    /// </summary>
    public partial class Historique : UserControl
    {
        HistoriqueViewModel _hvm;
        public Historique()
        {
            InitializeComponent();
            _hvm = new HistoriqueViewModel();
            DataContext = _hvm;
            _hvm.GetHistorique();
        }
    }
}
