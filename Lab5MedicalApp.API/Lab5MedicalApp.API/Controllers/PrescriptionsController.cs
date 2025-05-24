using Lab5MedicalApp.API.Data;
using Lab5MedicalApp.API.Models.Domain;
using Lab5MedicalApp.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5MedicalApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly Lab5MedicalAppDbContext _context;

    public PrescriptionsController(Lab5MedicalAppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequest request)
    {
        // Walidacja daty
        if (request.DueDate < request.Date)
        {
            return BadRequest("DueDate must be greater than or equal to Date.");
        }

        // Walidacja liczby leków
        if (request.Medicaments.Count > 10)
        {
            return BadRequest("Prescription cannot contain more than 10 medicaments.");
        }

        // Walidacja istnienia lekarza
        var doctor = await _context.Doctors.FindAsync(request.Doctor.IdDoctor);
        if (doctor == null)
        {
            return NotFound($"Doctor with ID {request.Doctor.IdDoctor} not found.");
        }

        // Sprawdzenie istnienia pacjenta lub dodanie nowego
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.FirstName == request.Patient.FirstName &&
                                      p.LastName == request.Patient.LastName &&
                                      p.BirthDate == request.Patient.BirthDate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.BirthDate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        // Walidacja istnienia każdego leku
        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        var notFoundIds = medicamentIds.Except(existingMedicaments).ToList();
        if (notFoundIds.Any())
        {
            return NotFound($"Medicaments with IDs [{string.Join(", ", notFoundIds)}] not found.");
        }

        // Tworzenie recepty
        var prescription = new Perscription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient,
            PerscriptionMedicaments = request.Medicaments.Select(m => new PerscriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        _context.Perscriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Prescription created successfully.", prescription.IdPerscription });
    }
}
