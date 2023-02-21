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
/// 画面遷移時にアニメーションを実行可能なFrame
/// </summary>
public class AnimatedFrame : Frame
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AnimatedFrame()
    {
        this.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        this.Navigating += this.onNavigating;
        this.LoadCompleted += this.onLoadCompleted;
    }

    /// <summary>
    /// 画面遷移時に古いウィンドウをビットマップに変換する時に使うのX方向のDPI
    /// </summary>
    public double DpiX { get; set; } = 96;

    /// <summary>
    /// 画面遷移時に古いウィンドウをビットマップに変換する時に使うのY方向のDPI
    /// </summary>
    public double DpiY { get; set; } = 96;

    /// <summary>
    /// GoForwardメソッドかNavigateメソッド実行時に再生するStoryboardを指定するためのプロパティ
    /// </summary>
    /// <remarks>
    /// BuiltinAnimTypeプロパティがnull出ないと本プロパティは無視されます。
    /// </remarks>
    public Storyboard? ForwardAnim { get; set; }

    /// <summary>
    /// GoBackメソッド実行時に再生するStoryboardを指定するためのプロパティ
    /// </summary>
    /// <remarks>
    /// BuiltinAnimTypeプロパティがnull出ないと本プロパティは無視されます。
    /// </remarks>
    public Storyboard? BackwardAnim { get; set; }

    /// <summary>
    /// 本ライブラリに標準搭載されているアニメーションを使用する際に再生するアニメーションの種類を指定するためのプロパティ
    /// </summary>
    /// <remarks>
    /// nullを指定するとBackwardAnimプロパティとForwardAnimプロパティで指定したアニメーションが再生されます。
    /// 本プロパティがnullでBackwardAnimプロパティとForwardAnimプロパティがともにnullの場合アニメーションは実行されません。
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