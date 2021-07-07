using AnimalsWebApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AnimalsWebApp.Service
{
    public class SQLDatabaseService : IDatabaseService
    {
        private IConfiguration _configuration; 

        public SQLDatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddAnimal(Animal animal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "INSERT INTO animal VALUES(@name, @description, @category, @area)";
                com.Parameters.AddWithValue("@name", animal.Name);
                com.Parameters.AddWithValue("@description", animal.Description);
                com.Parameters.AddWithValue("@category", animal.Category);
                com.Parameters.AddWithValue("@area", animal.Area);
                await con.OpenAsync();
                await com.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> CheckIfAnimalExists(int idAnimal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT COUNT(1) FROM Animal WHERE IdAnimal = @idAnimal";
                com.Parameters.AddWithValue("@idAnimal", idAnimal);
                await con.OpenAsync();

                int count = Convert.ToInt32(await com.ExecuteScalarAsync());

                return count == 1;
            }
        }

        public async Task DeleteAnimal(int idAnimal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM animal WHERE idAnimal = @idAnimal";
                com.Parameters.AddWithValue("@idAnimal", idAnimal);
                await con.OpenAsync();
                await com.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Animal>> GetAnimals(string OrderBy)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Animal ORDER BY " + OrderBy;
                await con.OpenAsync();

                SqlDataReader dr = com.ExecuteReader();

                var list = new List<Animal>();

                while (await dr.ReadAsync())
                {
                    list.Add(new Animal
                    {
                        IdAnimal = (int)dr["IdAnimal"],
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    });
                }

                return list;
            }
        }

        public async Task UpdateAnimal(Animal animal, int idAnimal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE animal SET Name = @name, Description = @description, Category = @category, " +
                                      "Area = @area WHERE IdAnimal = @idAnimal";
                com.Parameters.AddWithValue("@idAnimal", idAnimal);
                com.Parameters.AddWithValue("@name", animal.Name);
                com.Parameters.AddWithValue("@description", animal.Description);
                com.Parameters.AddWithValue("@category", animal.Category);
                com.Parameters.AddWithValue("@area", animal.Area);
                await con.OpenAsync();
                await com.ExecuteNonQueryAsync();
            }
        }
    }
}
