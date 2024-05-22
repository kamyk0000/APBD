using DBApp.Models;
using DBApp.Requests;

namespace DBApp.Repositories;

public interface IClientRepository
{
    Task<Client> GetClientByIdAsync(int idClient);
    Task<bool> ClientHasTripsAsync(int idClient);
    Task<int> GetClientNewId();
    Task<bool> ClientExistsAsync(int idClient);
    Task<bool> CreateClientAsync(Client client);
    Task<bool> PeselClientExistsAsync(string pesel);
    Task<bool> DeleteClientAsync(int idClient);
    Task<bool> ClientHasTripAssignedAsync(string pesel, int tripId);
    Task<bool> AssignClientToTripAsync(ClientTripRequest request);
}