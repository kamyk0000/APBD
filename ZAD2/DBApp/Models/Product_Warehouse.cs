using System.ComponentModel.DataAnnotations;

namespace DBApp.Models;

public class Product_Warehouse
{
    [Key] 
    public int IdProductWarehouse { get; set; }
    
    [Required] 
    public int IdWarehouse { get; set; }
    
    [Required] 
    public int IdOrder { get; set; }

    [Required] 
    public int IdProduct { get; set; }
    
    [Required] 
    [Range(0, Int32.MaxValue)]
    public int Amount { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required] 
    public DateTime CreatedAt { get; set; }
    
}