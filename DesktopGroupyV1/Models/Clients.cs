using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        [Column("idClient")]
        public int IdClient { get; set; }

        [Column("nom")]
        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Column("prenom")]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [Column("email")]
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Column("telephone")]
        [MaxLength(20)]
        public string Telephone { get; set; }

        [Column("adresse")]
        public string Adresse { get; set; }

        [Column("ville")]
        [MaxLength(100)]
        public string Ville { get; set; }

        [Column("code_postal")]
        [MaxLength(10)]
        public string CodePostal { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        // Propriété calculée (non en base)
        [NotMapped]
        public string NomComplet => $"{Prenom} {Nom}";
    }
}
