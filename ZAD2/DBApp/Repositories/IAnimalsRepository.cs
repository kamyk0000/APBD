using GakkoHorizontalSlice.Model;

namespace GakkoHorizontalSlice.Repositories;

public interface IAnimalsRepository
{
    Animal GetAnimal(int idAnimal);
    IEnumerable<Animal> GetAnimals();
    IEnumerable<Animal> GetAnimals(string orderBy);
    int CreateAnimal(string animalJSON2);
    public int UpdateAnimal(string animalJSON2);
    public int DeleteAnimal(int idAnimal);

}