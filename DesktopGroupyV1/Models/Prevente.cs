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
    [Table("prevente")]
    public class Prevente
    {
        [Key]
        [Column("idprevente")]
        public int IdPrevente { get; set; }

        [Column("idproduit")]
        public int IdProduit { get; set; }

        [Column("idclient")]
        public int IdClient { get; set; }

        [Column("quantite")]
        public int Quantite { get; set; }

        [Column("statut")]
        [MaxLength(20)]
        public string Statut { get; set; } = "en_attente";

        [Column("date_prevente")]
        public DateTime DatePrevente { get; set; }

        [Column("date_confirmation")]
        public DateTime? DateConfirmation { get; set; }

        // Relations de navigation
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }

        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }

    }
}
