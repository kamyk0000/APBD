using DBApp.Models;
using DBApp.Repositories;

namespace DBApp.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
        
    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    
    public async Task<object> ProcessRequest(WarehouseRequest request)
    {
        // 1: Check if the Product and Warehouse exist and get the Product (or should I only get the Price??)
        var product = await _warehouseRepository.GetProduct(request.IdProduct);
        var warehouseExists = await _warehouseRepository.WarehouseExists(request.IdWarehouse);

        if (product == null || !warehouseExists)
        {
            return "Product or Warehouse not found";
        }
        
        var price = product.Price;

        // 2: Check if an Order exists with the provided IdProduct and amount and get the Order (or should I only get the IdOrder??)
        var order = await _warehouseRepository.GetOrder(request.IdProduct, request.Amount, request.CreatedAt);

        if (order == null)
        {
            return "Order not found or transaction is outdated";
        }
        
        var orderId = order.IdOrder;

        // 3: Check if the order has already been completed
        var orderCompleted = await _warehouseRepository.OrderCompleted(orderId);

        if (orderCompleted)
        {
            return "Transaction already completed";
        }

        // 4: Update FulfilledAt date of the order
        await _warehouseRepository.UpdateOrderFulfilledAt(orderId);
        
        // 5:  Insert record into Product_Warehouse table
        var newProductWarehouse = new Product_Warehouse
        {
            IdWarehouse = request.IdWarehouse,
            IdProduct = request.IdProduct,
            IdOrder = orderId,
            Amount = request.Amount,
            Price = price * request.Amount,
            CreatedAt = request.CreatedAt
        };
        
        // 6: Return the key of inserted record;
        var result = await _warehouseRepository.AddTransaction(newProductWarehouse);

        if (result == null)
        {
            return "Something went wrong";
        }
        return result;
    }

    public async Task<object> ProcessSqlRequestProcedure(WarehouseRequest request)
    {
        var result =  await _warehouseRepository.RunSqlProcedure(request.IdProduct, request.IdWarehouse, request.Amount, request.CreatedAt);
        
        if (result == null)
        {
            return "Something went wrong";
        }
        
        return result;
    }
}