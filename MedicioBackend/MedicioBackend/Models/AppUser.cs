using Microsoft.AspNetCore.Identity;

namespace MedicioBackend.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
