using DBApp.Exceptions;
using DBApp.Models;
using DBApp.Repositories;
using DBApp.Requests;
using DBApp.Responses;

namespace DBApp.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IClientRepository _clientRepository;
    
    public TripService(ITripRepository tripRepository, IClientRepository clientRepository)
    {
        _tripRepository = tripRepository;
        _clientRepository = clientRepository;
    }

    public async Task<List<TripResponse>> GetTripsAsync()
    {
        var allTrips = new List<TripResponse>();
        var trips = await _tripRepository.GetAllTripsAsync();

        foreach (var trip in trips)
        {
            var tripResponse = new TripResponse()
            {
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,
                Countries = await _tripRepository.GetCountryNamesForTripAsync(trip.IdTrip),
                Clients = await _tripRepository.GetClientNamesForTripAsync(trip.IdTrip)
            };
            allTrips.Add(tripResponse);
        }

        return allTrips;
    }

    public async Task<bool> AssignClientToTripAsync(ClientTripRequest request)
    {
        if (await _tripRepository.TripExistsAsync(request.IdTrip))
        {
            try
            {
                await _clientRepository.PeselClientExistsAsync(request.Pesel);
            }
            catch (ClientException)
            {
                Client client = new Client()
                {
                    IdClient = await _clientRepository.GetClientNewId(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Pesel = request.Pesel,
                    Telephone = request.Telephone
                };
            
                await _clientRepository.CreateClientAsync(client);
            }
            
            if (!await _clientRepository.ClientHasTripAssignedAsync(request.Pesel, request.IdTrip))
            {
                return await _clientRepository.AssignClientToTripAsync(request);
            }
        }

        return false;
    }
}