namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Windows;
using System.Diagnostics;
using static Properties.Settings;

/// <summary>
/// Behavior to allow attaching to Window to save previous state
/// </summary>
public class WindowStateSaveBehavior
{
    /// <summary>
    /// Attachment property to specify whether previous state should be preserved
    /// </summary>
    public static readonly DependencyProperty IsStateSaveProperty
        = DependencyProperty.RegisterAttached(
            "IsStateSave",
            typeof(bool),
            typeof(WindowStateSaveBehavior),
            new PropertyMetadata(false, onIsStateSaveChanged)
        );

    /// <summary>
    /// IsStateSaveProperty attached property setter
    /// </summary>
    /// <param name="targetWindow">Window object to be attached</param>
    /// <param name="value">Value to be set</param>
    public static void SetIsStateSave(Window targetWindow, bool value)
        => targetWindow.SetValue(IsStateSaveProperty, value);

    /// <summary>
    /// Getter for IsStateSaveProperty attached property
    /// </summary>
    /// <param name="targetWindow">Window object to be attached</param>
    /// <returns>Value of the IsStateSaveProperty attached property</returns>
    public static bool GetIsStateSave(Window targetWindow)
        => (bool)targetWindow.GetValue(IsStateSaveProperty);

    private static void onIsStateSaveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var window = d as Window;
        Debug.Assert(window is not null);
        window.Initialized += onTargetInitialized;
        window.Closed += onTargetClosed;
    }

    private static void onTargetInitialized(object? sender, EventArgs e)
    {
        var window = sender as Window;
        Debug.Assert(window is not null);

        var isStateSave = GetIsStateSave(window);
        if (isStateSave)
        {
            window.Height = Default.WindowHeight;
            window.Width = Default.WindowWidth;
            window.Left = Default.WindowLeft;
            window.Top = Default.WindowTop;
            window.WindowState = Default.IsMaximizeState ? WindowState.Maximized : WindowState.Normal;
        }
    }

    private static void onTargetClosed(object? sender, EventArgs e)
    {
        var window = sender as Window;
        Debug.Assert(window is not null);

        var isStateSave = GetIsStateSave(window);
        if (isStateSave)
        {
            if (window.WindowState == WindowState.Normal)
            {
                Default.WindowHeight = window.Height;
                Default.WindowWidth = window.Width;
                Default.WindowLeft = window.Left;
                Default.WindowTop = window.Top;
                Default.IsMaximizeState = false;
            }
            else
            {
                Default.IsMaximizeState = true;
            }

            Default.Save();
        }

        window.Initialized -= onTargetInitialized;
        window.Closed -= onTargetClosed;
    }
}