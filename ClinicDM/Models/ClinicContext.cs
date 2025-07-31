using ClinicDM.Helpers;
using ClinicDM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.ClinicModels
{
    public class ClinicContext : IdentityDbContext<AppUser>
    {

        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;




        public ClinicContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تعيين القيمة الافتراضية لتاريخ إنشاء الموعد
            modelBuilder.Entity<Appointment>()
                .Property(x => x.CreationDate)
                .HasDefaultValueSql("GetDate()");

            // إضافة بيانات المرضى (Patients)
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    FullName = "Fay Saleh",
                    NationalId = "123456789",
                    Email = "Fay@gmail.com",
                    PhoneNumber = "0531234567",
                    DateOfBirth = new DateTime(1990, 1, 17),
                    Gender = "Female"
                }
            );

            // إضافة بيانات الأطباء (Doctors)
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    FullName = "Wael Osama",
                    Specialty = "heart",
                    Email = "weal@gmail.com",
                    PhoneNumber = "0501666930"
                }
            );

            // إضافة بيانات المواعيد (Appointments)
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    AppointmentDate = new DateTime(2025, 8, 1, 17, 0, 0),
                    DoctorId = 1,
                    PatientId = 1,
                    Status = "Scheduled"
                },
                new Appointment
                {
                    Id = 2,
                    AppointmentDate = new DateTime(2025, 7, 25, 17, 0, 0),
                    DoctorId = 1,
                    PatientId = 1,
                    Status = "Completed"
                },
                new Appointment
                {
                    Id = 3,
                    AppointmentDate = new DateTime(2025, 8, 5, 17, 0, 0),
                    DoctorId = 1,
                    PatientId = 1,
                    Status = "Scheduled"
                }
            );

            // تغيير أسماء جداول الهوية Identity
            modelBuilder.Entity<AppUser>(b => b.ToTable("Users"));
            modelBuilder.Entity<IdentityRole>(b => b.ToTable("Roles"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(b => b.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserClaim<string>>(b => b.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserRole<string>>(b => b.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserToken<string>>(b => b.ToTable("UserTokens"));
            modelBuilder.Entity<IdentityUserLogin<string>>(b => b.ToTable("UserLogins"));

            // بيانات الأدوار Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "7f04c9e3-ed37-4f63-88cf-b512b6fd61f6",
                    ConcurrencyStamp = "7f04c9e3-ed37-4f63-88cf-b512b6fd61f6",
                    Name = AppRoles.Admin.ToString(),
                    NormalizedName = AppRoles.Admin.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = "2c23dff5-efc4-4e34-945f-1a2f29da0231",
                    ConcurrencyStamp = "2c23dff5-efc4-4e34-945f-1a2f29da0231",
                    Name = AppRoles.Receptionist.ToString(),
                    NormalizedName = AppRoles.Receptionist.ToString().ToUpper()
                },
                new IdentityRole
                {
                    Id = "59fa9387-3202-4e97-aa7f-22a107c6e4a1",
                    ConcurrencyStamp = "59fa9387-3202-4e97-aa7f-22a107c6e4a1",
                    Name = AppRoles.Doctor.ToString(),
                    NormalizedName = AppRoles.Doctor.ToString().ToUpper()
                }
            );
        }


    }
}


