using DBApp.Models;
using DBApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DBApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private IAnimalsService _animalsService;
    
    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }
    
    /// <summary>
    /// Endpoints used to return list of animals.
    /// </summary>
    /// <returns>List of animals</returns>
    [HttpGet]
    public IActionResult GetAnimals()
    {
        var animals = _animalsService.GetAnimals();
        return Ok(animals);
    }
    
    /// <summary>
    /// Endpoints used to return ordered list of animals.
    /// </summary>
    /// /// <param name="orderBy">Column name to order by</param>
    /// <returns>Ordered list of animals</returns>
    [HttpGet("{orderBy}")]
    public IActionResult GetAnimals(string orderBy)
    {
        var animals = _animalsService.GetAnimals(orderBy);
        return Ok(animals);
    }
    
    /// <summary>
    /// Endpoint used to return a single animal.
    /// </summary>
    /// <param name="id">Id of an animal</param>
    /// <returns>Animal</returns>
    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animalsService.GetAnimal(id);

        if (animal==null)
        {
            return NotFound("Student not found");
        }
        
        return Ok(animal);
    }
    
    /// <summary>
    /// Endpoint used to create a animal from JSON2 formatted text.
    /// </summary>
    /// <param name="animalJSON2">JSON2 formatted text with Animal contents</param>
    /// <returns>201 Created</returns>
    [HttpPost("{animalJSON2}")]
    public IActionResult CreateAnimal([FromBody] JObject animalJSON2)
    {
        Animal? animal = animalJSON2.ToObject<Animal>();
        var affectedCount = _animalsService.CreateAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    /// <summary>
    /// Endpoint used to update a animal from JSON2 formatted text.
    /// </summary>
    /// <param name="animalJSON2">JSON2 formatted text with Animal contents</param>
    /// <returns></returns>
    [HttpPut("{animalJSON}")]
    public IActionResult UpdateAnimal([FromBody]JObject animalJSON)
    {
        var affectedCount = _animalsService.UpdateAnimal(animalJSON);
        return NoContent();
    }
    
    /// <summary>
    /// Endpoint used to delete a animal.
    /// </summary>
    /// <param name="id">Id of an animal</param>
    /// <returns>204 No Content</returns>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteStudent(int id)
    {
        var affectedCount = _animalsService.DeleteAnimal(id);
        return NoContent();
    }
    
}