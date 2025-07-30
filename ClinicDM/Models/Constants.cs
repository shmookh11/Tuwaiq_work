using System;
using System.Collections.Generic;

namespace ClinicDM.Models
{
    public static class Constants
    {
        public static List<Patient> Patients = new List<Patient> {
            new Patient {
                DateOfBirth = new DateTime(1990, 1, 17),
                Id = 1,
                FullName = "Fay Saleh",
                NationalId = "123456789",
                Email = "Fay@gmail.com",
                PhoneNumber = "0531234567"
            },
            new Patient {
                DateOfBirth = new DateTime(1980, 12, 1),
                Id = 2,
                FullName = "Norah Kahlid",
                NationalId = "987654321",
                Email = "Norah@gmail.com",
                PhoneNumber = "0541234567"
            },
            new Patient {
                DateOfBirth = new DateTime(2000, 9, 28),
                Id = 3,
                FullName = "Muhammad Saad",
                NationalId = "999999",
                Email = "Muhammad@gmail.com",
                PhoneNumber = "0551234567"
            }
        };

        public static List<Doctor> Doctors = new List<Doctor>
        {
            new Doctor
            {
                Id = 1,
                FullName = "Dr. Amal Salem",
                Specialty = "Cardiology",
                Email = "amal@example.com",
                PhoneNumber = "0551234567"
            },
            new Doctor
            {
                Id = 2,
                FullName = "Dr. Hamed Faisal",
                Specialty = "Dermatology",
                Email = "hamed@example.com",
                PhoneNumber = "0559876543"
            }
        };

        
        
    }
}
