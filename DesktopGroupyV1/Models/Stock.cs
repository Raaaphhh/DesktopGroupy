using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("stock")]    
    public class Stock
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idproduit")]
        public int IdProduit { get; set; }

        [Column("stock_physique")]
        public int StockPhysique { get; set; }

        [Column("stock_reserve")]
        public int StockReserve { get; set; }

        [Column("seuil_alerte")]
        public int SeuilAlerte { get; set; } = 10;

        [Column("prix_achat")]
        public decimal? PrixAchat { get; set; }

        [Column("emplacement")]
        [MaxLength(50)]
        public string Emplacement { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("date_modification")]
        public DateTime? DateModification { get; set; }

        // Propriétés calculées (non en base)
        [NotMapped]
        public int StockDisponible => StockPhysique - StockReserve;

        [NotMapped]
        public bool EnAlerte => StockPhysique < SeuilAlerte;

        [NotMapped]
        public decimal ValeurStock => StockPhysique * (PrixAchat ?? 0);

        // Relation de navigation
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }

    }
}
