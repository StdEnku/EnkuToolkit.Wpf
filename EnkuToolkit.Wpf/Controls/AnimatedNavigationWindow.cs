namespace EnkuToolkit.Wpf.Controls;

using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System;
using System.Windows.Navigation;
using Constants;

/// <summary>
/// アニメーション付きの画面遷移が行えるNavigationWindow
/// </summary>
public class AnimatedNavigationWindow : NavigationWindow
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AnimatedNavigationWindow()
    {
        this.Navigating += this.onNavigating;
        this.LoadCompleted += this.onLoadCompleted;
    }

    /// <summary>
    /// テンプレート内のOldImageで使用するX方向のDpiプロパティ
    /// </summary>
    public double DpiX { get; set; } = 96;

    /// <summary>
    /// テンプレート内のOldImageで使用するY方向のDpiプロパティ
    /// </summary>
    public double DpiY { get; set; } = 96;

    /// <summary>
    /// 新しいページ表示or表示しているページを進める場合に実行するStoryboardプロパティ
    /// </summary>
    /// <remarks>
    /// BuiltinAnimTypeプロパティがnull以外の場合は何も実行されません。
    /// </remarks>
    public Storyboard? ForwardAnim { get; set; }

    /// <summary>
    /// 表示しているページを戻す場合に実行するStoryboardプロパティ
    /// </summary>
    /// <remarks>
    /// BuiltinAnimTypeプロパティがnull以外の場合は何も実行されません。
    /// </remarks>
    public Storyboard? BackwardAnim { get; set; }

    /// <summary>
    /// 画面遷移時に実行するビルトインアニメーションの種類を指定するためのプロパティ
    /// </summary>
    /// <remarks>
    /// nullを指定した場合ForwardAnimプロパティとBackwardAnimプロパティで指定したアニメーションが実行されます。
    /// </remarks>
    public BuiltinAnimTypes? BuiltinAnimType { get; set; }

    private void onNavigating(object sender, NavigatingCancelEventArgs e)
    {
        if (!this.IsLoaded) return;

        this._currentNavigationMode = e.NavigationMode;

        var oldImage = this.OldImageFromTemplate;
        var currentTransform = this.CurrentTransformFromTemplate;
        var oldBitmap = new RenderTargetBitmap((int)currentTransform.ActualWidth,
                                               (int)currentTransform.ActualHeight,
                                               this.DpiX, this.DpiY, PixelFormats.Pbgra32);

        oldBitmap.Render(currentTransform);
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
        else if (this.BuiltinAnimType == BuiltinAnimTypes.Slidein)
        {
            nextAnim = this._currentNavigationMode == NavigationMode.Back ? (Storyboard)rootPanel.TryFindResource("SlideinToLeft") :
                       this._currentNavigationMode == NavigationMode.Forward ? (Storyboard)rootPanel.TryFindResource("SlideinToRight") :
                       this._currentNavigationMode == NavigationMode.New ? (Storyboard)rootPanel.TryFindResource("SlideinToRight") :
                       null;
        }
        else if (this.BuiltinAnimType == BuiltinAnimTypes.ModernSlidein)
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
    private NormalizedTransformContentControl CurrentTransformFromTemplate => (NormalizedTransformContentControl)this.Template.FindName("CurrentTransform", this);

    private NavigationMode? _currentNavigationMode;

    static AnimatedNavigationWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedNavigationWindow), new FrameworkPropertyMetadata(typeof(AnimatedNavigationWindow)));
    }
}