using DBApp.Exceptions;
using DBApp.Services;

namespace DBApp.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try
        {
            var result = await _clientService.DeleteClientAsync(idClient);
            return Ok(result);
        }
        catch (ClientException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting client: {ex.Message}");
            return StatusCode(500, $"An error occurred while deleting the client: {ex.Message}");
        }
    }
}
