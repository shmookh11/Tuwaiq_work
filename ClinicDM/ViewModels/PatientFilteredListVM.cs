namespace ClinicDM.ViewModels
{
    public class PatientFilteredListVM
    {

        public PatientFilterVM Filter { get; set; }

        public List<PatientVM> Data { get; set; }
    }
}