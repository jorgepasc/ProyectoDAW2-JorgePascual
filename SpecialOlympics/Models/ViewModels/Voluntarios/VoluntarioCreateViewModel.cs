using Microsoft.AspNetCore.Http;

namespace SpecialOlympics.Models.ViewModels.Voluntarios
{
    public class VoluntarioCreateViewModel
    {
        public Voluntario Voluntario { get; set; }
        public IFormFile Documento1 { get; set; }
        public IFormFile Foto { get; set; }

    }
}
