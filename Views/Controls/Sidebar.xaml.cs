using System.Windows;
using System.Windows.Controls;

namespace windows_pos_system.Views.Controls;

public partial class Sidebar : UserControl
{
    public event EventHandler? ThemeToggleRequested;
    
    public Sidebar()
    {
        InitializeComponent();
    }

    private void ThemeToggle_Click(object sender, RoutedEventArgs e)
    {
        ThemeToggleRequested?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateThemeIcon(bool isDarkTheme)
    {
        ThemeIcon.Text = isDarkTheme ? "🌙" : "☀️";
    }
}
