using ElectronicsStore.Models;
using System.Collections.Generic;
using System.Data.SQLite; 

namespace ElectronicsStore.Data
{
    public class Database
    {
        private const string ConnectionString = "Data Source=Electro.db;Version=3;";

        public static List<Product> GetProducts()
        {
            var products = new List<Product>();

            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("SELECT Id, Name, Supplier, Price, ReleaseDate, Weight, ImagePath FROM Products", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Supplier = reader.GetString(2), // Убедитесь, что это поле существует в базе
                                Price = reader.GetDecimal(3),
                                ReleaseDate = reader.GetString(4),
                                Weight = reader.GetDecimal(5),
                                ImagePath = reader.GetString(6)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений (логирование или вывод сообщения)
                Console.WriteLine($"Ошибка при получении продуктов: {ex.Message}");
            }

            return products;
        }

        public static void InsertOrder(int productId, int quantity)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("INSERT INTO Orders (ProductId, Quantity) VALUES (@productId, @quantity)", connection);
                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine($"Ошибка при вставке заказа: {ex.Message}");
            }
        }
    }
}