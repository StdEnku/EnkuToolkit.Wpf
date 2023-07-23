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
    public Thickness ResizeBorderThickness { get; init; } = new Thickness(8, 0, 8, 8);

    private ContentControl _titlebar => (ContentControl)GetTemplateChild("titlebar");

    private void MarginSetting()
    {
        var nextTitlebarMargin = WindowState == WindowState.Maximized ?
                                 new Thickness(8, 8, 8, 0) :
                                 new Thickness(0);

        var nextContentMargin = WindowState == WindowState.Maximized ?
                                 new Thickness(8, 0, 8, 8) :
                                 new Thickness(0);

        _titlebar.Margin = nextTitlebarMargin;
        if (Content is FrameworkElement content)
            content.Margin = nextContentMargin;
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

    private void OnTitlebarLoaded(object sender, RoutedEventArgs e)
    {
        var windowChrome = new WindowChrome()
        {
            CaptionHeight = _titlebar.ActualHeight,
            CornerRadius = CornerRadius,
            GlassFrameThickness = new Thickness(0),
            ResizeBorderThickness = ResizeBorderThickness,
            UseAeroCaptionButtons = false,
        };

        WindowChrome.SetWindowChrome(this, windowChrome);

        _titlebar.Loaded -= OnTitlebarLoaded;
    }

    static CustomizableTitlebarAnimatedNavigationWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomizableTitlebarAnimatedNavigationWindow),
            new FrameworkPropertyMetadata(typeof(CustomizableTitlebarAnimatedNavigationWindow))
        );
    }
}