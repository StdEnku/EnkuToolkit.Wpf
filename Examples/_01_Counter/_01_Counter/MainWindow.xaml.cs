namespace _01_Counter;

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

    private int _count;

    private void PlusButtonClicked(object sender, RoutedEventArgs e)
    {
        _count++;
        transitionEffect.Snapshot(); //Snapshot taken before screen update
        label.Content = _count;
        transitionEffect.RunForwardEffect(); //Effect execution after screen refresh
    }

    private void MinusButtonClicked(object sender, RoutedEventArgs e)
    {
        _count--;
        transitionEffect.Snapshot(); //Snapshot taken before screen update
        label.Content = _count;
        transitionEffect.RunBackwardEffect(); //Effect execution after screen refresh
    }

    private void ClearButtonClicked(object sender, RoutedEventArgs e)
    {
        _count = 0;
        transitionEffect.Snapshot(); //Snapshot taken before screen update
        label.Content = _count;
        transitionEffect.RunReloadEffect(); //Effect execution after screen refresh
    }
}
