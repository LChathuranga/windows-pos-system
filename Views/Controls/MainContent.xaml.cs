using System.Windows;
using System.Windows.Controls;

namespace windows_pos_system.Views.Controls;

public partial class MainContent : UserControl
{
    public event EventHandler<string>? CategoryChanged;
    public event EventHandler<string>? SearchTextChanged;
    
    public MainContent()
    {
        InitializeComponent();
    }

    private void Category_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            string category = button.Content?.ToString() ?? "All";
            CategoryChanged?.Invoke(this, category);
        }
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox searchBox)
        {
            string searchText = searchBox.Text;
            if (!string.IsNullOrEmpty(searchText) && searchText != "Search for food, coffe, etc..")
            {
                SearchTextChanged?.Invoke(this, searchText);
            }
            else
            {
                SearchTextChanged?.Invoke(this, string.Empty);
            }
        }
    }
}
