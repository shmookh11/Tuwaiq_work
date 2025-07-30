using ClinicDM.Models;

namespace ClinicDM.ViewModels
{
    public class DoctorFilteredListVM
    {
        public DoctorFilterVM Filter { get; set; }
        public List<Doctor> Data { get; set; }
    }
}
