using DBApp.Exceptions;
using DBApp.Models;
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
