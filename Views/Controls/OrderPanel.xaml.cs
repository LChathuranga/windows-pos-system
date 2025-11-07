using System.Windows;
using System.Windows.Controls;
using windows_pos_system.Models;
using windows_pos_system.ViewModels;

namespace windows_pos_system.Views.Controls;

public partial class OrderPanel : UserControl
{
    public event EventHandler? CompleteSaleRequested;
    
    public OrderPanel()
    {
        InitializeComponent();
    }

    private void CompleteSale_Click(object sender, RoutedEventArgs e)
    {
        CompleteSaleRequested?.Invoke(this, EventArgs.Empty);
    }

    private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is SaleItem item && DataContext is MainViewModel viewModel)
        {
            item.Quantity++;
            viewModel.RecalculateTotal();
        }
    }

    private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is SaleItem item && item.Quantity > 1 && DataContext is MainViewModel viewModel)
        {
            item.Quantity--;
            viewModel.RecalculateTotal();
        }
    }

    private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is SaleItem item && DataContext is MainViewModel viewModel)
        {
            viewModel.RemoveFromCart(item);
        }
    }
}
