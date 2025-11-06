namespace windows_pos_system.Models
{
    public class SaleItem
    {
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        public decimal TotalPrice 
        { 
            get 
            { 
                return Product.Price * Quantity; 
            } 
        }
    }
}