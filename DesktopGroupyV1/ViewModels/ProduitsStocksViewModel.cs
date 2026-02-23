using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
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
        public ObservableCollection<string> Alertes {  get; set; }

        int idVendeurCo = Session.currentVendeurConnected.Id;

        public ProduitsStocksViewModel()
        {
            _db = new GroupyContext();
            Stocks = new ObservableCollection<Stock>(GetStocksWithProducts());
            ProduitsVendeur = new ObservableCollection<Produit>(GetProduitVendeurCo());
            Alertes = new ObservableCollection<string>(AlertRuptureStock());
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

        public bool DefSeuilAlert(int seuil, Produit produitSelect)
        {
            int IdProduitSelec = produitSelect.IdProduit; 

            var seuilProduitAUpdate = _db.Stocks
             .Include(s => s.Produit)
             .FirstOrDefault(p => p.IdProduit == IdProduitSelec);

            seuilProduitAUpdate.SeuilAlerte = seuil;
            try
            {
                _db.SaveChanges();
            }catch (Exception ex)
            {
                var errorSaveChanges = ex.ToString();
            }

            return true; 
        }

        public ObservableCollection<string> AlertRuptureStock()
        {
            ObservableCollection<string> listAlertes = new ObservableCollection<string>(); 
            var stocksVerification = _db.Stocks
                .Include (s => s.Produit)
                .Where(s => s.Produit.IdVendeur == idVendeurCo)
                .Where(s => s.StockPhysique <= s.SeuilAlerte)
                .ToList();

            foreach (var stock in stocksVerification)
            {
                if (stock.StockDisponible <= stock.SeuilAlerte)
                {
                    string message = stock.Produit.Nom + " est en alerte seuil, avec un stock disponible (stock physique - stock reservé) de : " + stock.StockDisponible;
                    listAlertes.Add(message);
                    Console.WriteLine(message);
                }
            }

            return listAlertes; 
        }
    }
}
