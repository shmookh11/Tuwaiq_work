using System.ComponentModel.DataAnnotations;

namespace ClinicDM.ViewModels
{
    public class DoctorCreateVM
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string Specialty { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("05\\d{8}", ErrorMessage = "Phone number must be in format 05xxxxxxxx")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
