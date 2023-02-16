namespace EnkuToolkit.Wpf.Demo.Services;

using System;
using System.Windows.Navigation;

public class NavigationService : INavigationService
{
    private NavigationWindow MainNavigationWindow => (NavigationWindow)App.Current.MainWindow;

    public bool Navigate(string uriStr, object? extraData = null)
    {
        var baseUri = new Uri("pack://application:,,,/");
        var uri = new Uri(baseUri, uriStr);

        if (extraData is null)
        {
            return this.MainNavigationWindow.Navigate(uri);
        }
        else
        {
            return this.MainNavigationWindow.Navigate(uri, extraData);
        }
    }

    public void GoForward()
    {
        this.MainNavigationWindow.GoForward();
    }

    public void GoBack()
    {
        this.MainNavigationWindow.GoBack();
    }
}
