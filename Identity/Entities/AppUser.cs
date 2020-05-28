using Microsoft.AspNetCore.Identity;

namespace Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

    }
}