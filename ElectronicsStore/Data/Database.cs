using ElectronicsStore.Models;
using System.Collections.Generic;
using System.Data.SQLite; // или Microsoft.Data.Sqlite, если вы используете этот пакет

namespace ElectronicsStore.Data
{
    public class Database
    {
        private const string ConnectionString = "Data Source=Electro.db;Version=3;";

        public static List<Product> GetProducts()
        {
            var products = new List<Product>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Id, Name, Price FROM Products", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }
}