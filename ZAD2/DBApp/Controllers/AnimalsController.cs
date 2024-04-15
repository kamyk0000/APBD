using GakkoHorizontalSlice.Model;
using GakkoHorizontalSlice.Services;
using Microsoft.AspNetCore.Mvc;

namespace GakkoHorizontalSlice.Controllers;

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
    [HttpGet("{orderBy:string}")]
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
    [HttpPost("{animalJSON2:string}")]
    public IActionResult CreateAnimal(string animalJSON2)
    {
        var affectedCount = _animalsService.CreateAnimal(animalJSON2);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    /// <summary>
    /// Endpoint used to update a animal from JSON2 formatted text.
    /// </summary>
    /// <param name="animalJSON2">JSON2 formatted text with Animal contents</param>
    /// <returns></returns>
    [HttpPut("{animalJSON2:string}")]
    public IActionResult UpdateAnimal(string animalJSON2)
    {
        var affectedCount = _animalsService.UpdateAnimal(animalJSON2);
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