using DBApp.Models;
using DBApp.Responses;

namespace DBApp.Repositories;

public interface ITripRepository
{
    Task<List<Trip>> GetAllTripsAsync();
    Task<List<CountryNameRespone>> GetCountryNamesForTrip(int tripId);
    Task<List<ClientNamesResponse>> GetClientNamesForTrip(int tripId);
}