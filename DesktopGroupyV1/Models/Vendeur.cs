using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("vendeur")]
    public class Vendeur
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nom")]
        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Column("email")]
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Column("mot_de_passe")]
        [Required]
        [MaxLength(255)]
        public string MotDePasse { get; set; }

        [Column("telephone")]
        [Required]
        [MaxLength(20)]
        public string Telephone { get; set; }

        [Column("adresse")]
        [Required]
        public string Adresse { get; set; }

        [Column("entreprise")]
        [MaxLength(255)]
        public string Entreprise { get; set; }

        [Column("siret")]
        [MaxLength(14)]
        public string Siret { get; set; }

        [Column("statut")]
        [MaxLength(20)]
        public string Statut { get; set; } = "actif";

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        [Column("date_modification")]
        public DateTime? DateModification { get; set; }

    }
}
