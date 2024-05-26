using DBApp.Models;

namespace DBApp.DTO;

public class NewPrescriptionRequest
{
    public Patient Patient { get; set; }
    public ICollection<Medicament> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}