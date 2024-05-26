namespace DBApp.Models;

public class Prescription
{
    public int IdPrecription;
    public DateTime Date;
    public DateTime DueDate;
    public ICollection<Patient> Patients;
    public ICollection<Doctor> Doctors;
}