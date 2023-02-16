namespace EnkuToolkit.Wpf.Controls;

using System;
using System.Windows;
using System.Windows.Shell;

/// <summary>
/// タイトルバーをカスタマイズ可能なウィンドウ
/// </summary>
public class CustomTitlebarWindow : Window
{
    /// <summary>
    /// タイトルバーとして使用する領域の高さを指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty CaptionHeightProperty
        = DependencyProperty.Register(
            nameof(CaptionHeight),
            typeof(double),
            typeof(CustomTitlebarWindow),
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
        if (d is CustomTitlebarWindow window)
        {
            window._windowChrome.CaptionHeight = (double)e.NewValue;
        }
    }

    /// <summary>
    /// ウィンドウのサイズ変更に使用する境界線の幅を示す値を取得または設定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ResizeBorderThicknessProeprty
        = DependencyProperty.Register(
            nameof(ResizeBorderThickness),
            typeof(Thickness),
            typeof(CustomTitlebarWindow),
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
        if (d is CustomTitlebarWindow window)
        {
            window._windowChrome.ResizeBorderThickness = (Thickness)e.NewValue;
        }
    }

    /// <summary>
    /// 画面を最大化した時端が埋もれる問題を解決するための処理
    /// </summary>
    protected override void OnStateChanged(EventArgs e)
    {
        if (this.Content is FrameworkElement content)
        {
            var nextMargin = this.WindowState == WindowState.Maximized ? new Thickness(8) : new Thickness(0);
            content.Margin = nextMargin;
        }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CustomTitlebarWindow() : base()
    {
        this._windowChrome = new()
        {
            UseAeroCaptionButtons = false,
            NonClientFrameEdges = NonClientFrameEdges.Bottom,
            CaptionHeight = this.CaptionHeight,
            ResizeBorderThickness = this.ResizeBorderThickness,
        };
        WindowChrome.SetWindowChrome(this, this._windowChrome);
    }

    private WindowChrome _windowChrome;
}