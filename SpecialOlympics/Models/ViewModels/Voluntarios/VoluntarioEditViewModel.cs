using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpecialOlympics.Models.StoredProcedures;
using System.Collections.Generic;

namespace SpecialOlympics.Models.ViewModels.Voluntarios
{
    public class VoluntarioEditViewModel
        // TODO: Juntar con Create / Details si tienen los mismos campos
    {
        public Voluntario Voluntario { get; set; }        
        [BindProperty(SupportsGet = true)]
        public IFormFile Documento1 { get; set; }
        public IFormFile Foto { get; set; }
        public List<SpGetEntrenamientosFromVoluntarioByID> EntrenamientosFromVoluntario { get; set; }        
        public List<SpGetCampeonatosFromVoluntarioByID> CampeonatosFromVoluntario { get; set; }

    }
}
