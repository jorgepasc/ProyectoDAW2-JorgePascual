using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpecialOlympics.Models.ViewModels.Identity
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "UsernameRequired")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "EmailInvalid")]
        [Required(ErrorMessage = "EmailRequired")]        
        public string Email { get; set; }

        public IList<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}