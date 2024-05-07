using System.Data;
using DBApp.Models;
using DBApp.Services;
using Microsoft.Data.SqlClient;

namespace DBApp.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;
        
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<Product> GetProduct(int productId)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT IdProduct, Name, Description, Price FROM Product WHERE IdProduct = @ProductId", connection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Product
                        {
                            IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                        };
                    }
                    return null; // No matching product found
                }
                
                // Do i need it?
                //var dr = command.ExecuteReader();
                //if (!dr.Read()) return false;
                
            }
        }
    }

    public async Task<bool> WarehouseExists(int warehouseId)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @WarehouseId", connection))
            {
                command.Parameters.AddWithValue("@WarehouseId", warehouseId);
                return (int)await command.ExecuteScalarAsync() > 0;
            }
        }
    }

    public async Task<Order> GetOrder(int productId, int amount, DateTime createdAt)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT IdOrder, CreatedAt FROM [Order] WHERE IdProduct = @ProductId AND Amount = @Amount AND CreatedAt < @CreatedAt ORDER BY CreatedAt DESC", connection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@CreatedAt", createdAt);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Order
                        {
                            IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
                            IdProduct = productId,
                            Amount = amount,
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        };
                    }
                    return null; // No matching order found
                }
            }
        }
    }

    public async Task<bool> OrderCompleted(int orderId)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @OrderId", connection))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);
                return (int)await command.ExecuteScalarAsync() > 0;
            }
        }
    }
    
    public async Task<bool> UpdateOrderFulfilledAt(int orderId)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder = @OrderId", connection))
            {
                command.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
                command.Parameters.AddWithValue("@OrderId", orderId);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<int?> AddTransaction(Product_Warehouse transaction)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) OUTPUT INSERTED.IdProductWarehouse VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)", connection))
            {
                command.Parameters.AddWithValue("@IdWarehouse", transaction.IdWarehouse);
                command.Parameters.AddWithValue("@IdProduct", transaction.IdProduct);
                command.Parameters.AddWithValue("@IdOrder", transaction.IdOrder);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Price", transaction.Price);
                command.Parameters.AddWithValue("@CreatedAt", transaction.CreatedAt);

                var newId = await command.ExecuteScalarAsync();
                return newId != DBNull.Value ? Convert.ToInt32(newId) : (int?)null;
            }
        }
    }

    public async Task<int?> RunSqlProcedure(int productId, int warehouseId, int amount, DateTime createdAt)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("AddProductToWarehouse", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProduct", productId);
                command.Parameters.AddWithValue("@IdWarehouse", warehouseId);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@CreatedAt", createdAt);

                var result = await command.ExecuteScalarAsync();
                return result != null ? Convert.ToInt32(result) : (int?)null;
            }
        }
    }
}