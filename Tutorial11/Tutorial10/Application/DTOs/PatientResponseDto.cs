namespace Tutorial10.Application.DTOs;

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public List<PrescriptionResponse> Prescriptions { get; set; }
}