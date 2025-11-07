using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace windows_pos_system.Models
{
    public class SaleItem : INotifyPropertyChanged
    {
        private int _quantity;
        
        public Product Product { get; set; } = new Product();
        
        public int Quantity 
        { 
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        
        public decimal TotalPrice 
        { 
            get 
            { 
                return Product.Price * Quantity; 
            } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}