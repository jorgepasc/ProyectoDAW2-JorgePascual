using System.ComponentModel.DataAnnotations;

namespace SpecialOlympics.Models.ViewModels.Identity
{
    public class EditRolesInUserViewModel
    {
        // La pasamos por ViewBag para no duplicar la UserId para cada Rol
        //public string UserId { get; set; }
        public string RoleId { get; set; }
        [Required(ErrorMessage = "StringRequired")]
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}