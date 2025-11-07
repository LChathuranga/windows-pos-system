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

        // Add global exception handlers
        AppDomain.CurrentDomain.UnhandledException += (s, args) =>
        {
            var ex = args.ExceptionObject as Exception;
            System.Windows.MessageBox.Show($"Unhandled Exception: {ex?.Message}\n\n{ex?.StackTrace}", 
                "Fatal Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        };

        this.DispatcherUnhandledException += (s, args) =>
        {
            System.Windows.MessageBox.Show($"Dispatcher Exception: {args.Exception.Message}\n\n{args.Exception.StackTrace}", 
                "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            args.Handled = true;
        };

        try
        {
            // Apply migrations and create database on first run
            using var context = new AppDbContext();
            context.Database.EnsureCreated(); // Use EnsureCreated instead of Migrate

            // Add sample data
            windows_pos_system.Services.SeedData.Initialize();

            // Create and show main window
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Startup Error: {ex.Message}\n\n{ex.InnerException?.Message}\n\n{ex.StackTrace}", 
                "Startup Error", 
                System.Windows.MessageBoxButton.OK, 
                System.Windows.MessageBoxImage.Error);
            this.Shutdown();
        }
    }
}

