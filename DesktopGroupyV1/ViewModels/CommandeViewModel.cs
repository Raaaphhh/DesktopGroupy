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

        public CommandeViewModel(string test = null)
        {
            _context = new GroupyContext();
            Preventes = new ObservableCollection<Prevente>(GetPrevente(test));
        }

        public List<Prevente> GetPrevente(string filtre = null)
        {
            try
            {
                int vendeurConnected = Session.currentVendeurConnected.Id;
                Preventes = new ObservableCollection<Prevente>(_context.Preventes
                                                         .Include(prv => prv.Produit)
                                                         .ThenInclude(prv => prv.Vendeur)
                                                         .Include(v => v.NoteInterne)
                                                         .ThenInclude(v => v.Expeditions)
                                                         .Where(prev => prev.Produit.IdVendeur == vendeurConnected)
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
 