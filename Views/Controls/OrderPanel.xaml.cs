using System.Windows;
using System.Windows.Controls;

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
}
