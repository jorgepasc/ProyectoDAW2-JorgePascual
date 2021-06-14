using SpecialOlympics.Models.StoredProcedures;
using System.Collections.Generic;

namespace SpecialOlympics.Models.ViewModels.Campeonatos
{
    public class CampeonatoEditViewModel
    {
        public Campeonato Campeonato { get; set; }
        public List<SpGetVoluntariosFromActividadByID> VoluntariosFromCampeonato { get; set; }
        
        /// <summary>
        /// Lista de voluntarios en activo que no están participando en el campeonato
        /// </summary>
        public List<SpGetVoluntariosFromActividadByID> VoluntariosDisponibles { get; set; }

    }
}
