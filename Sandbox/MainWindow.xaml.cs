namespace Sandbox;

using EnkuToolkit.Wpf.Controls;
using System.Windows;

public partial class MainWindow : CustomTitlebarAnimatedNavigationWindow
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

    private void GoBackButtonClicked(object sender, RoutedEventArgs e)
    {
        this.GoBack();
    }

    private void GoForwardButtonClicked(object sender, RoutedEventArgs e)
    {
        this.GoForward();
    }
}
