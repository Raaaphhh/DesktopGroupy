using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using DesktopGroupyV1.Models;
using DesktopGroupyV1.Views.ControlUser;
using DesktopGroupyV1.Data;
using System.Security.RightsManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;



namespace DesktopGroupyV1.ViewModels
{
    internal class HistoriqueViewModel
    {
        public readonly GroupyContext _context;
        public ObservableCollection<Expeditions> Expedition { get; set; }
    }

    public HistoriqueViewModel()
        {
            _context = new GroupyContext();
            Expedition = new ObservableCollection<Expeditions>(GetExpedition());
        }


    }
}
