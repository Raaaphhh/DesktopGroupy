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
        public ObservableCollection<Expedition> Expeditions { get; set; }

        public HistoriqueViewModel()
        {
            _context = new GroupyContext();
            Expeditions = new ObservableCollection<Expedition>(GetHistorique());
        }

        public List<Expedition> GetHistorique()
        {
            try
            {
                var statut = new[] { "livre", "expedie" };
                int vendeurConnected = Session.currentVendeurConnected.Id;
                Expeditions = new ObservableCollection<Expedition>(_context.Expeditions
                    .Include(e => e.Prevente)
                    .ThenInclude(pr => pr.Produit)
                    .Where(e => e.Prevente.Produit.IdVendeur == vendeurConnected && statut.Contains(e.Statut))
                    .ToList());

                return Expeditions.ToList();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Expedition>();
            }
        }
    }
}
