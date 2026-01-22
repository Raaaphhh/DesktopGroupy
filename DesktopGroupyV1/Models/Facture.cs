using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("facture")]
    public class Facture
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idprevente")]
        public int IdPrevente { get; set; }

        [Column("numero_facture")]
        [Required]
        [MaxLength(50)]
        public string NumeroFacture { get; set; }

        [Column("idvendeur")]
        public int IdVendeur { get; set; }

        [Column("idclient")]
        public int IdClient { get; set; }

        [Column("idproduit")]
        public int IdProduit { get; set; }

        [Column("quantite")]
        public int Quantite { get; set; }

        [Column("prix_unitaire")]
        public decimal PrixUnitaire { get; set; }

        [Column("montant_ht")]
        public decimal MontantHT { get; set; }

        [Column("tva")]
        public decimal TVA { get; set; }

        [Column("montant_ttc")]
        public decimal MontantTTC { get; set; }

        [Column("date_emission")]
        public DateTime DateEmission { get; set; }

        [Column("pdf_url")]
        [MaxLength(255)]
        public string PdfUrl { get; set; }

        // Relations de navigation
        [ForeignKey("IdPrevente")]
        public virtual Prevente Prevente { get; set; }

        [ForeignKey("IdVendeur")]
        public virtual Vendeur Vendeur { get; set; }

        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }

        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }

    }
}
