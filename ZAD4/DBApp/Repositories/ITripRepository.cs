using DBApp.Models;
using DBApp.Requests;
using DBApp.Responses;

namespace DBApp.Repositories;

public interface ITripRepository
{
    Task<List<Trip>> GetAllTripsAsync();
    Task<List<CountryNameRespone>> GetCountryNamesForTripAsync(int tripId);
    Task<List<ClientNamesResponse>> GetClientNamesForTripAsync(int tripId);
    Task<bool> TripExistsAsync(int tripId);
}