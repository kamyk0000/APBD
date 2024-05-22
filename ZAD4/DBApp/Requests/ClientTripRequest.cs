namespace DBApp.Requests;

using System.ComponentModel.DataAnnotations;

public class ClientTripRequest
{
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
    
    [Key]
    public int IdTrip { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; }
    
    public DateTime? PaymentDate { get; set; }
}