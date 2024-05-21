namespace DBApp.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Client_Trip
{
    [Key]
    public int IdClient { get; set; }

    [Key]
    public int IdTrip { get; set; }

    [Required]
    public DateTime RegisteredAt { get; set; }

    public DateTime? PaymentDate { get; set; }
}