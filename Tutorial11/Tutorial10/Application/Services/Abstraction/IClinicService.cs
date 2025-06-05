using Tutorial10.Application.DTOs;

namespace Tutorial10.Application.Services.Abstraction;

public interface IClinicService
{
    Task<bool> DoctorExistsAsync(int id);
    Task<bool> MedicamentExistsAsync(int id);
    Task IssuePrescriptionAsync(NewPrescriptionRequest request);
    Task<PatientResponseDto?> GetPatientAsync(int id);
}