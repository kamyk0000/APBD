using DBApp.Models;
using DBApp.Responses;
using Microsoft.Data.SqlClient;

namespace DBApp.Repositories;

public class TripRepository : ITripRepository
{
    private IConfiguration _configuration;
    
    public TripRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<Trip>> GetAllTripsAsync()
    {
        var trips = new List<Trip>();
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT IdTrip, Name, Description, DateFrom, DateTo, MaxPeople FROM trip.Trip ORDER BY DateFrom DESC", connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var trip = new Trip()
                        {
                            IdTrip = reader.GetInt32(reader.GetOrdinal("IdTrip")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            DateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom")),
                            DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                            MaxPeople = reader.GetInt32(reader.GetOrdinal("MaxPeople"))
                        };

                        trips.Add(trip);
                    }
                    return trips;
                }
            }
        }
    }
    
    public async Task<List<CountryNameRespone>> GetCountryNamesForTrip(int tripId)
    {
        var countries = new List<CountryNameRespone>();
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT Name FROM trip.Country JOIN trip.Country_Trip ON trip.Country.IdCountry = trip.Country_Trip.IdCountry WHERE IdTrip = @TripId", connection))
            {
                command.Parameters.AddWithValue("@TripId", tripId);
                
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var country = new CountryNameRespone
                        {
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                        
                        countries.Add(country);
                    }
                    return countries;
                }
            }
        }
    }
    
    public async Task<List<ClientNamesResponse>> GetClientNamesForTrip(int tripId)
    {
        var clients = new List<ClientNamesResponse>();
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT FirstName, LastName FROM trip.Client JOIN trip.Client_Trip ON trip.Client.IdClient = trip.Client_Trip.IdClient WHERE IdTrip = @TripId", connection))
            {
                command.Parameters.AddWithValue("@TripId", tripId);
                
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var client = new ClientNamesResponse
                        {
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName"))
                        };
                        
                        clients.Add(client);
                    }
                    return clients;
                }
            }
        }
    }
    
}