﻿/*
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
namespace EnkuToolkit.Wpf.Behaviors;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using SystemNavigationMode = System.Windows.Navigation.NavigationMode;
using EnkuNavigationMode = UiIndependent.Navigation.NavigationMode;
using EnkuToolkit.UiIndependent.Navigation;

/// <summary>
/// Attached behaviors to extend NavigationWindow
/// </summary>
public class NavigationWindowExtensionBehavior
{
    /// <summary>
    /// Attached property to specify whether or not to pass parameters for screen transition by executing its OnNavigated method when the destination ViewModel is castable to INavigationAware after screen transition.
    /// </summary>
    public static readonly DependencyProperty IsSendNavigationParamProperty
        = DependencyProperty.RegisterAttached(
            "IsSendNavigationParam",
            typeof(bool),
            typeof(NavigationWindowExtensionBehavior),
            new PropertyMetadata(false, onIsSendChanged)
        );

    /// <summary>
    /// Setter for IsSendNavigationParamProperty, an attached property to specify whether or not to pass parameters for screen transition by executing its OnNavigated method when the destination ViewModel is castable to INavigationAware after screen transition.
    /// </summary>
    /// <param name="target">Target Frame</param>
    /// <param name="value">
    /// If true, the OnNavigated method is executed.
    /// If false, the OnNavigated method is not executed.
    /// </param>
    public static void SetIsSendNavigationParam(NavigationWindow target, bool value)
        => target.SetValue(IsSendNavigationParamProperty, value);

    /// <summary>
    /// Getter for IsSendNavigationParamProperty, an attached property to specify whether or not to pass parameters for screen transition by executing its OnNavigated method when the destination ViewModel is castable to INavigationAware after screen transition.
    /// </summary>
    /// <param name="target">Target Frame</param>
    /// <returns>
    /// If true, the OnNavigated method is executed.
    /// If false, the OnNavigated method is not executed.
    /// </returns>
    public static bool GetIsSendNavigationParam(NavigationWindow target)
        => (bool)target.GetValue(IsSendNavigationParamProperty);

    private static void onIsSendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var value = (bool)e.NewValue;
        var navigationWindow = (NavigationWindow)d;
        if (!value) return;
        navigationWindow.Navigating += OnFrameNavigating;
    }

    private static void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
    {
        var navigationWindow = (NavigationWindow)sender;
        var isSendNavigationParam = GetIsSendNavigationParam(navigationWindow);
        var navigationMode = e.NavigationMode;

        if (!isSendNavigationParam) return;

        NavigatedEventHandler? handler = null;
        handler = (sender, e) =>
        {
            var navigationWindow = (NavigationWindow)sender;
            var extraData = e.ExtraData;
            var nextPage = (Page)e.Content;

            if (nextPage.DataContext is INavigationAware navigationAware)
            {
                var nm = navigationMode == SystemNavigationMode.New ? EnkuNavigationMode.New :
                         navigationMode == SystemNavigationMode.Forward ? EnkuNavigationMode.Forward :
                         navigationMode == SystemNavigationMode.Back ? EnkuNavigationMode.Back :
                         EnkuNavigationMode.Refresh;
                navigationAware.OnNavigated(extraData, nm);
            }
            navigationWindow.Navigated -= handler;
        };

        navigationWindow.Navigated += handler;
    }
}