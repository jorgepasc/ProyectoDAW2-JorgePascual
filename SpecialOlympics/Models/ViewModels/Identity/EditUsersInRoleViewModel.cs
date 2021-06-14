using System.ComponentModel.DataAnnotations;

namespace SpecialOlympics.Models.ViewModels.Identity
{
    /// <summary>
    /// ViewModel usado en la vista EditUsersInRole
    /// </summary>
    public class EditUsersInRoleViewModel
    {
        
        public string UserId { get; set; }
        [Required(ErrorMessage = "UsernameRequired")]
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}