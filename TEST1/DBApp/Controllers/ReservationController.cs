using DBApp.Services;

namespace DBApp.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReservationController: ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("{idClient}")]
    public async Task<IActionResult> GetClientsReservations(int idClient)
    {
        try
        {
            var result = await _reservationService.GetClientsReservations(idClient);
            return Ok(result);
        }
        /*
        catch (ClientException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        */
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting client: {ex}");
            return StatusCode(500, $"An error occurred while deleting the client: {ex.Message}");
        }
    }
}