namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// Available when MainWindow of App class is NavigationWindow
/// Interface for ViewService to perform screen transition from ViewModel
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Methods for screen transitions that allow specifying transition destinations by relative URI based on the project root
    /// </summary>
    /// <param name="uriStr">Relative uri of the transition destination when based on the root of the project</param>
    /// <param name="extraData">Data to be passed to the destination page</param>
    /// <returns>true if navigation is not canceled otherwise false</returns>
    bool NavigateRootBase(string uriStr, object? extraData = null);

    /// <summary>
    /// Methods for screen transitions that allow specifying the transition destination by URI
    /// </summary>
    /// <param name="uri">Transition destination uri</param>
    /// <param name="extraData">Data to be passed to the destination page</param>
    /// <returns>true if navigation is not canceled otherwise false</returns>
    bool Navigate(Uri uri, object? extraData = null);

    /// <summary>
    /// Methods to advance the page
    /// </summary>
    void GoForward();

    /// <summary>
    /// Methods to return pages
    /// </summary>
    void GoBack();

    /// <summary>
    /// Methods for reloading pages
    /// </summary>
    void Refresh();

    /// <summary>
    /// Method to remove the latest history item from the history
    /// </summary>
    void RemoveBackEntry();

    /// <summary>
    /// Method to stop downloading content corresponding to the current navigation request
    /// </summary>
    void StopLoading();
}