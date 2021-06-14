using System.ComponentModel.DataAnnotations;

namespace SpecialOlympics.Models.StoredProcedures
{
    /// <summary>
    /// Result del procedimiento almacenado que devuelve los campeonatos en los que ha participado un voluntario
    /// TODO: Crear otro procedimiento o cambiar este para que filtre por año
    /// </summary>
    public class SpGetCampeonatosFromVoluntarioByID
    {
        // TODO: Quitar esa [Key] inventada y ponerlo como Keyless
        [Key]
        [Display(Name = "ID")]
        public int IdCampeonato { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Fecha Inicio"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public System.DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha Fin"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public System.DateTime? FechaFin { get; set; }
        
        [Display(Name = "Función")]
        public string Funcion { get; set; }

        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }
    }
}
