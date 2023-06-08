namespace _02_move_control;

using EnkuToolkit.Wpf.Controls;
using System.Windows;

public partial class MainWindow : CustomizableTitlebarWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ShutDownButtonClicked(object sender, RoutedEventArgs e)
    {
        App.Current.Shutdown();
    }

    private void MinimizeButtonClicked(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void NormalMaximizeButtonClicked(object sender, RoutedEventArgs e)
    {
        var nextState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        this.WindowState = nextState;
    }
}
