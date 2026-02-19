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
        public ObservableCollection<Produit> Produits { get; set; }

        int idVendeurCo = Session.currentVendeurConnected.Id;

        public ProduitsStocksViewModel()
        {
            _db = new GroupyContext();
            Produits = new ObservableCollection<Produit>(getProduits());
        }

        public List<Produit> getProduits()
        {
            var produits =  _db.Produits
                .Include(s => s.Stock)
                .Where(v => v.IdVendeur ==  idVendeurCo)
                .ToList();

            return produits.ToList();
        }
    }
}
