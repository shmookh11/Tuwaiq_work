using Microsoft.AspNetCore.Identity;

namespace ClinicDM.Models
{
    public class AppUser : IdentityUser
    {

        public byte[]? ProfilePicture { get; set; }
    }
}