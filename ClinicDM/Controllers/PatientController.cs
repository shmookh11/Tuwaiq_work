using ClinicDM.Models;
using ClinicDM.Services;
using ClinicDM.ViewModels;
using EFCore.ClinicModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicDM.Controllers
{

    [Authorize]
    public class PatientController : Controller
    {

        public ClinicContext context;
        public PatientService patientService;
        public AnotherService anotherService;

        public PatientController(ClinicContext context, PatientService patientService, AnotherService anotherService)
        {
            this.context = context;
            this.patientService = patientService;
            this.anotherService = anotherService;
        }


        public IActionResult Index(PatientFilterVM filter)
        {

            var patients = context.Patients
                                    .Where(p => filter.Id == null || p.Id == filter.Id)
                                    .Where(p => filter.FullName == null || p.FullName.Contains(filter.FullName))
                                    .Where(p => filter.PhoneNumber == null || p.PhoneNumber.StartsWith(filter.PhoneNumber))
                                    .Select(p => p.ToPatientVM())
                                    .ToList();

            var vm = new PatientFilteredListVM
            {
                Data = patients,
                Filter = filter
            };

            //ViewData["CreatedBy"] = "Omar Abdelkerim";
            //ViewBag.CreatedBy = "Omar Abdelkerim";


            return View(vm);
        }

        public IActionResult Details(int id)
        {

            var patient = context.Patients
                             .Include(p => p.Appointments)
                             .ThenInclude(a => a.Doctor)
                             .FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            var vm = patient.ToPatientVM();
            return View(vm);
        }


        public IActionResult Create()
        {
            var vm = new PatientCreateVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientCreateVM newPatient)
        {

            if (!ModelState.IsValid)
            {
                return View(newPatient);
            }

            var patient = newPatient.ToPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var patient = context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            var vm = patient.ToPatientUpdateVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PatientUpdateVM updatedPatient)
        {

            if (!ModelState.IsValid)
            {
                return View(updatedPatient);
            }

            var existingPatient = context.Patients.FirstOrDefault(p => p.Id == id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            updatedPatient.ToPatient(existingPatient);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var patient = context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            context.Patients.Remove(patient);
            context.SaveChanges();
            return Ok();
        }
    }
}