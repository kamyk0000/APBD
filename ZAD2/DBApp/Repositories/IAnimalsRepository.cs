using DBApp.Models;
using Newtonsoft.Json.Linq;

namespace DBApp.Repositories;

public interface IAnimalsRepository
{
    Animal GetAnimal(int idAnimal);
    IEnumerable<Animal> GetAnimals();
    
    IEnumerable<Animal> GetAnimals(string orderBy);
    int CreateAnimal(Animal? animalJSON2);
    public int UpdateAnimal(JObject animalJSON);
    public int DeleteAnimal(int idAnimal);

}