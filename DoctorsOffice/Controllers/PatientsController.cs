using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorsOffice.Controllers
{
  public class PatientsController : Controller
  {

    private readonly DoctorsOfficeContext _db;
    public PatientsController(DoctorsOfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patients.ToList());
    }

    public ActionResult Create()
    {
      // You have to reference the actual name of the field "DoctorName" => "Name"
      //ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "DoctorName");
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patient patient, int DoctorId)
    {
      _db.Patients.Add(patient);

      if (DoctorId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = DoctorId, PatientId = patient.PatientId });
      }

      _db.SaveChanges();
      return RedirectToAction("Index");
      // return RedirectToAction("SelectSpecialty");
    }

    public ActionResult Details(int id)
    {
      var thisPatient = _db.Patients
        .Include(patient => patient.JoinEntities)
        .ThenInclude(join => join.Doctor)
        .FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }

    public ActionResult Edit(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "DoctorName");
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult Edit(Patient patient, int DoctorId)
    {
      if (DoctorId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = DoctorId, PatientId = patient.PatientId });
      }
      _db.Entry(patient).State = EntityState.Modified;
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
      return View(thisPatient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
      _db.Patients.Remove(thisPatient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddDoctor(int id)
    {
      var thisPatient = _db.Patients.FirstOrDefault(p => p.PatientId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View (thisPatient);
    }

    [HttpPost]
    public ActionResult AddDoctor(Patient patient, int DoctorId)
    {
      if (DoctorId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = DoctorId, PatientId = patient.PatientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    // FRIDAY PROJECT
    // Make sure you can remove students, remove courses, and also unenroll
    // students from courses (aka delete the join relationship between the two)
    [HttpPost]
    public ActionResult DeleteDoctor(int joinId)
    {
      System.Console.WriteLine("joinId => " + joinId);
      var joinEntry = _db.DoctorPatient.FirstOrDefault(j => j.DoctorPatientId == joinId);
      _db.DoctorPatient.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // public ActionResult SelectSpecialty(Patient patient)
    // {
    //   ViewBag.SpecialtyId = new SelectList(_db.Specialties, "SpecialtyId", "Name");
    //   return View();
    // }

    // [HttpPost]
    // public ActionResult SelectSpecialty(Specialty specialty)
    // {
    //   // linq query to find all doctors of that specialty
    //   // return RedirectToAction("AddDoctor");
    // }
  }
}

// Add Patient => Create[GET]
// Create Patient => Create[POST] => Redirect to Index
// Details[PatientId] => 
// Add Doctor to Patient => Redirect to view SelectSpecialty
// Select Specialty => Post[SelectSpecialty] => AddDoctor(specialties)
// Select Doctor with that Specialty => Post[AddSpecialist] => Redirect to Index 
// Add Doctor To patient from list of specialists