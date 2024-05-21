namespace DBApp.Models;

using System.ComponentModel.DataAnnotations;

public class Client
{
    [Key]
    public int IdClient { get; set; }

    [Required]
    [MaxLength(120)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(120)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(120)]
    public string Email { get; set; }

    [Required]
    [MaxLength(120)]
    public string Telephone { get; set; }

    [Required]
    [MaxLength(120)]
    public string Pesel { get; set; }
}