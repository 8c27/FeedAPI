using FeedAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace SEPVDB_Api.Models.ViewModels
{
    public class LoginRolesViewModel
    {
        public int? Id { get; set; }
      
        [Required]
        public string Role_name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool Disabled { get; set; }
        [Required]
        public bool Is_admin { get; set; }

        public List<LoginMenus> Menus { get; set; }
    }

    public class LoginRolesSelectorViewModel
    {
        public int Id { get; set; }
        public string Role_name { get; set; }
        public bool Disabled { get; set; }
    }
}
