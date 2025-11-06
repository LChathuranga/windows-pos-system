using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Controls;
using windows_pos_system.ViewModels;
using windows_pos_system.Models;
using System;

namespace windows_pos_system;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    private bool _isDarkTheme = true;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        DataContext = _viewModel;
        
        // Wire up UserControl events
        SidebarControl.ThemeToggleRequested += (s, e) => ThemeToggle_Click(s ?? this, new RoutedEventArgs());
        MainContentControl.CategoryChanged += (s, category) => _viewModel.FilterByCategory(category);
        MainContentControl.SearchTextChanged += (s, searchText) => 
        {
            if (_viewModel != null)
                _viewModel.SearchText = searchText;
        };
        OrderPanelControl.CompleteSaleRequested += (s, e) => CompleteSale_Click(s ?? this, new RoutedEventArgs());
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void ProductCard_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is Border border && border.DataContext is Product product)
        {
            _viewModel.AddToCart(product);
        }
    }

    private void Category_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string category = button.Tag?.ToString() ?? "All";
            _viewModel.FilterByCategory(category);
            
            // Update button styles (simple approach)
            // In production, use proper MVVM with commands
        }
    }

    private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is SaleItem item)
        {
            _viewModel.RemoveFromCart(item);
        }
    }

    private void CompleteSale_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.CartItems.Count == 0)
        {
            MessageBox.Show("Cart is empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var result = MessageBox.Show($"Complete sale for {_viewModel.TotalAmount:C}?", 
            "Confirm Sale", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            MessageBox.Show("Sale completed successfully!", "Success", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            _viewModel.CartItems.Clear();
        }
    }

    private void ThemeToggle_Click(object sender, RoutedEventArgs e)
    {
        _isDarkTheme = !_isDarkTheme;
        
        // Clear existing theme
        Application.Current.Resources.MergedDictionaries.Clear();
        
        // Load new theme
        var themeUri = new Uri(_isDarkTheme 
            ? "Themes/DarkTheme.xaml" 
            : "Themes/LightTheme.xaml", UriKind.Relative);
        
        var themeDictionary = new ResourceDictionary { Source = themeUri };
        Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
        
        // Update sidebar icon
        SidebarControl.UpdateThemeIcon(_isDarkTheme);
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // This method is no longer needed as it's handled in MainContent UserControl
    }

    private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
    {
        // quick fade-out animation before minimizing
        var fade = new DoubleAnimation(0.0, TimeSpan.FromMilliseconds(120));
        fade.Completed += (s, _) => WindowState = WindowState.Minimized;
        this.BeginAnimation(Window.OpacityProperty, fade);
    }

    private void MaximizeWindow_Click(object sender, RoutedEventArgs e)
    {
        // Toggle maximize but constrain to the working area so taskbar remains visible
        if (WindowState == WindowState.Maximized)
        {
            // restore
            WindowState = WindowState.Normal;
            // animate restore scale (subtle)
            var anim = new DoubleAnimation(1.02, 1.0, TimeSpan.FromMilliseconds(180)) { EasingFunction = new QuadraticEase() };
            this.RenderTransform = new System.Windows.Media.ScaleTransform(1.0, 1.0);
            this.RenderTransform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, anim);
            this.RenderTransform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, anim);
        }
        else
        {
            // maximize to work area (keeps taskbar visible)
            var workArea = SystemParameters.WorkArea;
            // apply size and location
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = workArea.Left;
            this.Top = workArea.Top;
            this.Width = workArea.Width;
            this.Height = workArea.Height;
            WindowState = WindowState.Normal; // ensure non-max state since we manually set size
            // subtle scale animation in
            var animIn = new DoubleAnimation(0.98, 1.0, TimeSpan.FromMilliseconds(160)) { EasingFunction = new QuadraticEase() };
            this.RenderTransform = new System.Windows.Media.ScaleTransform(1.0, 1.0);
            this.RenderTransform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, animIn);
            this.RenderTransform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, animIn);
        }
    }

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    // Ensure hover behavior works even if template triggers fail
    private void CloseButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (sender is Control c)
        {
            c.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#E81123"));
            c.Foreground = System.Windows.Media.Brushes.White;
        }
    }

    private void CloseButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (sender is Control c)
        {
            c.Background = System.Windows.Media.Brushes.Transparent;
            // Reset to resource color if available
            var brush = Application.Current.Resources["TextSecondary"] as System.Windows.Media.Brush;
            c.Foreground = brush ?? System.Windows.Media.Brushes.Gray;
        }
    }
}