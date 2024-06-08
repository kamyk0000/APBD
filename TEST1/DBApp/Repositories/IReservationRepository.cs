using DBApp.Models;
using DBApp.Responses;

namespace DBApp.Repositories;

public interface IReservationRepository
{
    Task<List<ReservationResponse>> GetClientsReservations(int idClient);

}