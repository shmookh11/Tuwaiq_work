using System.ComponentModel.DataAnnotations;

namespace ClinicDM.ViewModels
{
    public class PatientFilterVM
    {
        public int? Id { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
