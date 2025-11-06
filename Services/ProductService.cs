using windows_pos_system.Data;
using windows_pos_system.Models;

namespace windows_pos_system.Services
{
    public class ProductService
    {
        public List<Product> GetAllProducts()
        {
            using var context = new AppDbContext();
            return [.. context.Products];
        }

        public void AddProduct(Product product)
        {
            using var context = new AppDbContext();
            context.Products.Add(product);
            context.SaveChanges();
        }

        public Product? GetProductByBarcode(string barcode)
        {
            using var context = new AppDbContext();
            return context.Products.FirstOrDefault(p => p.Barcode == barcode);
        }

        public void UpdateProduct(Product product)
        {
            using var context = new AppDbContext();
            context.Products.Update(product);
            context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            using var context = new AppDbContext();
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            using var context = new AppDbContext();
            return context.Products
                .Where(p => p.Name.Contains(searchTerm) || 
                           p.Barcode.Contains(searchTerm))
                .ToList();
        }
    }
}