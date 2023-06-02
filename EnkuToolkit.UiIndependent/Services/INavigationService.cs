/*
 * Copyright (c) 2022 StdEnku
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
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