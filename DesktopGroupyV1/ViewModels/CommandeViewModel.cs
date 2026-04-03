using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
using DesktopGroupyV1.Views;
using DesktopGroupyV1.Views.ControlUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace DesktopGroupyV1.ViewModels
{
    internal class CommandeViewModel
    {
        public readonly GroupyContext _context; 
        public ObservableCollection<Prevente> Preventes { get; set; }
       // public List<Expedition> StatutExp { get; set; }


        public CommandeViewModel(string? filtre = null)
        {
            _context = new GroupyContext();
            Preventes = new ObservableCollection<Prevente>(GetPrevente(filtre));
        }

        public List<Prevente> GetPrevente(string filtre)
        {
            if (filtre != "Tous" && filtre != null)
            {
                int vendeurConnected = Session.currentVendeurConnected.Id;
                Preventes = new ObservableCollection<Prevente>(_context.Preventes
                                                         .Include(prv => prv.Produit)
                                                         .ThenInclude(prv => prv.Vendeur)
                                                         .Include(v => v.NoteInterne)
                                                         .ThenInclude(v => v.Expeditions)                                                         
                                                         .Where(prev => prev.Produit.IdVendeur == vendeurConnected && prev.NoteInterne.Expeditions.Any(e => e.Statut == filtre))
                                                         .ToList());
                return Preventes.ToList();
            }
            //else if (filtre == "livre")
            //{
            //    int vendeurConnected = Session.currentVendeurConnected.Id;
            //    Preventes = new ObservableCollection<Prevente>(_context.Preventes
            //                                             .Include(prv => prv.Produit)
            //                                             .ThenInclude(prv => prv.Vendeur)
            //                                             .Include(v => v.NoteInterne)
            //                                             .ThenInclude(v => v.Expeditions)                                                         
            //                                             .Where(prev => prev.Produit.IdVendeur == vendeurConnected && prev.NoteInterne.Expeditions.Any(e => e.Statut == "livre"))
            //                                             .ToList());
            //    return Preventes.ToList();
            //}

            else if (filtre == null || filtre == "Tous")
            {
                try
                {

                    int vendeurConnected = Session.currentVendeurConnected.Id;
                    Preventes = new ObservableCollection<Prevente>(_context.Preventes
                                                             .Include(prv => prv.Produit)
                                                             .ThenInclude(prv => prv.Vendeur)
                                                             .Include(v => v.NoteInterne)
                                                             .ThenInclude(v => v.Expeditions)
                                                             .Where(prev => prev.Produit.IdVendeur == vendeurConnected)
                                                             .ToList());


                    return Preventes.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching Prevente data: {ex.Message}");
                    return new List<Prevente>();
                }
            }

            return new List<Prevente>();
        }
    }
}
