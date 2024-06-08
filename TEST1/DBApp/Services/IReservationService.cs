using DBApp.Models;
using DBApp.Responses;

namespace DBApp.Services;

public interface IReservationService
{
    Task<List<ReservationResponse>> GetClientsReservations(int idClient);
}