using System.ComponentModel.DataAnnotations;

namespace DBApp.Models;

public class WarehouseRequest
{
    [Required] 
    public int IdProduct { get; set; }
    [Required] 
    public int IdWarehouse { get; set; }
    [Required] 
    [Range(0, Int32.MaxValue)]
    public int Amount { get; set; }
    [Required] 
    public DateTime CreatedAt { get; set; }
}