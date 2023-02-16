namespace EnkuToolkit.Wpf.Controls;

using System;
using System.Windows;
using System.Windows.Shell;

/// <summary>
/// タイトルバーの無いウィンドウ
/// </summary>
public class EmptyWindow : Window
{
    /// <summary>
    /// タイトルバーとして使用する領域の高さを指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty CaptionHeightProperty
        = DependencyProperty.Register(
            nameof(CaptionHeight),
            typeof(double),
            typeof(EmptyWindow),
            new PropertyMetadata(default(double), onCaptionHeightPropertyChanged)
        );

    /// <summary>
    /// CaptionHeightProperty依存関係プロパティ用のCLRプロパティ
    /// </summary>
    public double CaptionHeight
    {
        get => (double)this.GetValue(CaptionHeightProperty);
        set => this.SetValue(CaptionHeightProperty, value);
    }

    private static void onCaptionHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EmptyWindow emptyWindow)
        {
            var newCaptionHeight = (double)e.NewValue;
            var oldWindowChrome = WindowChrome.GetWindowChrome(emptyWindow);
            var newWindowChrome = GetWindowChrom(newCaptionHeight, oldWindowChrome.ResizeBorderThickness);
            WindowChrome.SetWindowChrome(emptyWindow, newWindowChrome);
        }
    }

    /// <summary>
    /// ウィンドウのサイズ変更に使用する境界線の幅を示す値を取得または設定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ResizeBorderThicknessProeprty
        = DependencyProperty.Register(
            nameof(ResizeBorderThickness),
            typeof(Thickness),
            typeof(EmptyWindow),
            new PropertyMetadata(new Thickness(10, 0, 10, 10), onResizeBorderThicknessPropertyChanged)
        );

    /// <summary>
    /// ResizeBorderThicknessProeprty依存関係プロパティ用のCLRプロパティ
    /// </summary>
    public Thickness ResizeBorderThickness
    {
        get => (Thickness)this.GetValue(ResizeBorderThicknessProeprty);
        set => this.SetValue(ResizeBorderThicknessProeprty, value);
    }

    private static void onResizeBorderThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is EmptyWindow emptyWindow)
        {
            var newResizeBorderThickness = (Thickness)e.NewValue;
            var oldWindowChrome = WindowChrome.GetWindowChrome(emptyWindow);
            var newWindowChrome = GetWindowChrom(oldWindowChrome.CaptionHeight, newResizeBorderThickness);
            WindowChrome.SetWindowChrome(emptyWindow, newWindowChrome);
        }
    }

    protected override void OnStateChanged(EventArgs e)
    {
        if (this.Content is FrameworkElement content)
        {
            var nextMargin = this.WindowState == WindowState.Maximized ? new Thickness(8) : new Thickness(0);
            content.Margin = nextMargin;
        }
    }

    private static WindowChrome GetWindowChrom(double captionHeight, Thickness resizeBorderThickness)
    {
        return new WindowChrome()
        {
            UseAeroCaptionButtons = false,
            NonClientFrameEdges = NonClientFrameEdges.Bottom,
            CaptionHeight = captionHeight,
            ResizeBorderThickness = resizeBorderThickness,
        };
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public EmptyWindow() : base()
    {
        var winowChrome = GetWindowChrom(this.CaptionHeight, this.ResizeBorderThickness);
        WindowChrome.SetWindowChrome(this, winowChrome);
    }
}