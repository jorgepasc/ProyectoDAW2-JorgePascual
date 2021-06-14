using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialOlympics.Models
{
    [Table("VoluntariosCampeonatos")]
    public class VoluntarioCampeonato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVoluntarioCampeonato { get; set; }

        [ForeignKey(nameof(Voluntario))]
        public int IdVoluntario { get; set; }

        [ForeignKey(nameof(Campeonato))]
        public int IdCampeonato { get; set; }

        [Display(Name = "Función")]
        [StringLength(50, ErrorMessage = "Length50")]
        public string Funcion { get; set; }
    }
}
