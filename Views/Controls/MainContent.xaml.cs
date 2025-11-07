using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using windows_pos_system.Models;

namespace windows_pos_system.Views.Controls;

public partial class MainContent : UserControl
{
    public event EventHandler<string>? CategoryChanged;
    public event EventHandler<string>? SearchTextChanged;
    public event EventHandler<Product>? ProductClicked;
    
    public MainContent()
    {
        InitializeComponent();
    }

    private void Category_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string category = button.Tag is windows_pos_system.Models.Category c ? c.Name : button.Content?.ToString() ?? "All";
            CategoryChanged?.Invoke(this, category);
        }
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox searchBox)
        {
            // Update placeholder visibility
            SearchPlaceholder.Visibility = string.IsNullOrEmpty(searchBox.Text) 
                ? Visibility.Visible 
                : Visibility.Collapsed;
            
            // Trigger search
            SearchTextChanged?.Invoke(this, searchBox.Text);
        }
    }

    private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
    {
        SearchPlaceholder.Visibility = Visibility.Collapsed;
    }

    private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox searchBox && string.IsNullOrEmpty(searchBox.Text))
        {
            SearchPlaceholder.Visibility = Visibility.Visible;
        }
    }

    private void ProductCard_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is Border border && border.Tag is Product product)
        {
            ProductClicked?.Invoke(this, product);
        }
    }
}
