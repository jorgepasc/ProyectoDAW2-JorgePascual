using System.ComponentModel.DataAnnotations;

namespace SpecialOlympics.Models.ViewModels.Identity
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "StringRequired")]
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
    }
}