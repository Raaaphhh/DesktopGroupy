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
                    .Where(e => e.IdNotes != null)
                    .Include(e => e.NoteInterne)
                    .Where(e => e.NoteInterne.IdVendeur == vendeurConnected && statut.Contains(e.Statut))
                    .AsNoTracking()
                    .ToList();

                Expeditions = new ObservableCollection<Expedition>(expeditions);
                return expeditions;
            }
            catch (Exception ex)
            {
                // Logger l'erreur pour diagnostiquer
                System.Diagnostics.Debug.WriteLine($"Erreur GetExpeditions: {ex.Message}");
                return new List<Expedition>();
            }
        }
    }
}
