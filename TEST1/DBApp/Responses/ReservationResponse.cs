namespace DBApp.Responses;

public class ReservationResponse
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int Capacity { get; set; }
    public int NumOfBoats { get; set; }
    public bool Fulfilled { get; set; }
    public double Price { get; set; }
    public string? CancelReason { get; set; }
}