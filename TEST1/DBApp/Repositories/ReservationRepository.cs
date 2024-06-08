using DBApp.Models;
using DBApp.Responses;
using Microsoft.Data.SqlClient;

namespace DBApp.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly IConfiguration _configuration;

    public ReservationRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<List<ReservationResponse>> GetClientsReservations(int idClient)
    { var reservations = new List<ReservationResponse>();
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(
                       "SELECT DateTo, DateFrom, Capacity, NumOfBoats, Fulfilled, Price, CancelReason FROM Reservation JOIN Client ON Reservation.IdClient = Client.IdClient WHERE Client.IdClient = @ClientId ORDER BY DateTo"
                       , connection))
            {
                command.Parameters.AddWithValue("@ClientId", idClient);
                
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var reservation = new ReservationResponse()
                        {
                            DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                            DateFrom = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                            Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                            NumOfBoats = reader.GetInt32(reader.GetOrdinal("NumOfBoats")),
                            Fulfilled = reader.GetBoolean(reader.GetOrdinal("Fulfilled")),
                            Price = reader.GetDouble(reader.GetOrdinal("Price")),
                            CancelReason = reader.IsDBNull(reader.GetOrdinal("CancelReason")) ? null : reader.GetString(reader.GetOrdinal("CancelReason"))
                        };
                        
                        reservations.Add(reservation);
                    }
                    return reservations;
                }
            }
        }
    }
}