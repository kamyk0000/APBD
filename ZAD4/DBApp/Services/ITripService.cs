using DBApp.Requests;
using DBApp.Responses;

namespace DBApp.Services;

public interface ITripService
{
    Task<List<TripResponse>> GetTripsAsync();
}