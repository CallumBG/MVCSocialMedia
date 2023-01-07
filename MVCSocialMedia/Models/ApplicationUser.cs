using Microsoft.AspNetCore.Identity;

namespace MVCSocialMedia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePicURL { get; set; }
    }
}
