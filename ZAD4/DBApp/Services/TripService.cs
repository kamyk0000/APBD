using DBApp.Repositories;
using DBApp.Responses;

namespace DBApp.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    
    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
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
                Countries = await _tripRepository.GetCountryNamesForTrip(trip.IdTrip),
                Clients = await _tripRepository.GetClientNamesForTrip(trip.IdTrip)
            };
            allTrips.Add(tripResponse);
        }

        return allTrips;
    }
}