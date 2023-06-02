/*
 * These codes are licensed under CC0.
 * https://creativecommons.org/publicdomain/zero/1.0/deed
 */
namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// Interface for ViewService for screen transition from ViewModel that can be used when Application.Current.MainWindow is NavigationWindow
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// A method for screen transitions that allows specifying the destination URI using the project root folder as the base URI
    /// </summary>
    /// <param name="uriStr">Relative URI to the destination page</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    bool NavigateRootBase(string uriStr, object? extraData = null);

    /// <summary>
    /// Methods to perform screen transitions by specifying the destination URI
    /// </summary>
    /// <param name="uri">URI to the destination page</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    bool Navigate(Uri uri, object? extraData = null);

    /// <summary>
    /// Method to advance the displayed page based on history
    /// </summary>
    void GoForward();

    /// <summary>
    /// Method to return the page displayed based on the history
    /// </summary>
    void GoBack();

    /// <summary>
    /// Methods for reloading the displayed page
    /// </summary>
    void Refresh();

    /// <summary>
    /// Method to delete the previously viewed page from the history
    /// </summary>
    void RemoveBackEntry();

    /// <summary>
    /// Methods for interrupting screen transitions
    /// </summary>
    void StopLoading();

    /// <summary>
    /// Method to delete all the history of the last viewed page from the history
    /// </summary>
    void RemoveAllBackEntry();

    /// <summary>
    /// Property indicating whether the GoBack method can be executed in the Frame or NavigationWindow to which the screen transition is targeted.
    /// </summary>
    bool CanGoBack { get; }

    /// <summary>
    /// Property indicating whether the GoForward method can be executed in the Frame or NavigationWindow to which the screen transition is targeted.
    /// </summary>
    bool CanGoForward { get; }
}