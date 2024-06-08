using System.Data.SqlTypes;

namespace DBApp.Models;

public class BoatStandard
{
    public int IdBoatStandard { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public IEnumerable<Sailboat> Sailboats { get; set; }
    public IEnumerable<Reservation> Reservations { get; set; }

}