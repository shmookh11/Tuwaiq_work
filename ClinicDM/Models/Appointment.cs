using ClinicDM.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ClinicDM.Models
{
    public class Appointment
    {

        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime CreationDate { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = null!;


        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;


        public AppointmentVM ToAppointmentVM()
        {
            return new AppointmentVM
            {
                Id = Id,
                AppointmentDate = AppointmentDate,
                DoctorId = DoctorId,
                PatientId = PatientId,
                RawStatus = Status,
                DoctorName = Doctor.FullName,
            };
        }
    }
}