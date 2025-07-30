namespace ClinicDM.Services
{
    public class PatientService
    {

        public static int Count { get; set; }

        public PatientService()
        {
            Count++;
            Console.WriteLine($"PatientService #{Count}");
        }

    }
}