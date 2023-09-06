using Microsoft.AspNetCore.Identity;

namespace Surf.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
