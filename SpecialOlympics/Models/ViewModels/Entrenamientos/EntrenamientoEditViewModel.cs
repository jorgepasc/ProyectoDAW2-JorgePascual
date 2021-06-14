using SpecialOlympics.Models.StoredProcedures;
using System.Collections.Generic;

namespace SpecialOlympics.Models.ViewModels.Entrenamientos
{
    public class EntrenamientoEditViewModel
    {
        public Entrenamiento Entrenamiento { get; set; }
        public List<SpGetVoluntariosFromActividadByID> VoluntariosFromEntrenamiento { get; set; }
        
        /// <summary>
        /// Lista de voluntarios en activo que no están participando en el entrenamiento
        /// </summary>
        public List<SpGetVoluntariosFromActividadByID> VoluntariosDisponibles { get; set; }

    }
}
