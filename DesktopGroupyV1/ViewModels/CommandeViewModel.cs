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
using System.ComponentModel.DataAnnotations.Schema;


namespace DesktopGroupyV1.ViewModels
{
    internal class CommandeViewModel
    {
        public readonly GroupyContext _context; 
        public ObservableCollection<Prevente> Preventes { get; set; }

        public CommandeViewModel()
        {
            _context = new GroupyContext();
            Preventes = new ObservableCollection<Prevente>(GetPrevente());
        }

        public List<Prevente> GetPrevente()
        {
            try
            {
                int vendeurConnected = Session.currentVendeurConnected.Id; 
                Preventes = new ObservableCollection<Prevente>(_context.Preventes
                                                        .Where(p => p.Produit.IdVendeur == vendeurConnected)
                                                        .Include(p => p.Produit)
                                                        .ToList());
                return Preventes.ToList();
            }
            catch (Exception ex) {
                Console.WriteLine($"Error fetching Prevente data: {ex.Message}");
                return new List<Prevente>();

            }

            
        }
    }
}
 