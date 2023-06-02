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
namespace EnkuToolkit.Wpf.Controls;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

/// <summary>
/// Customizable title bar AnimatedNavigationWindow
/// </summary>
public class CustomizableTitlebarAnimatedNavigationWindow : AnimatedNavigationWindow
{
    /// <summary>
    /// Property for specifying the interior of the title bar
    /// </summary>
    public FrameworkElement? Titlebar { get; init; }

    /// <summary>
    /// Property for specifying rounded corners
    /// </summary>
    public CornerRadius CornerRadius { get; init; } = new CornerRadius(0);

    /// <summary>
    /// Gets or sets a value indicating the width of the border used to resize the window
    /// </summary>
    public Thickness ResizeBorderThickness { get; init; } = new Thickness(15, 0, 15, 15);

    private ContentControl _titlebar => (ContentControl)GetTemplateChild("titlebar");

    private void MarginSetting()
    {
        var nextMargin = WindowState == WindowState.Maximized ?
                         new Thickness(8, 8, 8, 0) :
                         new Thickness(0);

        _titlebar.Margin = nextMargin;
    }

    /// <summary>
    /// Processing to be performed when the WindowState property is changed
    /// </summary>
    protected override void OnStateChanged(EventArgs e)
    {
        MarginSetting();
        base.OnStateChanged(e);
    }

    /// <summary>
    /// Process executed when template is loaded
    /// </summary>
    /// <exception cref="InvalidOperationException">Exception thrown when Titlebar.Height is an invalid value</exception>
    public override void OnApplyTemplate()
    {
        MarginSetting();
        _titlebar.Content = Titlebar;
        _titlebar.Loaded += OnTitlebarLoaded;
        base.OnApplyTemplate();
    }

    private static void OnTitlebarLoaded(object sender, RoutedEventArgs e)
    {
        var titlebar = (ContentControl)sender;
        var customizableTitlebarAnimatedNavigationWindow = (CustomizableTitlebarAnimatedNavigationWindow)titlebar.TemplatedParent;

        var windowChrome = new WindowChrome()
        {
            CaptionHeight = titlebar.ActualHeight,
            CornerRadius = customizableTitlebarAnimatedNavigationWindow.CornerRadius,
            GlassFrameThickness = new Thickness(0),
            //NonClientFrameEdges = NonClientFrameEdges.Bottom,
            ResizeBorderThickness = customizableTitlebarAnimatedNavigationWindow.ResizeBorderThickness,
            UseAeroCaptionButtons = false,
        };

        WindowChrome.SetWindowChrome(customizableTitlebarAnimatedNavigationWindow, windowChrome);
    }

    static CustomizableTitlebarAnimatedNavigationWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomizableTitlebarAnimatedNavigationWindow),
            new FrameworkPropertyMetadata(typeof(CustomizableTitlebarAnimatedNavigationWindow))
        );
    }
}