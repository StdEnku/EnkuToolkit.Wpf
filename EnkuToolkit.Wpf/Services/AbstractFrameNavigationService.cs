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
namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.Wpf._internal;
using EnkuToolkit.Wpf.MarkupExtensions;
using System;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// ViewService for executing screen transitions from the ViewModel to the Frame on the Window
/// </summary>
public abstract class AbstractFrameNavigationService : INavigationService
{
    /// <summary>
    /// Property that returns the target Frame object for screen transition
    /// </summary>
    protected abstract Frame TargetFrame { get; }

    /// <summary>
    /// Methods to perform screen transitions to pages specified by type name including namespace
    /// </summary>
    /// <param name="nextPageFullName">The page to which the screen transition is to be made, specified by a type name that includes a namespace</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    /// <exception cref="ArgumentException">Thrown if the type of the name specified in nextPageFullName does not exist.</exception>
    public bool Navigate(string nextPageFullName, object? extraData = null)
    {
        var nextPageType = AssemblyUtils.SearchAllClientDefinedTypes(nextPageFullName);
        if (nextPageType is null) throw new ArgumentException("Could not find the type of the full name specified in the AbstractFrameNavigationService.NavigateFullName method.", nameof(nextPageFullName));
        var nextPageInstance = Activator.CreateInstance(nextPageType);
        return TargetFrame.Navigate(nextPageInstance, extraData);
    }

    /// <summary>
    /// A method that transitions the screen to the page specified by the type name including the namespace; unlike the Navigate method, the object of the destination page is generated from the DI container
    /// </summary>
    /// <param name="nextPageFullName">The page to which the screen transition is to be made, specified by a type name that includes a namespace</param>
    /// <param name="extraData">Data to be passed to the destination</param>
    /// <returns>Returns false if the screen transition is canceled, true if not canceled</returns>
    /// <exception cref="ArgumentException">Thrown if the type of the name specified in nextPageFullName does not exist.</exception>
    /// <exception cref="InvalidOperationException">
    /// Exception thrown when Application.Current cannot be cast to IServicesOwner
    /// or
    /// Exception thrown when the specified instance cannot be obtained from the DI container
    /// </exception>
    public bool NavigateDi(string nextPageFullName, object? extraData = null)
    {
        var nextPageType = AssemblyUtils.SearchAllClientDefinedTypes(nextPageFullName);
        if (nextPageType is null) 
            throw new ArgumentException("Could not find the type of the full name specified in the AbstractFrameNavigationService.NavigateFullName method.", nameof(nextPageFullName));

        var servicesOwner = Application.Current as IServicesOwner;
        if (servicesOwner is null)
            throw new InvalidOperationException("Cannot cast Application.Current to IServicesOwner.");

        var services = servicesOwner.Services;
        var nextPageInstance = services.GetService(nextPageType);
        if (nextPageInstance is null)
            throw new InvalidOperationException("Could not get the specified instance from the DI container.");

        return TargetFrame.Navigate(nextPageInstance, extraData);
    }

    /// <summary>
    /// Method to advance the displayed page based on history
    /// </summary>
    public void GoForward()
    {
        TargetFrame.GoForward();
    }

    /// <summary>
    /// Method to return the page displayed based on the history
    /// </summary>
    public void GoBack()
    {
        TargetFrame.GoBack();
    }

    /// <summary>
    /// Methods for reloading the displayed page
    /// </summary>
    public void Refresh()
    {
        TargetFrame.Refresh();
    }

    /// <summary>
    /// Method to delete the previously viewed page from the history
    /// </summary>
    public void RemoveBackEntry()
    {
        TargetFrame.RemoveBackEntry();
    }

    /// <summary>
    /// Methods for interrupting screen transitions
    /// </summary>
    public void StopLoading()
    {
        TargetFrame.StopLoading();
    }

    /// <summary>
    /// Method to delete all the history of the last viewed page from the history
    /// </summary>
    public void RemoveAllBackEntry()
    {
        var ns = TargetFrame;
        while (ns.CanGoBack)
        {
            ns.RemoveBackEntry();
        }
    }

    /// <summary>
    /// Property indicating whether the GoBack method can be executed in the Frame or NavigationWindow to which the screen transition is targeted.
    /// </summary>
    public bool CanGoBack => TargetFrame.CanGoBack;

    /// <summary>
    /// Property indicating whether the GoForward method can be executed in the Frame or NavigationWindow to which the screen transition is targeted.
    /// </summary>
    public bool CanGoForward => TargetFrame.CanGoForward;
}