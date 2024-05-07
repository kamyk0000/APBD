using DBApp.Models;

namespace DBApp.Repositories;

public interface IWarehouseRepository
{
    Task<Product> GetProduct(int productId);
    Task<bool> WarehouseExists(int warehouseId);
    Task<Order> GetOrder(int productId, int amount, DateTime createdAt);
    Task<bool> OrderCompleted(int orderId);
    Task<bool> UpdateOrderFulfilledAt(int orderId);
    Task<int?> AddTransaction(Product_Warehouse transaction);
    Task<int?> RunSqlProcedure(int productId, int warehouseId, int amount, DateTime createdAt);

}