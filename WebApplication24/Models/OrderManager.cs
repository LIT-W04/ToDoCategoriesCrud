using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication24.Models
{
    public class OrderManager
    {
        private string _connectionString;

        public OrderManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> GetOrders(DateTime from, DateTime to)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Orders WHERE " +
                                  "OrderDate BETWEEN @from AND @to";
            command.Parameters.AddWithValue("@from", from);
            command.Parameters.AddWithValue("@to", to);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                Order order = new Order
                {
                    CustomerId = (string) reader["CustomerId"],
                    Freight = (decimal) reader["Freight"],
                    OrderDate = (DateTime) reader["OrderDate"],
                    ShipName = (string) reader["ShipName"]
                };
                orders.Add(order);
            }

            return orders;
        }
    }
}