namespace EnkuToolkit.Wpf.Controls;

using System;
using System.Windows;
using System.Windows.Shell;

/// <summary>
/// Customizable title bar window
/// </summary>
public class CustomTitlebarWindow : Window
{
    /// <summary>
    /// Dependency properties for specifying the title bar
    /// </summary>
    public static readonly DependencyProperty TitlebarProperty
        = DependencyProperty.Register(
            nameof(Titlebar),
            typeof(FrameworkElement),
            typeof(CustomTitlebarWindow),
            new PropertyMetadata(null, onTitlebarPropertyChanged)
        );

    /// <summary>
    /// CLR property corresponding to TitlebarProperty
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
    /// Dependency property to get or set a value indicating the width of the border used to resize the window
    /// </summary>
    public static readonly DependencyProperty ResizeBorderThicknessProeprty
        = DependencyProperty.Register(
            nameof(ResizeBorderThickness),
            typeof(Thickness),
            typeof(CustomTitlebarWindow),
            new PropertyMetadata(new Thickness(10, 0, 10, 10), onResizeBorderThicknessPropertyChanged)
        );

    /// <summary>
    /// CLR property for ResizeBorderThicknessProeprty dependency property
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
    /// Processing to solve the problem of edges being buried when the screen is maximized
    /// </summary>
    protected override void OnStateChanged(EventArgs e)
    {
        var nextMargin = this.WindowState == WindowState.Maximized ? 
                         new Thickness(8) : 
                         new Thickness(0);

        this.Titlebar.Margin = nextMargin;
    }

    /// <summary>
    /// Method called when the Initialized event occurs
    /// Internally sets the values of CaptionHeight and ResizeBorderThickness to WindowChrome
    /// internally sets the values of CaptionHeight and ResizeBorderThickness to WindowChrome from the properties of this class.
    /// </summary>
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        this._windowChrome.CaptionHeight = this.Titlebar?.Height ?? default(double);
        this._windowChrome.ResizeBorderThickness = this.ResizeBorderThickness;
    }

    /// <summary>
    /// constructor
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