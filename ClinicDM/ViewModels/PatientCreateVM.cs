using ClinicDM.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ClinicDM.ViewModels
{
    public class PatientCreateVM
    {

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string Gender { get; set; }

        [Display(Name = "National Id")]
        public string NationalId { get; set; }

        public string Email { get; set; }

        [RegularExpression("05\\d{8}", ErrorMessage = "Phone number must be in format 05xxxxxxxx")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now.AddYears(-20);


        public Patient ToPatient()
        {
            return new Patient
            {
                FullName = FullName,
                Gender = Gender,
                NationalId = NationalId,
                Email = Email,
                PhoneNumber = PhoneNumber,
                DateOfBirth = DateOfBirth
            };
        }
    }
}