namespace DBApp.Models;

using System.ComponentModel.DataAnnotations;

public class Country_Trip
{
    [Key]
    public int IdCountry { get; set; }

    [Key]
    public int IdTrip { get; set; }
}