using Microsoft.AspNetCore.Mvc;
using ClinicDM.Models;
using ClinicDM.ViewModels;
using System.Linq;

namespace ClinicDM.Controllers
{
    public class DoctorController : Controller
    {
        // استخدمنا Constants.Doctors حسب مشروعك
        public IActionResult Index(DoctorFilterVM filter)
        {
            var doctors = Constants.Doctors
                .Where(d => !filter.Id.HasValue || d.Id == filter.Id)
                .Where(d => string.IsNullOrEmpty(filter.FullName) || d.FullName.Contains(filter.FullName, StringComparison.OrdinalIgnoreCase))
                .Where(d => string.IsNullOrEmpty(filter.PhoneNumber) || d.PhoneNumber.StartsWith(filter.PhoneNumber))
                .ToList();

            var vm = new DoctorFilteredListVM
            {
                Filter = filter,
                Data = doctors
            };

            return View(vm);
        }


        public IActionResult Details(int id)
        {
            var doctor = Constants.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        public IActionResult Create()
        {
            var doctor = new Doctor();
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Create(Doctor newDoctor)
        {
            if (!ModelState.IsValid)
            {
                return View(newDoctor);
            }

            int newId = Constants.Doctors.Any() ? Constants.Doctors.Max(d => d.Id) + 1 : 1;
            newDoctor.Id = newId;

            Constants.Doctors.Add(newDoctor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var doctor = Constants.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Update(Doctor updatedDoctor)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedDoctor);
            }

            var doctor = Constants.Doctors.FirstOrDefault(d => d.Id == updatedDoctor.Id);
            if (doctor == null) return NotFound();

            doctor.FullName = updatedDoctor.FullName;
            doctor.Specialty = updatedDoctor.Specialty;
            doctor.Email = updatedDoctor.Email;
            doctor.PhoneNumber = updatedDoctor.PhoneNumber;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var doctor = Constants.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null) return NotFound();

            Constants.Doctors.Remove(doctor);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = Constants.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null) return NotFound();

            Constants.Doctors.Remove(doctor);
            return RedirectToAction(nameof(Index));
        }
    }
}
