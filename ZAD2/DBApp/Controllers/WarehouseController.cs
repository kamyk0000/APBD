using DBApp.Models;
using DBApp.Repositories;
using DBApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DBApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    
    /// <summary>
    /// Endpoints used to process a order request.
    /// </summary>
    /// /// <param name="IdProduct, IdWarehouse, Amount, CreatedAt">Formatted request data</param>
    /// <returns>ID key of newly generated record for Product_Warehouse table</returns>
    [HttpPost("RequestAddToWarehouse")]
    public async Task<IActionResult> CreateProductTransaction([FromBody] WarehouseRequest request)
    {
        try
        {
            var result = await _warehouseService.ProcessRequest(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while processing transaction: {ex.Message}");
            return StatusCode(500, "An error occurred while processing the request");
        }
    }
    
    /// <summary>
    /// Endpoints used to process a order request in a sql procedure.
    /// </summary>
    /// /// <param name="IdProduct, IdWarehouse, Amount, CreatedAt">Formatted request data</param>
    /// <returns>ID key of newly generated record for Product_Warehouse table</returns>
    [HttpPost("AddToWarehouse")]
    public async Task<IActionResult> AddProductToWarehouse([FromBody] WarehouseRequest request)
    {
        try
        {
            var result = await _warehouseService.ProcessSqlRequestProcedure(request);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to add product to warehouse");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while adding product to warehouse: {ex.Message}");
            return StatusCode(500, "An error occurred while processing the request");
        }
    }
    
}