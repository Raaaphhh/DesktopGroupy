using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopGroupyV1.Models;
using DesktopGroupyV1.Views;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using DesktopGroupyV1.Data;
using System.Collections.ObjectModel;

namespace DesktopGroupyV1.ViewModels
{
    internal class GestionViewModel
    {
        public readonly DbContext _context;
        public ObservableCollection<Prevente> Preventes { get; set; }

        public GestionViewModel()
        {
            _context = new GroupyContext();
            Preventes = new ObservableCollection<Prevente>(GetPrevente());
        }

        public List<Prevente> GetPrevente()
        {
            try
            {
                Preventes = new ObservableCollection<Prevente>(_context.Set<Prevente>().ToList());
                return Preventes.ToList();
            }
            catch (Exception ex) {
                Console.WriteLine($"Error fetching Prevente data: {ex.Message}");
                return new List<Prevente>();

            }
        }
    }
}
