using DBApp.Models;
using DBApp.Repositories;
using DBApp.Responses;

namespace DBApp.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<List<ReservationResponse>> GetClientsReservations(int idClient)
    {
        var allReservations = await _reservationRepository.GetClientsReservations(idClient);
        if (allReservations == null)
        {
            throw new Exception("No reservations found");
        }

        return allReservations;
    }
}
