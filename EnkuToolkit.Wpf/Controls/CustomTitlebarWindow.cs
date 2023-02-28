namespace EnkuToolkit.Wpf.Controls;

using System;
using System.Windows;
using System.Windows.Shell;

/// <summary>
/// タイトルバーをカスタマイズ可能なWindow
/// </summary>
public class CustomTitlebarWindow : Window
{
    /// <summary>
    /// タイトルバーの内容を記すための依存関係プロパティ
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
    /// ウィンドウのサイズ変更で使用する端の幅を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ResizeBorderThicknessProeprty
        = DependencyProperty.Register(
            nameof(ResizeBorderThickness),
            typeof(Thickness),
            typeof(CustomTitlebarWindow),
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
        if (d is CustomTitlebarWindow window)
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