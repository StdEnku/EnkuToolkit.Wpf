namespace Sandbox;

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
        WindowState = WindowState.Minimized;
    }

    private void NormalMaximizeButtonClicked(object sender, RoutedEventArgs e)
    {
        var nextState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        WindowState = nextState;
    }

    private void GoBackButtonClicked(object sender, RoutedEventArgs e)
    {
        mainFrame.GoBack();
    }

    private void GoForwardButtonClicked(object sender, RoutedEventArgs e)
    {
        mainFrame.GoForward();
    }

    private void RefleshButtonClicked(object sender, RoutedEventArgs e)
    {
        mainFrame.Refresh();
    }
}
