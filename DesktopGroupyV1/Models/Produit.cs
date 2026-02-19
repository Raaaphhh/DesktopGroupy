using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("produit")]
    public class Produit
    {
        [Key]
        [Column("idproduit")]
        public int IdProduit { get; set; }

        [Column("reference")]
        [MaxLength(50)]
        public string Reference { get; set; }

        [Column("nom")]
        [Required]
        [MaxLength(255)]
        public string Nom { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("idvendeur")]
        public int IdVendeur { get; set; }

        [Column("idcategorie")]
        public int IdCategorie { get; set; }

        [Column("prix_initial")]
        public decimal PrixInitial { get; set; }

        [Column("prix_groupe")]
        public decimal PrixGroupe { get; set; }

        [Column("nombre_min_acheteurs")]
        public int NombreMinAcheteurs { get; set; }

        [Column("date_limite")]
        public DateTime DateLimite { get; set; }

        [Column("statut")]
        [MaxLength(20)]
        public string Statut { get; set; } = "en_attente";

        [Column("image_url")]
        [MaxLength(255)]
        public string? ImageUrl { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("date_modification")]
        public DateTime? DateModification { get; set; }

        // Relations de navigation
        [ForeignKey("IdVendeur")]
        public virtual Vendeur Vendeur { get; set; }

        [ForeignKey("IdCategorie")]
        public virtual Categorie Categorie { get; set; }

        //[ForeignKey("IdStock")]
        //public virtual Stock Stock { get; set; }
    }
}
