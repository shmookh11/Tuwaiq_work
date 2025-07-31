using ClinicDM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ClinicDM.ViewModels
{
    public class UserCreateVM
    {

        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        [EnumDataType(typeof(AppRoles))]
        public string Role { get; set; } = null!;

        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePicture { get; set; }
    }
}