﻿/*
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
namespace EnkuToolkit.Wpf.Behaviors;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using SystemNavigationMode = System.Windows.Navigation.NavigationMode;
using EnkuNavigationMode = UiIndependent.Navigation.NavigationMode;
using EnkuToolkit.UiIndependent.Navigation;
using EnkuToolkit.Wpf._internal;

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
    /// <param name="target">Target NavigationWindow</param>
    /// <param name="value">
    /// If true, the OnNavigated method is executed.
    /// If false, the OnNavigated method is not executed.
    /// </param>
    public static void SetIsSendNavigationParam(NavigationWindow target, bool value)
        => target.SetValue(IsSendNavigationParamProperty, value);

    /// <summary>
    /// Getter for IsSendNavigationParamProperty, an attached property to specify whether or not to pass parameters for screen transition by executing its OnNavigated method when the destination ViewModel is castable to INavigationAware after screen transition.
    /// </summary>
    /// <param name="target">Target NavigationWindow</param>
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
        navigationWindow.Navigating += OnNavigationWindowNavigating;
        navigationWindow.Unloaded += OnNavigationWindowUnloaded;
    }

    private static void OnNavigationWindowUnloaded(object sender, RoutedEventArgs e)
    {
        var navigationWindow = (NavigationWindow)sender;
        navigationWindow.Navigating -= OnNavigationWindowNavigating;
        navigationWindow.Unloaded -= OnNavigationWindowUnloaded;
    }

    private static void OnNavigationWindowNavigating(object sender, NavigatingCancelEventArgs e)
    {
        var navigationWindow = (NavigationWindow)sender;
        var isSendNavigationParam = GetIsSendNavigationParam(navigationWindow);
        if (!isSendNavigationParam) return;

        var navigationMode = NavigationModeUtils.EnkuNaviMode2WpfNaviMode(e.NavigationMode);
        var extraData = e.ExtraData;
        var oldPage = navigationWindow.Content as FrameworkElement;
        var oldNavigationAware = oldPage?.DataContext as INavigationAware;

        if (oldNavigationAware is not null)
            e.Cancel = oldNavigationAware.OnNavigatingFrom(extraData, navigationMode);

        if (e.Cancel) return;
        NavigatedEventHandler? handler = null;
        handler = (sender, e) =>
        {
            var nextPage = e.Content as FrameworkElement;
            var nextNavigationAware = nextPage?.DataContext as INavigationAware;

            oldNavigationAware?.OnNavigatedFrom(extraData, navigationMode);
            nextNavigationAware?.OnNavigatedTo(extraData, navigationMode);
            navigationWindow.Navigated -= handler;
        };
        navigationWindow.Navigated += handler;
    }
}