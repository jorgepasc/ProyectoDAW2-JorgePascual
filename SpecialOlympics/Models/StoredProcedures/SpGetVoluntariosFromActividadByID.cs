using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialOlympics.Models.StoredProcedures
{
    /// <summary>
    /// Result del procedimiento almacenado que devuelve los campeonatos/entrenamientos/formaciones en los que ha participado un voluntario. 
    /// Se agrupa en el mismo modelo por ahora porque necesitamos los mismos campos
    /// TODO: Crear otro procedimiento o adaptar este para que filtre por año
    /// </summary>
    public class SpGetVoluntariosFromActividadByID
    {
        // TODO: Quitar esa [Key] inventada y ponerlo como Keyless
        [Key]
        [Display(Name = "ID")]
        public int IdVoluntario { get; set; }

        /// <summary>
        /// ID primario de la tabla VoluntariosEntrenamientos o VoluntariosCampeonatos
        /// </summary>
        public int IdVoluntarioActividad { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono1 { get; set; }

        public string Email { get; set; }

        [Display(Name = "Función")]
        public string Funcion { get; set; }

    }
}
