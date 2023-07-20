/*
 * MIT License
 * 
 * Copyright (c) 2023 StdEnku
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// Interface for ViewService for screen transition from ViewModel that can be used when Application.Current.MainWindow is NavigationWindow
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Methods to perform screen transitions to pages specified by type name including namespace
    /// </summary>
    /// <param name="nextPageFullName">The page to which the screen transition is to be made, specified by a type name that includes a namespace</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    bool Navigate(string nextPageFullName, object? extraData = null);

    /// <summary>
    /// A method that transitions the screen to the page specified by the type name including the namespace; unlike the Navigate method, the object of the destination page is generated from the DI container
    /// </summary>
    /// <param name="nextPageFullName">The page to which the screen transition is to be made, specified by a type name that includes a namespace</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    bool NavigateDi(string nextPageFullName, object? extraData = null);

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