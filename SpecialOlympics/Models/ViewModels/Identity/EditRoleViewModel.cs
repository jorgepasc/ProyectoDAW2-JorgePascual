using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpecialOlympics.Models.ViewModels.Identity
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "StringRequired")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}