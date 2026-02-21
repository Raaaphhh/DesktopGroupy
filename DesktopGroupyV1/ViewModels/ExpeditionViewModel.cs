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
                int vendeurConnected = Session.currentVendeurConnected.Id;
                Expeditions = new ObservableCollection<Expedition>(_context.Expeditions
                    .Include(e => e.NoteInterne)
                    .ThenInclude(n => n.Vendeur)
                    .Where(e => e.NoteInterne.IdVendeur == vendeurConnected)
                    .ToList());

                return Expeditions.ToList();
            }
            catch (Exception ex)
            {
                return new List<Expedition>();
            }
        }
    }
}
