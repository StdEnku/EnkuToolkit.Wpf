namespace EnkuToolkit.Wpf.Controls;

using System;
using System.Windows;
using static Properties.Settings;

/// <summary>
/// 以前の位置とサイズとWindowStateを記憶できるWindow
/// </summary>
public class StateSaveWindow : Window
{

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public StateSaveWindow() : base()
    {
        this.Height = Default.WindowHeight;
        this.Width = Default.WindowWidth;
        this.Left = Default.WindowLeft;
        this.Top = Default.WindowTop;
        this.WindowState = Default.IsMaximizeState ? WindowState.Maximized : WindowState.Normal;
    }

    /// <summary>
    /// Closedイベント発生時の処理
    /// </summary>
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        if (this.WindowState == WindowState.Normal)
        {
            Default.WindowHeight = this.Height;
            Default.WindowWidth = this.Width;
            Default.WindowLeft = this.Left;
            Default.WindowTop = this.Top;
            Default.IsMaximizeState = false;
        }
        else
        {
            Default.IsMaximizeState = true;
        }

        Default.Save();
    }
}