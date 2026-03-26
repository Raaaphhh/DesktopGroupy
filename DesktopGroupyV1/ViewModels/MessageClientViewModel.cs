using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using DesktopGroupyV1.Models;
using DesktopGroupyV1.Data;

namespace DesktopGroupyV1.ViewModels
{
    internal class MessageClientViewModel
    {
        private readonly GroupyContext _context;

        public ObservableCollection<Produit> ProduitsVendeur { get; set; }
        public ObservableCollection<Client> ClientsCibles { get; set; }

        private int IdVendeurCo => Session.currentVendeurConnected.Id;

        public MessageClientViewModel()
        {
            _context = new GroupyContext();
            ProduitsVendeur = new ObservableCollection<Produit>(GetProduitsVendeur());
            ClientsCibles = new ObservableCollection<Client>();
        }

        public List<Produit> GetProduitsVendeur()
        {
            return _context.Produits
                .Where(p => p.IdVendeur == IdVendeurCo)
                .ToList();
        }

        public List<Client> GetClientsByProduit(int idProduit)
        {
            return _context.Preventes
                .Include(p => p.Client)
                .Where(p => p.IdProduit == idProduit)
                .Select(p => p.Client)
                .Distinct()
                .ToList();
        }

        public void ChargerClientsCibles(Produit produit)
        {
            ClientsCibles.Clear();
            var clients = GetClientsByProduit(produit.IdProduit);
            foreach (var client in clients)
                ClientsCibles.Add(client);
        }

        public bool EnvoyerMessage(string objet, string corps)
        {
            if (ClientsCibles.Count == 0)
                return false;

            try
            {
                using var smtp = new SmtpClient
                {
                    // A changer et voir comment on peut mettre ça en place
                    Host = "smtp.example.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("votre@email.com", "motdepasse")
                };

                foreach (var client in ClientsCibles)
                {
                    var mail = new MailMessage
                    {
                        From = new MailAddress("votre@email.com", "Groupy"),
                        Subject = objet,
                        Body = corps,
                        IsBodyHtml = false
                    };
                    mail.To.Add(new MailAddress(client.Email, client.NomComplet));
                    smtp.Send(mail);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur envoi email : {ex.Message}");
                return false;
            }
        }
    }
}
