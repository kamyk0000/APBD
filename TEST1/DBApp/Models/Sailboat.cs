using System.Data.SqlTypes;

namespace DBApp.Models;

public class Sailboat
{
    public int IdSailboat { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public int IdBoatStandard { get; set; }
    public BoatStandard BoatStandard { get; set; }
    public double Price { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    
}