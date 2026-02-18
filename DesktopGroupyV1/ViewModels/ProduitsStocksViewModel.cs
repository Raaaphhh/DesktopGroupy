using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Org.BouncyCastle.Asn1.Mozilla;

namespace DesktopGroupyV1.ViewModels
{
    internal class ProduitsStocksViewModel
    {
        private readonly GroupyContext _db;
        public ProduitsStocksViewModel()
        {
            _db = new GroupyContext();
        }

        public ObservableCollection<Stock> Stocks { get; set; }
        public ObservableCollection<Produit> Produits { get; set; }

        // Suite a dev envoyer la liste des produits en stock et la liste des produits pour les afficher dans la vue
    }
}
