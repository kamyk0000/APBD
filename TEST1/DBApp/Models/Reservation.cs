using System.Data.SqlTypes;

namespace DBApp.Models;

public class Reservation
{
    public int IdReservation { get; set; }
    public int IdClient { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int IdBoatStandard { get; set; }
    public BoatStandard BoatStandard { get; set; }
    public int Capacity { get; set; }
    public int NumOfBoats { get; set; }
    public bool Fulfilled { get; set; }
    public double Price { get; set; }
    public string? CancelReason { get; set; }
    public ICollection<Sailboat> Sailboats { get; set; }
}