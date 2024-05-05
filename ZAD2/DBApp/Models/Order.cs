using System.ComponentModel.DataAnnotations;

namespace DBApp.Models;

public class Order
{
    [Key] 
    public int IdOrder { get; set; }

    [Required] 
    public int IdProduct { get; set; }
    
    [Required] 
    [Range(0, Int32.MaxValue)]
    public int Amount { get; set; }

    [Required] 
    public DateTime CreatedAt { get; set; }
    
    public DateTime FulfilledAt { get; set; }
    
}