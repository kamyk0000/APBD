namespace DBApp.Models;

using System.ComponentModel.DataAnnotations;

public class Country
{
    [Key]
    public int IdCountry { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; }
}

