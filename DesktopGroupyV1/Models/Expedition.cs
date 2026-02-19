    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("Expeditions")]
    public class Expedition
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idprevente")]
        public int IdPrevente { get; set; }

        [Column("numero_tracking")]
        [MaxLength(100)]
        public string NumeroTracking { get; set; }

        [Column("transporteur")]
        [MaxLength(100)]
        public string Transporteur { get; set; }

        [Column("statut")]
        [MaxLength(50)]
        public string Statut { get; set; } = "en_preparation";

        [Column("date_preparation")]
        public DateTime? DatePreparation { get; set; }

        [Column("date_expedition")]
        public DateTime? DateExpedition { get; set; }

        [Column("date_livraison_prevue")]
        public DateTime? DateLivraisonPrevue { get; set; }

        [Column("date_livraison_reelle")]
        public DateTime? DateLivraisonReelle { get; set; }

        [Column("poids")]
        public decimal? Poids { get; set; }

        [Column("dimensions")]
        [MaxLength(50)]
        public string? Dimensions { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        // Relation de navigation
        [ForeignKey("IdPrevente")]
        public virtual Prevente Prevente { get; set; }
    }
}
