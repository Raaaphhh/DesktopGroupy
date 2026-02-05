using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DesktopGroupyV1.ViewModels
{
    internal class ProduitViewModel : INotifyPropertyChanged
    {
        private readonly GroupyContext _db;
        public ProduitViewModel()
        {
            _db = new GroupyContext();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _totalPreventes;
        public int TotalPreventes
        {
            get => _totalPreventes;
            set 
            {
                _totalPreventes = value;
                OnPropertyChanged(nameof(TotalPreventes));
            }
        }

        private int _totalPreventesExpediees;
        public int TotalPreventesExpediees
        {
            get => _totalPreventesExpediees;
            set
            {
                _totalPreventesExpediees = value;
                OnPropertyChanged(nameof(TotalPreventesExpediees));
            }
        }

        public void chargerInfos()
        {
            var preventes = _db.Preventes.ToList();
            TotalPreventes = preventes.Count;
            TotalPreventesExpediees = preventes.Count(p => p.Statut == "Expédié");
        }
    }
}
