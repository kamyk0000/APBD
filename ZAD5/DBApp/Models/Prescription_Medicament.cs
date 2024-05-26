namespace DBApp.Models;

public class Prescription_Medicament
{
    public Medicament Medicament { get; set; }
    public int IdMedicament { get; set; }
    public Prescription Prescription { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}