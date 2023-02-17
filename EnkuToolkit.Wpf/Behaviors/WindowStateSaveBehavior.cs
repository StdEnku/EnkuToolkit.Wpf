namespace EnkuToolkit.Wpf.Behaviors;

using System;
using System.Windows;
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
    /// <param name="obj">対象のDependencyObject</param>
    /// <param name="value">セットしたい値</param>
    public static void SetIsStateSave(DependencyObject obj, bool value)
            => obj.SetValue(IsStateSaveProperty, value);

    /// <summary>
    /// IsStateSaveProperty添付プロパティのゲッター
    /// </summary>
    /// <param name="obj">対象のDependencyObject</param>
    /// <returns>IsStateSaveProperty添付プロパティの値</returns>
    public static bool GetIsStateSave(DependencyObject obj)
        => (bool)obj.GetValue(IsStateSaveProperty);

    private static void onIsStateSaveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var window = (Window)d;
        window.Initialized += onTargetInitialized;
        window.Closed += onTargetClosed;
    }

    private static void onTargetInitialized(object? sender, EventArgs e)
    {
        if (sender is Window window)
        {
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
    }

    private static void onTargetClosed(object? sender, EventArgs e)
    {
        if (sender is Window window)
        {
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
}