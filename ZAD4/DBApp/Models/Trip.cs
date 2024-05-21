namespace DBApp.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Trip
{
    [Key]
    public int IdTrip { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; }

    [Required]
    [MaxLength(220)]
    public string Description { get; set; }

    [Required]
    public DateTime DateFrom { get; set; }

    [Required]
    public DateTime DateTo { get; set; }

    [Required]
    public int MaxPeople { get; set; }
}
