using Microsoft.Build.Framework;

namespace MVCSocialMedia.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
