using System.ComponentModel.DataAnnotations;

namespace DBApp.Models;

public class Product
{
    [Key]
    public int IdProduct { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

}