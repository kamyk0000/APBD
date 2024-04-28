
using DBApp.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace DBApp.Repositories;

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
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM s24651.Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var animal = new Animal()
        {
            IdAnimal = dr.GetInt32(dr.GetOrdinal("IdAnimal")),
            Name = dr.GetString(dr.GetOrdinal("Name")),
            Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? "null" : dr.GetString(dr.GetOrdinal("Description")),
            Category = dr.GetString(dr.GetOrdinal("Category")),
            Area = dr.GetString(dr.GetOrdinal("Area"))
        };
        
        return animal;
    }

    public IEnumerable<Animal> GetAnimals() { return GetAnimals("DEFAULT"); }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        string column;
        
        switch (orderBy?.ToLower())
        {
            case "description":
                column = "Description";
                break;
            case "category":
                column = "Category";
                break;
            case "area":
                column = "Area";
                break;
            default:
                column = "Name";
                break;
        }

        var animals = new List<Animal>();

        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY " + column;
        var dr = cmd.ExecuteReader();
        
            while (dr.Read())
            {
                var animal = new Animal()
                {
                    IdAnimal = dr.GetInt32(dr.GetOrdinal("IdAnimal")),
                    Name = dr.GetString(dr.GetOrdinal("Name")),
                    Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? "null" : dr.GetString(dr.GetOrdinal("Description")),
                    Category = dr.GetString(dr.GetOrdinal("Category")),
                    Area = dr.GetString(dr.GetOrdinal("Area"))
                };
                animals.Add(animal);
            }
        

        return animals;
    }
    
    public int CreateAnimal(Animal? animal)
    {
        
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES(@Name, @Description, @Category, @Area)";
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(JObject animalJSON)
    {
        Animal? animal = animalJSON.ToObject<Animal>();
        
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
    
    public int DeleteAnimal(int idAnimal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
}