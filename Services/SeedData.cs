using System.Linq;
using windows_pos_system.Data;
using windows_pos_system.Models;

namespace windows_pos_system.Services
{
    public static class SeedData
    {
        public static void Initialize()
        {
            using var context = new AppDbContext();

            // Seed categories if none exist
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Fresh Juices", Description = "Freshly pressed fruit and vegetable juices" },
                    new Category { Name = "Smoothies", Description = "Creamy blended smoothies" },
                    new Category { Name = "Shots", Description = "Energy and wellness shots" },
                    new Category { Name = "Bowls", Description = "Smoothie and acai bowls" },
                    new Category { Name = "Add-ons", Description = "Boosters and add-ons (protein, chia, etc.)" },
                    new Category { Name = "Seasonal", Description = "Seasonal specials and limited editions" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Seed products if none exist
            if (!context.Products.Any())
            {
                var products = new[]
                {
                    new Product { Name = "Spicy seasoned seafood noodles", Price = 2.29m, Stock = 20, Category = "Hot", Barcode = "001" },
                    new Product { Name = "Salted Pasta with mushroom sauce", Price = 2.69m, Stock = 11, Category = "Hot", Barcode = "002" },
                    new Product { Name = "Beef dumpling in hot and sour soup", Price = 2.99m, Stock = 16, Category = "Soup", Barcode = "003" },
                    new Product { Name = "Healthy noodle with spinach leaf", Price = 3.29m, Stock = 22, Category = "Hot", Barcode = "004" },
                    new Product { Name = "Hot spicy fried rice with omelet", Price = 3.49m, Stock = 13, Category = "Hot", Barcode = "005" },
                    new Product { Name = "Spicy instant noodle with special omelette", Price = 3.59m, Stock = 17, Category = "Hot", Barcode = "006" },
                    new Product { Name = "Grilled Chicken Breast", Price = 8.99m, Stock = 15, Category = "Grill", Barcode = "007" },
                    new Product { Name = "Caesar Salad", Price = 4.99m, Stock = 25, Category = "Appetizer", Barcode = "008" },
                    new Product { Name = "Chocolate Cake", Price = 5.99m, Stock = 10, Category = "Dessert", Barcode = "009" },
                    new Product { Name = "Ice Cream Sundae", Price = 3.99m, Stock = 30, Category = "Dessert", Barcode = "010" }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}