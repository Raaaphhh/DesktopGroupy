using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Org.BouncyCastle.Asn1.Mozilla;

namespace DesktopGroupyV1.ViewModels
{
    internal class ProduitsStocksViewModel
    {
        private readonly GroupyContext _db;
        public ObservableCollection<Stock> Stocks { get; set; }
        public ObservableCollection<Produit> ProduitsVendeur { get; set; }

        int idVendeurCo = Session.currentVendeurConnected.Id;

        public ProduitsStocksViewModel()
        {
            _db = new GroupyContext();
            Stocks = new ObservableCollection<Stock>(GetStocksWithProducts());
            ProduitsVendeur = new ObservableCollection<Produit>(GetProduitVendeurCo()); 
        }

        public List<Stock> GetStocksWithProducts()
        {
            var stocks = _db.Stocks
                .Include(s => s.Produit) 
                .Where(s => s.Produit.IdVendeur == idVendeurCo) 
                .ToList();

            return stocks;
        }

        public List<Produit> GetProduitVendeurCo()
        {
            var produitsVendeurCo = _db.Produits
                .Where(v => v.IdVendeur == idVendeurCo)
                .ToList();

            return produitsVendeurCo; 
        }

        // A Dev
        public bool DefSeuilAlert(int seuil, int IdProduit)
        {
            var seuilProduitAUpdate = _db.Stocks
             .Include(s => s.Produit)
             .FirstOrDefault(p => p.IdProduit == IdProduit);

            seuilProduitAUpdate.SeuilAlerte = seuil;
            _db.SaveChanges();

            return true; 
        }

        // A Dev
        public string AlertRupture()
        {
            var stocksVerification = _db.Stocks
                .Include (s => s.Produit)
                .Where(s => s.Produit.IdVendeur == idVendeurCo)
                .Where(s => s.StockPhysique <= s.SeuilAlerte)
                .ToList();

            return "a"; 
        }
    }
}
