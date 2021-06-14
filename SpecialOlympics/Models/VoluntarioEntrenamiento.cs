using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialOlympics.Models
{
    [Table("VoluntariosEntrenamientos")]
    public class VoluntarioEntrenamiento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVoluntarioEntrenamiento { get; set; }

        [ForeignKey(nameof(Voluntario))]
        public int IdVoluntario { get; set; }

        [ForeignKey(nameof(Entrenamiento))]
        public int IdEntrenamiento { get; set; }

        [Display(Name = "Función")]
        [StringLength(50, ErrorMessage = "Length50")]
        public string Funcion { get; set; }
    }
}
