using DBApp.Models;

namespace DBApp.Repositories;

public interface IClientRepository
{
    Task<Client> GetClientByIdAsync(int idClient);
    Task<bool> ClientHasTripsAsync(int idClient);
    Task<bool> ClientExistsAsync(int idClient);
    Task<bool> DeleteClientAsync(int idClient);
}