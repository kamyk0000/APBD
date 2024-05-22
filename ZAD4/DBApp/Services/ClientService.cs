using DBApp.Exceptions;
using DBApp.Models;
using DBApp.Repositories;

namespace DBApp.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client> GetClientByIdAsync(int idClient)
    {
        return await _clientRepository.GetClientByIdAsync(idClient);
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        if (!await _clientRepository.ClientHasTripsAsync(idClient) && await _clientRepository.ClientExistsAsync(idClient))
        {
            return await _clientRepository.DeleteClientAsync(idClient);
        }

        return false;
    }
}
