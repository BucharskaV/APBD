using Tutorial10.Application.DTOs;

namespace Tutorial10.Application.Services;

public interface IClinicService
{
    Task<bool> DoctorExistsAsync(int id);
    Task<bool> MedicamentExistsAsync(int id);
    Task IssuePrescriptionAsync(NewPrescriptionRequest request);
}