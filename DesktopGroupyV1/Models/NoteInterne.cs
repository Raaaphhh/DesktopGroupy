using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Models
{
    [Table("notes_internes")]
    public class NoteInterne
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idprevente")]
        public int IdPrevente { get; set; }

        [Column("idvendeur")]
        public int IdVendeur { get; set; }

        [Column("contenu")]
        [Required]
        public string Contenu { get; set; }

        [Column("type_note")]
        [MaxLength(20)]
        public string TypeNote { get; set; } = "info";

        [Column("date_creation")]
        public DateTime DateCreation { get; set; }

        // Relations de navigation
        [ForeignKey("IdPrevente")]
        public virtual Prevente Prevente { get; set; }

        [ForeignKey("IdVendeur")]
        public virtual Vendeur Vendeur { get; set; }

    }
}
