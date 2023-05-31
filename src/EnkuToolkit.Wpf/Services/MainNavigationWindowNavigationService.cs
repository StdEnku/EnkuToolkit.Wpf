namespace EnkuToolkit.Wpf.Services;

using System;
using System.Windows;
using System.Windows.Navigation;

/// <summary>
/// NavigationService available only when Application.Current.MainWindow is NavigationWindow
/// </summary>
public class MainNavigationWindowNavigationService : AbstractNavigationService
{
    /// <summary>
    /// Property that casts Application.Current.MainWindow to NavigationWindow and returns its NavigationServcie property
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when Application.Current.MainWindow cannot be cast to NavigationWindow.</exception>
    protected override NavigationService TargetNavigationService
    {
        get
        {
            var nw = Application.Current.MainWindow as NavigationWindow;
            if (nw is null) throw new InvalidCastException("Application.Current.MainWindow cannot be cast to NavigationWindow.");
            return nw.NavigationService;
        }
    }
}