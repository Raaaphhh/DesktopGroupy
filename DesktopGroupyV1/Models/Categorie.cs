using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("categories")]
    public class Categorie
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nom")]
        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }
    }
}
