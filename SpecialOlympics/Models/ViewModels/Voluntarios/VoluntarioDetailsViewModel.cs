using SpecialOlympics.Models.StoredProcedures;
using System.Collections.Generic;

namespace SpecialOlympics.Models.ViewModels.Voluntarios
{
    public class VoluntarioDetailsViewModel
    {                       
        public Voluntario Voluntario { get; set; }   
        public List<SpGetEntrenamientosFromVoluntarioByID> EntrenamientosFromVoluntario { get; set; }     
        // <!--TODO: Revisar - No entiendo por qué no peta si Model.EntrenamientosFromVoluntario.Count = 0 al evaluar [0] en los encabezados de la tabla (DisplayNameFor)-->
        public List<SpGetCampeonatosFromVoluntarioByID> CampeonatosFromVoluntario { get; set; }

    }
}
