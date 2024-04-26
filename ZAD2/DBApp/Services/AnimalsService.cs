using DBApp.Models;
using DBApp.Repositories;
using Newtonsoft.Json.Linq;

namespace DBApp.Services;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }
    
    public Animal GetAnimal(int idAnimal)
    {
        return _animalsRepository.GetAnimal(idAnimal);
    }

    public IEnumerable<Animal> GetAnimals()
    {
        return _animalsRepository.GetAnimals();
    }
    
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        return _animalsRepository.GetAnimals(orderBy);
    }
    
    public int CreateAnimal(Animal? animalJSON2)
    {
        return _animalsRepository.CreateAnimal(animalJSON2);
    }

    public int UpdateAnimal(JObject animalJSON)
    {
        return _animalsRepository.UpdateAnimal(animalJSON);
    }

    public int DeleteAnimal(int idAnimal)
    {
        return _animalsRepository.DeleteAnimal(idAnimal);
    }
}