using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DesktopGroupyV1.Models;
using DesktopGroupyV1.Data;

namespace DesktopGroupyV1.ViewModels
{
    internal class SignalementProduitViewModel
    {
        private readonly GroupyContext _context;
        private int IdVendeurCo => Session.currentVendeurConnected.Id;

        public ObservableCollection<Signalements> Signalements { get; set; }

        public SignalementProduitViewModel()
        {
            _context = new GroupyContext();
            Signalements = new ObservableCollection<Signalements>();
            ChargerSignalements();
        }

        public void ChargerSignalements(string? filtreStatut = null)
        {
            var query = _context.Signalements
                .Include(s => s.Client)
                .Include(s => s.Produit)
                .Where(s => s.Produit.IdVendeur == IdVendeurCo);

            if (!string.IsNullOrEmpty(filtreStatut) && filtreStatut != "Tous")
                query = query.Where(s => s.Statut == filtreStatut);

            var liste = query.OrderByDescending(s => s.DateSignalement).ToList();

            Signalements.Clear();
            foreach (var s in liste)
                Signalements.Add(s);
        }

        public bool ChangerStatut(Signalements signalement, string nouveauStatut)
        {
            try
            {
                var s = _context.Signalements.Find(signalement.Id);
                if (s == null) return false;

                s.Statut = nouveauStatut;
                _context.SaveChanges();
                signalement.Statut = nouveauStatut;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
