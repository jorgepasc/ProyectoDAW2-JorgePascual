using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialOlympics.Models
{
    [Table("Entrenamientos")]
    public class Entrenamiento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntrenamiento { get; set; }

        [Required(ErrorMessage = "StringRequired")]
        [StringLength(50, ErrorMessage = "Length50")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "StringRequired")]
        [StringLength(400, ErrorMessage = "Length400")]
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "DateInvalid")]
        [Display(Name = "Año"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaInicio { get; set; }

        //[Display(Name = "Año")]
        //[DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? FechaFin { get; set; }

        //[Required(ErrorMessage = "StringRequired")]
        [StringLength(400, ErrorMessage = "Length400")]
        public string Observaciones { get; set; }


    }
}
