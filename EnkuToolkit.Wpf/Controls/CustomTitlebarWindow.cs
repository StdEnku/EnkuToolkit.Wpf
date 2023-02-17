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
    /// タイトルバーを指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TitlebarProperty
        = DependencyProperty.Register(
            nameof(Titlebar),
            typeof(FrameworkElement),
            typeof(CustomTitlebarWindow),
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
        var window = (CustomTitlebarWindow)d;
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
    public CustomTitlebarWindow() : base()
    {
        this._windowChrome = new()
        {
            UseAeroCaptionButtons = false,
            NonClientFrameEdges = NonClientFrameEdges.Bottom,
        };
        WindowChrome.SetWindowChrome(this, this._windowChrome);
    }

    static CustomTitlebarWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTitlebarWindow), new FrameworkPropertyMetadata(typeof(CustomTitlebarWindow)));
    }

    private WindowChrome _windowChrome;
}