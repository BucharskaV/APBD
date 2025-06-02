using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.DTOs;
using Tutorial10.Application.Exceptions;
using Tutorial10.Core.Models;

namespace Tutorial10.Application.Services;

public class ClinicService(ClinicDBContext context) : IClinicService
{
    public async Task<bool> DoctorExistsAsync(int id)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.IdDoctor == id);
        return doctor is not null;
    }
    
    public async Task<bool> MedicamentExistsAsync(int id)
    {
        var m = await context.Medicaments.FirstOrDefaultAsync(x => x.IdMedicament == id);
        return m is not null;
    }
    
    public async Task IssuePrescriptionAsync(NewPrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
            throw new MedicamentsNumberException();
        if (request.DueDate < request.Date)
            throw new DueDateException();
        if(!await DoctorExistsAsync(request.IdDoctor))
            throw new DoctorDoesNotExistsException();
        var medicamentEntities = await context.Medicaments
            .Where(m => request.Medicaments.Select(dm => dm.IdMedicament).Contains(m.IdMedicament))
            .ToListAsync();
        if (medicamentEntities.Count != request.Medicaments.Count)
            throw new MedicamentsNotFoundException();

        var patient = await context.Patients.FirstOrDefaultAsync(x => x.IdPatient == request.Patient.IdPatient);
        if (patient is null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                DateOfBirth = request.Patient.DateOfBirth,
            };
        }
        
        var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.IdDoctor == request.IdDoctor);
        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Doctor = doctor,
            Patient = patient
        };
        
        foreach (var m in request.Medicaments)
        {
            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            });
        }
        
        context.Prescriptions.Add(prescription);
        await context.SaveChangesAsync();
    }

    public async Task<PatientResponseDto?> GetPatientAsync(int id)
    {
        var patient = await context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null) throw new PatientNotFoundException();
        
        var dto = new PatientResponseDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Prescriptions = patient.Prescriptions.OrderBy(p => p.DueDate).Select(p => new PrescriptionResponse()
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new DoctorResponse()
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName
                },
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentsResponse()
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Details
                }).ToList()
            }).ToList()
        };
        
        return dto;
    }
}