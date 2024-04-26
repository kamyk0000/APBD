using DBApp.Models;
using Newtonsoft.Json.Linq;

namespace DBApp.Services;

public interface IAnimalsService
{
    Animal GetAnimal(int idAnimal);
    IEnumerable<Animal> GetAnimals();
    IEnumerable<Animal> GetAnimals(string orderBy);
    public int CreateAnimal(Animal? animalJSON2);
    public int UpdateAnimal(JObject animalJSON);
    public int DeleteAnimal(int idAnimal);
    
}