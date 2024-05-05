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
    [HttpPost("{IdProduct, IdWarehouse, Amount, CreatedAt}")]
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
    
    
    /*
    [HttpPost]
        public async Task<IActionResult> ProcessWarehouseRequest([FromBody] WarehouseRequest request)
        {
            try
            {
                // Step 1: Check if the Product and Warehouse exist
                var productExists = await _warehouseService.Product.AnyAsync(p => p.IdProduct == request.IdProduct);
                var warehouseExists = await _warehouseService.Warehouse.AnyAsync(w => w.IdWarehouse == request.IdWarehouse);

                if (!productExists || !warehouseExists)
                {
                    return BadRequest("Product or Warehouse not found");
                }

                // Step 2: Check if an Order exists with the provided IdProduct and amount
                var orderExists = await _warehouseService.Order.AnyAsync(o => o.IdProduct == request.IdProduct 
                                                                              && o.Amount == request.Amount && o.CreatedAt < request.CreatedAt);

                if (!orderExists)
                {
                    return BadRequest("Order not found or transaction is outdated");
                }

                // Step 3: Check if the transaction has already been completed
                var transactionCompleted = await _warehouseService.Product_Warehouse.AnyAsync(pw => pw.IdOrder == request.IdOrder);

                if (transactionCompleted)
                {
                    return BadRequest("Transaction already completed");
                }

                // Step 4: Update FulFilledAt date of the transaction and insert record into Product_Warehouse table
                var newProductWarehouse = new Product_Warehouse
                {
                    IdWarehouse = request.IdWarehouse,
                    IdProduct = request.IdProduct,
                    IdOrder = request.IdOrder,
                    Amount = request.Amount,
                    Price = request.Price * request.Amount,
                    CreatedAt = request.CreatedAt
                };

                _warehouseService.Product_Warehouse.Add(newProductWarehouse);

                var existingTransaction = await _warehouseService.Order.FirstOrDefaultAsync(o => o.IdOrder == request.IdOrder);
                if (existingTransaction != null)
                {
                    existingTransaction.FulfilledAt = DateTime.Now;
                }

                await _warehouseService.SaveChangesAsync();

                return Ok(newProductWarehouse.IdProductWarehouse);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
        */
    
}