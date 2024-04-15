using System.Data.SqlClient;
using GakkoHorizontalSlice.Model;

namespace GakkoHorizontalSlice.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;
    
    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Animal GetAnimal(int idAnimal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var animal = new Animal()
        {
            IdAnimal = (int)dr["IdAnimal"],
            Name = dr["Name"].ToString(),
            Description = dr["Description"].ToString(),
            Category = dr["Category"].ToString(),
            Area = dr["Area"].ToString()
        };
        
        return animal;
    }

    public IEnumerable<Animal> GetAnimals() { return GetAnimals("DEFAULT"); }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        switch (orderBy?.ToLower())
        {
            case "description":
                orderBy = "Description";
                break;
            case "category":
                orderBy = "Category";
                break;
            case "area":
                orderBy = "Area";
                break;
            default:
                orderBy = "Name";
                break;
        }

        var animals = new List<Animal>();

        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY @OrderBy";
        cmd.Parameters.AddWithValue("@IdAnimal", orderBy);

        try
        {
            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var animal = new Animal
                {
                    IdAnimal = dr.GetInt32(dr.GetOrdinal("IdAnimal")),
                    Name = dr.GetString(dr.GetOrdinal("Name")),
                    Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? "null" : dr.GetString(dr.GetOrdinal("Description")),
                    Category = dr.GetString(dr.GetOrdinal("Category")),
                    Area = dr.GetString(dr.GetOrdinal("Area"))
                };
                animals.Add(animal);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("[ERROR] An error occurred while fetching animals from the database.", ex);
        }

        return animals;
    }
    //Will code these later!!
    public int CreateAnimal(string animalJSON2)
    {
        throw new NotImplementedException();
    }

    public int UpdateAnimal(string animalJSON2)
    {
        throw new NotImplementedException();
    }
    
    public int DeleteAnimal(int idAnimal)
    {
        throw new NotImplementedException();
    }
}