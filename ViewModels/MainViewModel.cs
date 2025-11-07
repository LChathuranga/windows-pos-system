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
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                LoadProducts();
            }
        }

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
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
            Categories = new ObservableCollection<Category>();
            CartItems = new ObservableCollection<SaleItem>();
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            Categories.Clear();
            // Add an 'All' pseudo-category
            Categories.Add(new Category { Name = "All", Description = "All items" });
            var cats = CategoryService.GetAllCategories();
            foreach (var c in cats)
            {
                Categories.Add(c);
            }
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

        public void FilterByCategory(Category category)
        {
            _selectedCategory = category?.Name ?? "All";
            LoadProducts();
        }

        public void AddToCart(Product product)
        {
            try
            {
                if (product == null)
                {
                    System.Windows.MessageBox.Show("Product is null", "Error");
                    return;
                }

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
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding to cart: {ex.Message}\n\n{ex.StackTrace}", 
                    "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
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

        public void RecalculateTotal()
        {
            CalculateTotal();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}