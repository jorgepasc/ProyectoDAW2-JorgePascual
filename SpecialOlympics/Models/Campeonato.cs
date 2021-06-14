using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialOlympics.Models
{
    [Table("Campeonatos")]
    public class Campeonato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCampeonato { get; set; }
        
        [Required(ErrorMessage = "StringRequired")]
        [StringLength(100, ErrorMessage = "Length100")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "DateRequired")]
        [Display(Name = "Fecha Inicio"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaInicio { get; set; }

        [Required(ErrorMessage = "DateRequired")]
        [Display(Name = "Fecha Fin"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "StringRequired")]
        [StringLength(400, ErrorMessage = "Length400")]
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }
    }
}
