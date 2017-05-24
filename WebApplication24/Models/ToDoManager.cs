using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication24.Models
{
    public class ToDoManager
    {
        private string _connectionString;

        public ToDoManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ToDoCategory> GetCategories()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Categories";
            connection.Open();
            List<ToDoCategory> categories = new List<ToDoCategory>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ToDoCategory c = new ToDoCategory
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"]
                };
                categories.Add(c);
            }

            return categories;
        }

        public void AddCategory(ToDoCategory category)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Categories VALUES (@name); SELECT @@Identity";
            command.Parameters.AddWithValue("@name", category.Name);
            connection.Open();
            category.Id = (int)(decimal)command.ExecuteScalar();
        }

        public ToDoCategory GetById(int id)
        {
            return GetCategories().FirstOrDefault(c => c.Id == id);
        }

        public void UpdateCategory(ToDoCategory category)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Categories SET Name = @name WHERE Id = @id";
            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@id", category.Id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}