namespace DBApp.Models;

public class Patient
{
    public int IdPatient;
    public string FirstName;
    public string LastName;
    public DateTime Birthdate;
    public Prescription Prescription;
    public int IdPrescription;
}