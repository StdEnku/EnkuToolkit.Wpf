namespace Sandbox.Views;

using System.Windows;
using EnkuToolkit.Wpf.Controls;

/// <summary>
/// MainWindow.xaml の相互作用ロジック
/// </summary>
public partial class MainWindow : CustomizableTitlebarWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MinimizeButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeOrNormalizeButtonClicked(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void ShutDownButtonClicked(object sender, RoutedEventArgs e)
    {
        App.Current.Shutdown();
    }
}
