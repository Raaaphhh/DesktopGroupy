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
using DesktopGroupyV1.Data;
using DesktopGroupyV1.ViewModels;
using DesktopGroupyV1.Views;

namespace DesktopGroupyV1.Views.ControlUser
{
    /// <summary>
    /// Logique d'interaction pour Expeditions.xaml
    /// </summary>
    public partial class Expeditions : UserControl
    {
        ExpeditionViewModel _evm;
        public Expeditions()
        {
            _evm = new ExpeditionViewModel();
            InitializeComponent();
            DataContext = _evm;
            _evm.GetExpeditions();
        }
    }
}
