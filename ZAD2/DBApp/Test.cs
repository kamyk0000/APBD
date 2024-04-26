using System.Data.SqlClient;

namespace DBApp;

public class Test

{
    public static void Main2()
    {
        string connectionString = "Server=db-mssql.pjwstk.edu.pl;Database=2019SBD;Integrated Security=True;";
        string query = "SELECT * FROM s24651.Animal WHERE IdAnimal = @categoryID";
        using (SqlConnection connection = new
                   SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query,
                       connection))
            {
                command.Parameters.AddWithValue("@categoryID", 1);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string productName =
                            reader["Name"].ToString();
                        
                    }
                }
            }
        }
    }


}