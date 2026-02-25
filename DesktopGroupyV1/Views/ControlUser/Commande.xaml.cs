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
            _cvm = new CommandeViewModel();
            InitializeComponent();
            DataContext = _cvm;
            _cvm.GetPrevente();
        }
    }
}
