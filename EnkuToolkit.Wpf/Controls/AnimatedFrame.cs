namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using System;
using Constants;

/// <summary>
/// Frame for animated screen transitions
/// </summary>
public class AnimatedFrame : Frame
{
    /// <summary>
    /// constructor
    /// </summary>
    public AnimatedFrame()
    {
        this.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        this.Navigating += this.onNavigating;
        this.LoadCompleted += this.onLoadCompleted;
    }

    /// <summary>
    /// Dpi property in X direction used for OldImage in template
    /// </summary>
    public double DpiX { get; set; } = 96;

    /// <summary>
    /// Dpi property in Y direction used for OldImage in template
    /// </summary>
    public double DpiY { get; set; } = 96;

    /// <summary>
    /// Storyboard property to execute when displaying a new page or advancing the displayed page
    /// </summary>
    /// <remarks>
    /// If the BuiltinAnimType property is anything other than null, nothing is executed.
    /// </remarks>
    public Storyboard? ForwardAnim { get; set; }

    /// <summary>
    /// Storyboard property to execute when returning the displayed page
    /// </summary>
    /// <remarks>
    /// If the BuiltinAnimType property is anything other than null, nothing is executed.
    /// </remarks>
    public Storyboard? BackwardAnim { get; set; }

    /// <summary>
    /// Property for specifying the type of built-in animation to execute during screen transitions
    /// </summary>
    /// <remarks>
    /// If null is specified, the animation specified by the ForwardAnim and BackwardAnim properties is executed.
    /// </remarks>
    public BuiltinAnimTypes? BuiltinAnimType { get; set; }

    private void onNavigating(object sender, NavigatingCancelEventArgs e)
    {
        if (!this.IsLoaded) return;

        this._currentNavigationMode = e.NavigationMode;

        var oldImage = this.OldImageFromTemplate;
        var oldBitmap = new RenderTargetBitmap((int)this.ActualWidth,
                                               (int)this.ActualHeight,
                                               this.DpiX, this.DpiY, PixelFormats.Pbgra32);

        oldBitmap.Render(this);
        oldImage.Source = oldBitmap;
    }

    private void onLoadCompleted(object sender, NavigationEventArgs e)
    {
        if (this._currentNavigationMode is null) return;

        var oldImage = this.OldImageFromTemplate;
        var rootPanel = this.RootPanelFromTemplate;

        Storyboard? nextAnim = null;
        if (this.BuiltinAnimType is null)
        {
            nextAnim = this._currentNavigationMode == NavigationMode.Back ? this.BackwardAnim :
                       this._currentNavigationMode == NavigationMode.Forward ? this.ForwardAnim :
                       this._currentNavigationMode == NavigationMode.New ? this.ForwardAnim :
                       null;
        }
        else if(this.BuiltinAnimType == BuiltinAnimTypes.Slidein)
        {
            nextAnim = this._currentNavigationMode == NavigationMode.Back ? (Storyboard)rootPanel.TryFindResource("SlideinToLeft") :
                       this._currentNavigationMode == NavigationMode.Forward ? (Storyboard)rootPanel.TryFindResource("SlideinToRight") :
                       this._currentNavigationMode == NavigationMode.New ? (Storyboard)rootPanel.TryFindResource("SlideinToRight") :
                       null;
        }
        else if(this.BuiltinAnimType == BuiltinAnimTypes.ModernSlidein)
        {
            nextAnim = this._currentNavigationMode == NavigationMode.Back ? (Storyboard)rootPanel.TryFindResource("ModernSlideinToLeft") :
                       this._currentNavigationMode == NavigationMode.Forward ? (Storyboard)rootPanel.TryFindResource("ModernSlideinToRight") :
                       this._currentNavigationMode == NavigationMode.New ? (Storyboard)rootPanel.TryFindResource("ModernSlideinToRight") :
                       null;
        }

        if (nextAnim is null) return;

        EventHandler? completedEvent = null;
        completedEvent = (sender, e) =>
        {
            oldImage.Visibility = Visibility.Hidden;
            this.IsHitTestVisible = true;
            nextAnim.Completed -= completedEvent;
        };
        nextAnim.Completed += completedEvent;
        oldImage.Visibility = Visibility.Visible;
        this.IsHitTestVisible = false;
        nextAnim.Begin(rootPanel);

        this._currentNavigationMode = null;
    }

    private Image OldImageFromTemplate => (Image)this.Template.FindName("OldImage", this);
    private Grid RootPanelFromTemplate => (Grid)this.Template.FindName("RootPanel", this);

    private NavigationMode? _currentNavigationMode;

    static AnimatedFrame()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedFrame), new FrameworkPropertyMetadata(typeof(AnimatedFrame)));
    }
}