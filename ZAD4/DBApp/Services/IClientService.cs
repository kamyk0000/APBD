using DBApp.Models;

namespace DBApp.Services;

public interface IClientService
{
    Task<Client> GetClientByIdAsync(int idClient);
    Task<bool> DeleteClientAsync(int idClient);
}