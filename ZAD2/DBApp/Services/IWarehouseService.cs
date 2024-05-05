using DBApp.Models;

namespace DBApp.Services;

public interface IWarehouseService
{
    Task<object> ProcessRequest(WarehouseRequest request);
}