using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows.Input;
using ElectronicsStore.Data;
using System.Data.SQLite;
namespace ElectronicsStore.ViewModels
{
    public class BuyerViewModel : INotifyPropertyChanged
    {
        private Product _selectedProduct;
        private int _selectedQuantity;

        public ObservableCollection<Product> Products { get; set; }
        public Cart Cart { get; set; }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public int SelectedQuantity
        {
            get => _selectedQuantity;
            set
            {
                _selectedQuantity = value;
                OnPropertyChanged(nameof(SelectedQuantity));
            }
        }

        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand UpdateQuantityCommand { get; }
        public string ConnectionString { get; private set; }
        public ICommand CompleteOrderCommand { get; }

        public BuyerViewModel()
        {
            Products = new ObservableCollection<Product>();
            Cart = new Cart();
            LoadProducts(); // Метод для загрузки продуктов из базы данных
            AddToCartCommand = new RelayCommand(AddToCart);
            RemoveFromCartCommand = new RelayCommand(RemoveFromCart);
            UpdateQuantityCommand = new RelayCommand(UpdateQuantity);
            CompleteOrderCommand = new RelayCommand(CompleteOrder);

        }

        private void AddToCart()
        {
            if (SelectedProduct != null && SelectedQuantity > 0)
            {
                Cart.AddItem(SelectedProduct, SelectedQuantity);
                SelectedQuantity = 0; // Сброс количества после добавления
                OnPropertyChanged(nameof(Cart));
                LoadProducts(); // Обновить список товаров после изменения в корзине
            }
        }

        private void RemoveFromCart()
        {
            if (SelectedProduct != null)
            {
                Cart.RemoveItem(SelectedProduct.Id);
                OnPropertyChanged(nameof(Cart));
            }
        }

        private void UpdateQuantity()
        {
            if (SelectedProduct != null && SelectedQuantity > 0)
            {
                Cart.UpdateItemQuantity(SelectedProduct.Id, SelectedQuantity);
                OnPropertyChanged(nameof(Cart));
            }
        }
       
        private void LoadProducts()
        {
            Products.Clear();
            var productsFromDb = Data.Database.GetProducts(); // Метод для получения продуктов из базы данных
            foreach (var product in productsFromDb)
            {
                Products.Add(product);
            }

        }
        // Метод для обновления количества товара в базе данных
        private void UpdateProductQuantity(Product product, int quantity)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("UPDATE Products SET Quantity = Quantity - @quantity WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@id", product.Id);
                command.ExecuteNonQuery();
            }
        }

        // Метод для создания заказа
        private void CreateOrder()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                foreach (var item in Cart.Items)
                {
                    var command = new SQLiteCommand("INSERT INTO Orders (ProductId, Quantity) VALUES (@productId, @quantity)", connection);
                    command.Parameters.AddWithValue("@productId", item.Product.Id);
                    command.Parameters.AddWithValue("@quantity", item.Quantity);
                    command.ExecuteNonQuery();

                    // Обновляем количество товара в базе данных
                    UpdateProductQuantity(item.Product, item.Quantity);
                }
            }
        }
        private void CompleteOrder()
        {
            foreach (var item in Cart.Items)
            {
                Data.Database.InsertOrder(item.ProductId, item.Quantity);
            }
            Cart.Clear();
            LoadProducts();
            OnPropertyChanged(nameof(Cart));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
