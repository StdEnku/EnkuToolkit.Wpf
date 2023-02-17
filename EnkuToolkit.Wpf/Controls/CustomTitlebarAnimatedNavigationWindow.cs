namespace EnkuToolkit.Wpf.Controls;

using System.Windows.Shell;
using System.Windows;
using System;

/// <summary>
/// タイトルバーをカスタマイズ可能なAnimatedNavigationWindow
/// </summary>
public class CustomTitlebarAnimatedNavigationWindow : AnimatedNavigationWindow
{
    /// <summary>
    /// タイトルバーを指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TitlebarProperty
        = DependencyProperty.Register(
            nameof(Titlebar),
            typeof(FrameworkElement),
            typeof(CustomTitlebarAnimatedNavigationWindow),
            new PropertyMetadata(null, onTitlebarPropertyChanged)
        );

    /// <summary>
    /// TitlebarPropertyに対応するCLRプロパティ
    /// </summary>
    public FrameworkElement Titlebar
    {
        get => (FrameworkElement)this.GetValue(TitlebarProperty);
        set => this.SetValue(TitlebarProperty, value);
    }

    private static void onTitlebarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var window = (CustomTitlebarAnimatedNavigationWindow)d;
        if (!window.IsInitialized) return;
        var titlebar = (FrameworkElement)e.NewValue;
        window._windowChrome.CaptionHeight = titlebar.Height;
    }

    /// <summary>
    /// ウィンドウのサイズ変更に使用する境界線の幅を示す値を取得または設定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ResizeBorderThicknessProeprty
        = DependencyProperty.Register(
            nameof(ResizeBorderThickness),
            typeof(Thickness),
            typeof(CustomTitlebarAnimatedNavigationWindow),
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
        if (d is CustomTitlebarAnimatedNavigationWindow window)
        {
            window._windowChrome.ResizeBorderThickness = (Thickness)e.NewValue;
        }
    }

    /// <summary>
    /// 画面を最大化した時端が埋もれる問題を解決するための処理
    /// </summary>
    protected override void OnStateChanged(EventArgs e)
    {
        var nextMargin = this.WindowState == WindowState.Maximized ?
                         new Thickness(8) :
                         new Thickness(0);

        this.Titlebar.Margin = nextMargin;
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        this._windowChrome.CaptionHeight = this.Titlebar?.Height ?? default(double);
        this.ResizeBorderThickness = this.ResizeBorderThickness;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CustomTitlebarAnimatedNavigationWindow() : base()
    {
        this._windowChrome = new()
        {
            UseAeroCaptionButtons = false,
            NonClientFrameEdges = NonClientFrameEdges.Bottom,
        };
        WindowChrome.SetWindowChrome(this, this._windowChrome);
    }

    static CustomTitlebarAnimatedNavigationWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTitlebarAnimatedNavigationWindow), new FrameworkPropertyMetadata(typeof(CustomTitlebarAnimatedNavigationWindow)));
    }

    private WindowChrome _windowChrome;
}