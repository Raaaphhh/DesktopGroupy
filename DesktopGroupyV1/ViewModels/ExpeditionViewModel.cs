using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
using System.Collections.ObjectModel;

namespace DesktopGroupyV1.ViewModels
{
    internal class ExpeditionViewModel
    {
        private readonly GroupyContext _context;
        public ObservableCollection<Expedition> Expeditions { get; set; }

        public ExpeditionViewModel()
        {
            _context = new GroupyContext();
            Expeditions = new ObservableCollection<Expedition>(GetExpeditions());
        }

        public List<Expedition> GetExpeditions()
        {
            try
            {
                var statut = new[] { "en_transit", "en_preparation"};
                int vendeurConnected = Session.currentVendeurConnected.Id;

                var expeditions = _context.Expeditions
                    .Include(e => e.Prevente)
                    .ThenInclude(p => p.NoteInterne)
                    .Where(e => e.Prevente.NoteInterne != null && e.Prevente.NoteInterne.IdVendeur == vendeurConnected && statut.Contains(e.Statut))  
                    .ToList();

//                var note = _context.NotesInternes
 //                   .Where(n => n.IdVendeur == vendeurConnected)
   //                 .ToList();

                Expeditions = new ObservableCollection<Expedition>(expeditions);
                return expeditions;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur GetExpeditions: {ex.Message}");
                return new List<Expedition>();
            }
        }
    }
}
