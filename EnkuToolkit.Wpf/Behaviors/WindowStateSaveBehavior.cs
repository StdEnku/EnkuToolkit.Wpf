namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Windows;
using System.Diagnostics;
using static Properties.Settings;

/// <summary>
/// Windowに添付して以前の状態を保存できるようにするためのビヘイビア
/// </summary>
public class WindowStateSaveBehavior
{
    /// <summary>
    /// 以前の状態を保存するか指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty IsStateSaveProperty
        = DependencyProperty.RegisterAttached(
            "IsStateSave",
            typeof(bool),
            typeof(WindowStateSaveBehavior),
            new PropertyMetadata(false, onIsStateSaveChanged)
        );

    /// <summary>
    /// IsStateSaveProperty添付プロパティのセッター
    /// </summary>
    /// <param name="targetWindow">添付対象のWindowオブジェクト</param>
    /// <param name="value">セットしたい値</param>
    public static void SetIsStateSave(Window targetWindow, bool value)
        => targetWindow.SetValue(IsStateSaveProperty, value);

    /// <summary>
    /// IsStateSaveProperty添付プロパティのゲッター
    /// </summary>
    /// <param name="targetWindow">添付対象のWindowオブジェクト</param>
    /// <returns>IsStateSaveProperty添付プロパティの値</returns>
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