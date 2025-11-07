using System.Collections.Generic;
using System.Linq;
using windows_pos_system.Data;
using windows_pos_system.Models;

namespace windows_pos_system.Services
{
    public static class CategoryService
    {
        public static List<Category> GetAllCategories()
        {
            using var context = new AppDbContext();
            return context.Categories.ToList();
        }
    }
}
