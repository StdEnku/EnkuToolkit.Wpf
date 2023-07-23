namespace Sandbox.Views;

using EnkuToolkit.UiIndependent.Attributes;
using EnkuToolkit.Wpf.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

[DiRegister(DiRegisterMode.Singleton)]
public partial class MainWindow : CustomizableTitlebarWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnStateChanged(object sender, EventArgs e)
        => settingNormalizeOrMaximizeButtonIcon();

    private void OnLoaded(object sender, RoutedEventArgs e)
        => settingNormalizeOrMaximizeButtonIcon();

    private void MinimizeButtonClicked(object sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;

    private void normalizeOrMaximizeButtonClicked(object sender, RoutedEventArgs e)
        => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

    private void ShutdownButtonClicked(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    private void settingNormalizeOrMaximizeButtonIcon()
    {
        normalizeOrMaximizeButton.Content = WindowState == WindowState.Maximized ?
            new PackIcon() { Kind = PackIconKind.WindowRestore } :
            new PackIcon() { Kind = PackIconKind.WindowMaximize };
    }
}
