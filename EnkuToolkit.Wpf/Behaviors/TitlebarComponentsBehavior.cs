namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Windows;
using System.Windows.Shell;

/// <summary>
/// CustomTitlebarWindow, CustomTitlebarAnimatedNavigationWindow, etc.
/// Objects implementing IInputElement, such as buttons used in custom titlebars, such as CustomTitlebarWindow and CustomTitlebarAnimatedNavigationWindow, must be clickable.
/// Behaviors for specifying whether an object that implements an IInputElement, such as a button used in a custom titlebar, is clickable
/// </summary>
/// <remarks>
/// This behavior is only implemented to unify the namespace, but the reality is that it is a thin wrapper for
/// IsHitTestVisibleInChrome.
/// </remarks>
public class TitlebarComponentsBehavior
{
    /// <summary>
    /// Attachment property to specify whether previous state should be preserved
    /// </summary>
    public static readonly DependencyProperty IsHitTestVisibleProperty
        = DependencyProperty.RegisterAttached(
            "IsHitTestVisible",
            typeof(bool),
            typeof(TitlebarComponentsBehavior),
            new PropertyMetadata(false, onIsHitTestVisibleChanged)
        );

    /// <summary>
    /// IsHitTestVisibleProperty attached property setter
    /// </summary>
    /// <param name="inputElement">DependencyObject that implements the target IInputElement</param>
    /// <param name="value">Value to be set</param>
    public static void SetIsHitTestVisible(IInputElement inputElement, bool value)
    {
        var attachedObject = inputElement as DependencyObject;

        if (attachedObject is null)
            throw new ArgumentException("The element must be a DependencyObject", "inputElement");

        attachedObject.SetValue(IsHitTestVisibleProperty, value);
    }

    /// <summary>
    /// Getter for IsHitTestVisibleProperty attached property
    /// </summary>
    /// <param name="inputElement">DependencyObject that implements the target IInputElement</param>
    /// <returns>Value of the IsStateSaveProperty attached property</returns>
    public static bool GetIsHitTestVisible(IInputElement inputElement)
    {
        var attachedObject = inputElement as DependencyObject;

        if (attachedObject is null)
            throw new ArgumentException("The element must be a DependencyObject", "inputElement");

        return (bool)attachedObject.GetValue(IsHitTestVisibleProperty);
    }

    private static void onIsHitTestVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var titlebarContent = (IInputElement)d;
        var value = (bool)e.NewValue;
        WindowChrome.SetIsHitTestVisibleInChrome(titlebarContent, value);
    }
}