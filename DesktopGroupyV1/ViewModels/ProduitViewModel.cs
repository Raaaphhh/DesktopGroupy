using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

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

        private int _totalPreventesConfirmes;
        public int TotalPreventesConfirmes
        {
            get => _totalPreventesConfirmes;
            set
            {
                _totalPreventesConfirmes = value;
                OnPropertyChanged(nameof(TotalPreventesConfirmes));
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

        private decimal _chiffreAffaire;
        public decimal ChiffreAffaire
        {
            get => _chiffreAffaire;
            set
            {
                _chiffreAffaire = value;
                OnPropertyChanged(nameof(ChiffreAffaire));
            }
        }

        private int _nbrSignal;
        public int SignalCount
        {
            get => _nbrSignal;
            set
            {
                _nbrSignal = value;
                OnPropertyChanged(nameof(SignalCount));
            }
        }

        private PlotModel _salesPlotModel;
        public PlotModel SalesPlotModel
        {
            get => _salesPlotModel;
            set
            {
                _salesPlotModel = value;
                OnPropertyChanged(nameof(SalesPlotModel));
            }
        }

        public void chargerInfos()
        {
            var preventes = _db.Preventes.ToList();
            TotalPreventes = preventes.Count;
            TotalPreventesConfirmes = preventes.Count(p => p.Statut == "confirmee");
            TotalPreventesExpediees = preventes.Count;
            ChiffreAffaire = _db.Preventes
                .Where(p => p.Statut == "confirmee" && p.Produit.IdVendeur == Session.currentVendeurConnected.Id)
                .Sum(p => p.Produit.PrixGroupe);
            SignalCount = _db.Signalements
                .Where(s => s.Statut == "en_attente" && s.Produit.IdVendeur == Session.currentVendeurConnected.Id)
                .Count();
            ChargerGraphique();
        }

        private void ChargerGraphique()
        {
            var model = new PlotModel { Title = "Evolution des ventes" };

            model.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                ItemsSource = new[] { "jan", "fev", "mar", "avri", "mai", "Juin", "Juil", "Aout", "sept", "oct", "nov", "dec" },
                Title = "Mois"
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                StringFormat = "C0",
                MajorGridlineStyle = LineStyle.Dot,
                Title = "Chiffre d'affaires"
            });

            var ventesParMois = _db.Preventes
                .Where(p => p.Statut == "confirmee" && p.Produit.IdVendeur == Session.currentVendeurConnected.Id)
                .Include(p => p.Produit)
                .ToList()
                .GroupBy(p => p.DatePrevente.Month)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Produit.PrixGroupe));

            var serie = new LineSeries
            {
                Title = "CA sur l'année",
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerStroke = OxyColors.RoyalBlue,
                StrokeThickness = 2,
            };

            for (int mois = 1; mois <= 12; mois++)
            {
                decimal valeur = ventesParMois.ContainsKey(mois) ? ventesParMois[mois] : 0;
                serie.Points.Add(new DataPoint(mois -1, (double)valeur));
            }

            model.Series.Add(serie);
            SalesPlotModel = model;
        }



    }
}
