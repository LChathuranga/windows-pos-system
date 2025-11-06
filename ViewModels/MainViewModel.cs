using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using windows_pos_system.Models;
using windows_pos_system.Services;

namespace windows_pos_system.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;
        private string _searchText = string.Empty;
        private decimal _totalAmount;
        private string _selectedCategory = "All";

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<SaleItem> CartItems { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                LoadProducts();
            }
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }

        public string CurrentDate => DateTime.Now.ToString("dddd, d MMM yyyy");

        public MainViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();
            CartItems = new ObservableCollection<SaleItem>();
            LoadProducts();
        }

        private void LoadProducts()
        {
            Products.Clear();
            var products = string.IsNullOrWhiteSpace(SearchText) 
                ? _productService.GetAllProducts()
                : _productService.SearchProducts(SearchText);

            // Filter by category if not "All"
            if (_selectedCategory != "All")
            {
                products = products.Where(p => p.Category == _selectedCategory).ToList();
            }

            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        public void FilterByCategory(string category)
        {
            _selectedCategory = category;
            LoadProducts();
        }

        public void AddToCart(Product product)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.Product.Id == product.Id);
            
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new SaleItem { Product = product, Quantity = 1 });
            }

            CalculateTotal();
        }

        public void RemoveFromCart(SaleItem item)
        {
            CartItems.Remove(item);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalAmount = CartItems.Sum(item => item.TotalPrice);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}