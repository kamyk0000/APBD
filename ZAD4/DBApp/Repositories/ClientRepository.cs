using DBApp.Exceptions;
using DBApp.Models;
using DBApp.Requests;
using Microsoft.Data.SqlClient;

namespace DBApp.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IConfiguration _configuration;

    public ClientRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Client> GetClientByIdAsync(int idClient)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT * FROM trip.Client WHERE IdClient = @IdClient", connection))
            {
                command.Parameters.AddWithValue("@IdClient", idClient);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Client
                        {
                            IdClient = reader.GetInt32(reader.GetOrdinal("IdClient")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Telephone = reader.GetString(reader.GetOrdinal("Telephone")),
                            Pesel = reader.GetString(reader.GetOrdinal("Pesel"))
                        };
                    }
                    return null;
                }
            }
        }
    }
    public async Task<int> GetClientNewId()
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT MAX(IdClient) FROM trip.Client", connection))
            {
                return (int)await command.ExecuteScalarAsync();
            }
        }
    }
    public async Task<bool> CreateClientAsync(Client client)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            
                await connection.OpenAsync();
                using (var command = new SqlCommand(
                           "INSERT INTO trip.Client(IdClient, FirstName, LastName, Email, Telephone, Pesel) " +
                           "VALUES(@IdClient, @FirstName, @LastName, @Email, @Telephone, @Pesel)", connection))
                {
                    command.Parameters.AddWithValue("@IdClient", client.IdClient);
                    command.Parameters.AddWithValue("@FirstName", client.FirstName);
                    command.Parameters.AddWithValue("@LastName", client.LastName);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@Telephone", client.Telephone);
                    command.Parameters.AddWithValue("@Pesel", client.Pesel);
                    
                    return (int)await command.ExecuteScalarAsync() > 0 ? true : throw new ClientWasNotCreatedException();
                }
            
        }
    }
    public async Task<bool> ClientExistsAsync(int idClient)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT COUNT(*) FROM trip.Client WHERE IdClient = @ClientId", connection))
            {
                command.Parameters.AddWithValue("@ClientId", idClient);
                return (int)await command.ExecuteScalarAsync() > 0 ? true : throw new ClientDoesNotExistException();
            }
        }
    }
        
    public async Task<bool> PeselClientExistsAsync(string pesel)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT COUNT(*) FROM trip.Client WHERE Pesel = @Pesel", connection))
            {
                command.Parameters.AddWithValue("@Pesel", pesel);
                return (int)await command.ExecuteScalarAsync() > 0 ? true : throw new ClientDoesNotExistException();
            }
        }
    }
    public async Task<bool> ClientHasTripAssignedAsync(string pesel, int idTrip)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT COUNT(*) FROM trip.Client_Trip JOIN trip.Client ON Client_Trip.IdClient = Client.IdClient WHERE IdTrip = @TripId AND Pesel = @Pesel", connection))
            {
                command.Parameters.AddWithValue("@TripId", idTrip);
                command.Parameters.AddWithValue("@Pesel", pesel);
                return (int)await command.ExecuteScalarAsync() > 0 ? throw new ClientHasTripsException() : false;
            }
        }
    }
    
    public async Task<bool> ClientHasTripsAsync(int idClient)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT COUNT(*) FROM trip.Client_Trip WHERE IdTrip IS NOT NULL AND IdClient = @ClientId", connection))
            {
                command.Parameters.AddWithValue("@ClientId", idClient);
                return (int)await command.ExecuteScalarAsync() > 0 ? throw new ClientHasTripsException() : false;
            }
        }
    }
    public async Task<bool> AssignClientToTripAsync(ClientTripRequest request)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
           
                using (var command =
                       new SqlCommand(
                           "INSERT INTO trip.Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate) SELECT IdClient, @IdTrip, GETDATE(), @PaymentDate FROM trip.Client WHERE Pesel = @Pesel",
                           connection))
                {
                    command.Parameters.AddWithValue("@Pesel", request.Pesel);
                    command.Parameters.AddWithValue("@IdTrip", request.IdTrip);
                    command.Parameters.AddWithValue("@PaymentDate", request.PaymentDate);
                    
                    return (int)await command.ExecuteNonQueryAsync() > 0 ? true : throw new ClientWasNotCreatedException();
                }
            
        }
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var transaction = connection.BeginTransaction())
            {
                using (var command = new SqlCommand("DELETE FROM trip.Client WHERE IdClient = @IdClient", connection, transaction))
                {
                    command.Parameters.AddWithValue("@IdClient", idClient);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    transaction.Commit();
                    return rowsAffected > 0;
                }
                
                /*
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Rethrow the exception to propagate it up
                }
                */
            }
        }
    }

    
}
