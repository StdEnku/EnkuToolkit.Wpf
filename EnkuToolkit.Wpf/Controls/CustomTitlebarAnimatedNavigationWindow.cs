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
    /// タイトルバーの内容を記すための依存関係プロパティ
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
    /// ウィンドウのサイズ変更で使用する端の幅を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ResizeBorderThicknessProeprty
        = DependencyProperty.Register(
            nameof(ResizeBorderThickness),
            typeof(Thickness),
            typeof(CustomTitlebarAnimatedNavigationWindow),
            new PropertyMetadata(new Thickness(10, 0, 10, 10), onResizeBorderThicknessPropertyChanged)
        );

    /// <summary>
    /// ResizeBorderThicknessProeprtyに対応するCLRプロパティ
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
    /// ウィンドウを最大化すると画面の端が埋もれてしまう問題への対策
    /// </summary>
    protected override void OnStateChanged(EventArgs e)
    {
        var nextMargin = this.WindowState == WindowState.Maximized ?
                         new Thickness(8, 8, 8, 0) :
                         new Thickness(0);

        this.Titlebar.Margin = nextMargin;
    }

    /// <summary>
    /// Initializedイベント時に呼ばれるWindowChromeへの
    /// CaptionHeightプロパティとResizeBorderThicknessプロパティを
    /// 本オブジェクトのプロパティから指定するためのメソッド
    /// </summary>
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        this._windowChrome.CaptionHeight = this.Titlebar?.Height ?? default(double);
        this._windowChrome.ResizeBorderThickness = this.ResizeBorderThickness;
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