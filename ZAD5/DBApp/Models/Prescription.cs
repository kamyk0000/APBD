namespace DBApp.Models;

public class Prescription
{
    public int IdPrecription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public ICollection<Medicament> Medicaments { get; set; }
    public Doctor Doctor { get; set; }
    public int IdDoctor { get; set; }
    public Patient Patient { get; set; }
    public int IdPatient { get; set; }
    
}