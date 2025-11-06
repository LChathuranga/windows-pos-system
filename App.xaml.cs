using System.Windows;
using Microsoft.EntityFrameworkCore;
using windows_pos_system.Data;

namespace windows_pos_system;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            // Apply migrations and create database on first run
            using var context = new AppDbContext();
            context.Database.EnsureCreated(); // Use EnsureCreated instead of Migrate

            // Add sample data
            windows_pos_system.Services.SeedData.Initialize();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Database Error: {ex.Message}\n\n{ex.StackTrace}", 
                "Startup Error", 
                System.Windows.MessageBoxButton.OK, 
                System.Windows.MessageBoxImage.Error);
        }
    }
}

