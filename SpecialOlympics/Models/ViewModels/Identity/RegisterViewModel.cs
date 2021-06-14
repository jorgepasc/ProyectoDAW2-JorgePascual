using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SpecialOlympics.Models.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "EmailInvalid")]
        [Remote(action: "IsEmailInUse", "Account", ErrorMessage = "EmailInUse")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contraseña")]
        [Compare("Password", ErrorMessage = "PasswordsNotMatch")]
        public string ConfirmPassword { get; set; }
    }
}
