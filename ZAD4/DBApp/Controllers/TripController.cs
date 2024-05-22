using DBApp.Requests;
using DBApp.Services;

namespace DBApp.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet("GetAllTrips")]
    public async Task<IActionResult> GetAllTrips()
    {
        try
        {
            var trips = await _tripService.GetTripsAsync();
            return Ok(trips);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while fetching trips: {ex.Message}");
            return StatusCode(500, "An error occurred while fetching the trips");
        }
    }
    
    [HttpPost("{idTrip}/Client")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] ClientTripRequest request)
    {
        try
        {
            request.IdTrip = idTrip;
            await _tripService.AssignClientToTripAsync(request);
            return Ok("Client assigned to trip successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}