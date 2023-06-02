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
namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows.Navigation;
using System;

/// <summary>
/// When customizing a ViewService abstract class inherited from ViewModel to perform screen transitions, override the TargetNavigationService property getter. When customizing a ViewService abstract class inherited from ViewModel to perform screen transitions, override the TargetNavigationService property getter and write a process that returns the NavigationService property of the target Frame or NavigationWindow for screen transitions.
/// </summary>
public abstract class AbstractNavigationService : INavigationService
{
    /// <summary>
    /// Property that describes the process of returning the NavigationService property of the target Frame or NavigationWindow for screen transitions.
    /// </summary>
    protected abstract NavigationService TargetNavigationService { get; }

    /// <summary>
    /// A method for screen transitions that allows specifying the destination URI using the project root folder as the base URI
    /// </summary>
    /// <param name="uriStr">Relative URI to the destination page</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    public bool NavigateRootBase(string uriStr, object? extraData = null)
    {
        var baseUri = new Uri("pack://application:,,,/");
        var uri = new Uri(baseUri, uriStr);

        if (extraData is null)
        {
            return TargetNavigationService.Navigate(uri);
        }
        else
        {
            return TargetNavigationService.Navigate(uri, extraData);
        }
    }

    /// <summary>
    /// Methods to perform screen transitions by specifying the destination URI
    /// </summary>
    /// <param name="uri">URI to the destination page</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    public bool Navigate(Uri uri, object? extraData = null)
    {
        if (extraData is null)
        {
            return TargetNavigationService.Navigate(uri);
        }
        else
        {
            return TargetNavigationService.Navigate(uri, extraData);
        }
    }

    /// <summary>
    /// Method to advance the displayed page based on history
    /// </summary>
    public void GoForward()
    {
        TargetNavigationService.GoForward();
    }

    /// <summary>
    /// Method to return the page displayed based on the history
    /// </summary>
    public void GoBack()
    {
        TargetNavigationService.GoBack();
    }

    /// <summary>
    /// Methods for reloading the displayed page
    /// </summary>
    public void Refresh()
    {
        TargetNavigationService.Refresh();
    }

    /// <summary>
    /// Method to delete the previously viewed page from the history
    /// </summary>
    public void RemoveBackEntry()
    {
        TargetNavigationService.RemoveBackEntry();
    }

    /// <summary>
    /// Methods for interrupting screen transitions
    /// </summary>
    public void StopLoading()
    {
        TargetNavigationService.StopLoading();
    }

    /// <summary>
    /// Method to delete all the history of the last viewed page from the history
    /// </summary>
    public void RemoveAllBackEntry()
    {
        var ns = TargetNavigationService;
        while (ns.CanGoBack)
        {
            ns.RemoveBackEntry();
        }
    }

    /// <summary>
    /// Property indicating whether the GoBack method can be executed in the Frame or NavigationWindow to which the screen transition is targeted.
    /// </summary>
    public bool CanGoBack => TargetNavigationService.CanGoBack;

    /// <summary>
    /// Property indicating whether the GoForward method can be executed in the Frame or NavigationWindow to which the screen transition is targeted.
    /// </summary>
    public bool CanGoForward => TargetNavigationService.CanGoForward;
}