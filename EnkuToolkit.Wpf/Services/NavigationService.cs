namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows.Navigation;
using System;
using System.Windows;

/// <summary>
/// Available when the MainWindow of the App class is NavigationWindow
/// ViewService for executing screen transitions from ViewModel
/// </summary>
public class NavigationService : INavigationService
{
    private NavigationWindow MainNavigationWindow => (NavigationWindow)Application.Current.MainWindow;

    /// <summary>
    /// Methods for screen transitions that allow specifying transition destinations by relative URI based on the project root
    /// </summary>
    /// <param name="uriStr">Relative uri of the transition destination when based on the root of the project</param>
    /// <param name="extraData">Data to be passed to the destination page</param>
    /// <returns>true if navigation is not canceled otherwise false</returns>
    public bool NavigateRootBase(string uriStr, object? extraData = null)
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

    /// <summary>
    /// Methods for screen transitions that allow specifying the transition destination by URI
    /// </summary>
    /// <param name="uri">Transition destination uri</param>
    /// <param name="extraData">Data to be passed to the destination page</param>
    /// <returns>true if navigation is not canceled otherwise false</returns>
    public bool Navigate(Uri uri, object? extraData = null)
    {
        if (extraData is null)
        {
            return this.MainNavigationWindow.Navigate(uri);
        }
        else
        {
            return this.MainNavigationWindow.Navigate(uri, extraData);
        }
    }

    /// <summary>
    /// Methods to advance the page
    /// </summary>
    public void GoForward()
    {
        this.MainNavigationWindow.GoForward();
    }

    /// <summary>
    /// Methods to return pages
    /// </summary>
    public void GoBack()
    {
        this.MainNavigationWindow.GoBack();
    }

    /// <summary>
    /// Methods for reloading pages
    /// </summary>
    public void Refresh()
    {
        this.MainNavigationWindow.Refresh();
    }

    /// <summary>
    /// Method to remove the latest history item from the history
    /// </summary>
    public void RemoveBackEntry()
    {
        this.MainNavigationWindow.RemoveBackEntry();
    }

    /// <summary>
    /// Method to stop downloading content corresponding to the current navigation request
    /// </summary>
    public void StopLoading()
    {
        this.MainNavigationWindow.StopLoading();
    }
}