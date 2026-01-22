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
    [Table("signalement")]
    public class Signalement
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idproduit")]
        public int IdProduit { get; set; }

        [Column("idclient")]
        public int IdClient { get; set; }

        [Column("motif")]
        [Required]
        [MaxLength(255)]
        public string Motif { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("date_signalement")]
        public DateTime DateSignalement { get; set; }

        [Column("statut")]
        [MaxLength(20)]
        public string Statut { get; set; } = "en_attente";

        // Relations de navigation
        [ForeignKey("IdProduit")]
        public virtual Produit Produit { get; set; }

        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }

    }
}
