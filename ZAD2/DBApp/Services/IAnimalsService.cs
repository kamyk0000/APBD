using GakkoHorizontalSlice.Model;

namespace GakkoHorizontalSlice.Services;

public interface IAnimalsService
{
    Animal GetAnimal(int idAnimal);
    IEnumerable<Animal> GetAnimals();
    IEnumerable<Animal> GetAnimals(string orderBy);
    int CreateAnimal(string animalJSON2);
    public int UpdateAnimal(string animalJSON2);
    public int DeleteAnimal(int idAnimal);
    
}