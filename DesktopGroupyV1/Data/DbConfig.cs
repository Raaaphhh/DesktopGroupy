using DesktopGroupyV1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using DotNetEnv; 

namespace DesktopGroupyV1.Data
{
    public class GroupyContext : DbContext
    {
        public DbSet<Vendeur> Vendeurs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Prevente> Preventes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<MouvementStock> MouvementsStock { get; set; }
        public DbSet<Expedition> Expeditions { get; set; }
        public DbSet<NoteInterne> NotesInternes { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Signalement> Signalements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DotNetEnv.Env.Load("Env/.env");
                string server = DotNetEnv.Env.GetString("DB_SERVER");
                string database = DotNetEnv.Env.GetString("DB_DATABASE");
                string user = DotNetEnv.Env.GetString("DB_USER");
                string password = DotNetEnv.Env.GetString("DB_PASSWORD");

                string connectionString = $"Server={server};Database={database};User={user};Password={password};";

                optionsBuilder
                  .UseMySql(
                      connectionString,
                      new MySqlServerVersion(new Version(8, 0, 39))
                  )
                  .EnableSensitiveDataLogging() // affiche les valeurs des paramètres
                  .EnableDetailedErrors()       // erreurs EF plus explicites
                  .LogTo(
                      Console.WriteLine,
                      LogLevel.Information
                  );

                // Activer les logs SQL pour le debug (optionnel)
                //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            }
        }

        // ================================================
        // CONFIGURATION DES RELATIONS ET CONTRAINTES
        // ================================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================================================
            // PRODUIT
            // ================================================

            modelBuilder.Entity<Produit>()
                .HasOne(p => p.Vendeur)
                .WithMany()
                .HasForeignKey(p => p.IdVendeur)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produit>()
                .HasOne(p => p.Categorie)
                .WithMany()
                .HasForeignKey(p => p.IdCategorie)
                .OnDelete(DeleteBehavior.Restrict);

            // ================================================
            // PREVENTE
            // ================================================

            modelBuilder.Entity<Prevente>()
                .HasOne(p => p.Produit)
                .WithMany()
                .HasForeignKey(p => p.IdProduit)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prevente>()
                .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.IdClient)
                .OnDelete(DeleteBehavior.Restrict);

            // Contrainte unique : un client ne peut pas commander 2 fois le même produit
            modelBuilder.Entity<Prevente>()
                .HasIndex(p => new { p.IdProduit, p.IdClient })
                .IsUnique();

            // ================================================
            // STOCK
            // ================================================

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Produit)
                .WithMany()
                .HasForeignKey(s => s.IdProduit)
                .OnDelete(DeleteBehavior.Cascade);

            // Un produit = un stock unique
            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.IdProduit)
                .IsUnique();

            // ================================================
            // MOUVEMENT STOCK
            // ================================================

            modelBuilder.Entity<MouvementStock>()
                .HasOne(m => m.Produit)
                .WithMany()
                .HasForeignKey(m => m.IdProduit)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MouvementStock>()
                .HasOne(m => m.Vendeur)
                .WithMany()
                .HasForeignKey(m => m.IdVendeur)
                .OnDelete(DeleteBehavior.Restrict);

            // ================================================
            // EXPEDITION
            // ================================================

            modelBuilder.Entity<Expedition>()
                .HasOne(e => e.Prevente)
                .WithMany()
                .HasForeignKey(e => e.IdPrevente)
                .OnDelete(DeleteBehavior.Restrict);

            // Une prévente = une expédition unique
            modelBuilder.Entity<Expedition>()
                .HasIndex(e => e.IdPrevente)
                .IsUnique();

            // Numéro de tracking unique
            modelBuilder.Entity<Expedition>()
                .HasIndex(e => e.NumeroTracking)
                .IsUnique();

            // ================================================
            // NOTE INTERNE
            // ================================================

            modelBuilder.Entity<NoteInterne>()
                .HasOne(n => n.Prevente)
                .WithMany()
                .HasForeignKey(n => n.IdPrevente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NoteInterne>()
                .HasOne(n => n.Vendeur)
                .WithMany()
                .HasForeignKey(n => n.IdVendeur)
                .OnDelete(DeleteBehavior.Restrict);

            // ================================================
            // FACTURE
            // ================================================

            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Prevente)
                .WithMany()
                .HasForeignKey(f => f.IdPrevente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Vendeur)
                .WithMany()
                .HasForeignKey(f => f.IdVendeur)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Client)
                .WithMany()
                .HasForeignKey(f => f.IdClient)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Produit)
                .WithMany()
                .HasForeignKey(f => f.IdProduit)
                .OnDelete(DeleteBehavior.Restrict);

            // Une prévente = une facture unique
            modelBuilder.Entity<Facture>()
                .HasIndex(f => f.IdPrevente)
                .IsUnique();

            // Numéro de facture unique
            modelBuilder.Entity<Facture>()
                .HasIndex(f => f.NumeroFacture)
                .IsUnique();

            // ================================================
            // SIGNALEMENT
            // ================================================

            modelBuilder.Entity<Signalement>()
                .HasOne(s => s.Produit)
                .WithMany()
                .HasForeignKey(s => s.IdProduit)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Signalement>()
                .HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.IdClient)
                .OnDelete(DeleteBehavior.Cascade);

            // Un client ne peut signaler qu'une fois le même produit
            modelBuilder.Entity<Signalement>()
                .HasIndex(s => new { s.IdProduit, s.IdClient })
                .IsUnique();

            // ================================================
            // CONFIGURATION DES TYPES DECIMAUX
            // ================================================

            // Produits - Prix
            modelBuilder.Entity<Produit>()
                .Property(p => p.PrixInitial)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Produit>()
                .Property(p => p.PrixGroupe)
                .HasPrecision(10, 2);

            // Stocks - Prix d'achat
            modelBuilder.Entity<Stock>()
                .Property(s => s.PrixAchat)
                .HasPrecision(10, 2);

            // Expéditions - Poids
            modelBuilder.Entity<Expedition>()
                .Property(e => e.Poids)
                .HasPrecision(8, 2);

            // Factures - Montants
            modelBuilder.Entity<Facture>()
                .Property(f => f.PrixUnitaire)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Facture>()
                .Property(f => f.MontantHT)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Facture>()
                .Property(f => f.TVA)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Facture>()
                .Property(f => f.MontantTTC)
                .HasPrecision(10, 2);
        }
    }
}