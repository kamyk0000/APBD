using DBApp.Models;

namespace DBApp.DTO;

public class PatientInfoResponse
{
    public Patient Patient { get; set; }
    public ICollection<PrescriptionResponse> Prescriptions { get; set; }
}