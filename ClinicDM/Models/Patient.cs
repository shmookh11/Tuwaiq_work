using ClinicDM.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ClinicDM.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string NationalId { get; set; }

        [MaxLength(50)]
        public string Gender { get; set; }

        public string Email { get; set; }

        [RegularExpression("05\\d{8}", ErrorMessage = "Phone number must be in format 05xxxxxxxx")]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }


        public List<Appointment> Appointments { get; set; } = new();


        public PatientVM ToPatientVM()
        {
            return new PatientVM
            {
                Id = Id,
                FullName = FullName,
                Gender = Gender,
                DateOfBirth = DateOfBirth,
                Email = Email,
                NationalId = NationalId,
                PhoneNumber = PhoneNumber,
                Appointments = Appointments.Select(a => a.ToAppointmentVM()).ToList(),
            };
        }

        public PatientUpdateVM ToPatientUpdateVM()
        {
            return new PatientUpdateVM
            {
                Id = Id,
                FullName = FullName,
                Gender = Gender,
                DateOfBirth = DateOfBirth,
                Email = Email,
                NationalId = NationalId,
                PhoneNumber = PhoneNumber
            };
        }


    }
}