namespace DBApp.Responses;

public class TripResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryNameRespone> Countries { get; set; }
    public List<ClientNamesResponse> Clients { get; set; }
}
