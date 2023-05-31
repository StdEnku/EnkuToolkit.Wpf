namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System;
using EnkuToolkit.Wpf.Constants;
using EnkuToolkit.Wpf.Animations;

/// <summary>
/// ContentControl that can express the effects of state transitions
/// </summary>
public class TransitionEffectContentControl : ContentControl
{
    #region Dependency property for setting the horizontal Dpi value specified when creating a snapshot of Content
    /// <summary>
    /// Dependency property for setting the horizontal Dpi value specified when creating a snapshot of Content
    /// </summary>
    public static readonly DependencyProperty SnapshotDpiXProperty
        = DependencyProperty.Register(
            nameof(SnapshotDpiX),
            typeof(int),
            typeof(TransitionEffectContentControl),
            new PropertyMetadata(96)
        );

    /// <summary>
    /// CLR property for the dependency property to set the horizontal Dpi value specified when creating a snapshot of Content
    /// </summary>
    public int SnapshotDpiX
    {
        get => (int)GetValue(SnapshotDpiXProperty);
        set => SetValue(SnapshotDpiXProperty, value);
    }
    #endregion

    #region Dependency property for setting the vertical Dpi value specified when creating a snapshot of Content
    /// <summary>
    /// Dependency property for setting the vertical Dpi value specified when creating a snapshot of Content
    /// </summary>
    public static readonly DependencyProperty SnapshotDpiYProperty
        = DependencyProperty.Register(
            nameof(SnapshotDpiY),
            typeof(int),
            typeof(TransitionEffectContentControl),
            new PropertyMetadata(96)
        );

    /// <summary>
    /// CLR property for the dependency property to set the vertical Dpi value specified when creating a snapshot of Content
    /// </summary>
    public int SnapshotDpiY
    {
        get => (int)GetValue(SnapshotDpiYProperty);
        set => SetValue(SnapshotDpiYProperty, value);
    }
    #endregion

    #region Dependency properties of type Storyboard, including animations executed during forward state transitions
    /// <summary>
    /// Dependency properties of type Storyboard, including animations executed during forward state transitions
    /// </summary>
    /// <remarks>
    /// If the value of TransitionEffect, a dependency property of this class, is not set to Custom, it is ignored.
    /// </remarks>
    public static readonly DependencyProperty ForwardStoryboardProperty
        = DependencyProperty.Register(
            nameof(ForwardStoryboard),
            typeof(Storyboard),
            typeof(TransitionEffectContentControl),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for ForwardStoryboardProperty, a dependency property of type Storyboard that contains an animation to be executed during a forward state transition
    /// </summary>
    /// <remarks>
    /// If the value of TransitionEffect, a dependency property of this class, is not set to Custom, it is ignored.
    /// </remarks>
    public Storyboard? ForwardStoryboard
    {
        get => GetValue(ForwardStoryboardProperty) as Storyboard;
        set => SetValue(ForwardStoryboardProperty, value);
    }
    #endregion

    #region Dependency properties of type Storyboard containing animations to be performed during state transitions in the reverse direction
    /// <summary>
    /// Dependency properties of type Storyboard containing animations to be performed during state transitions in the reverse direction
    /// </summary>
    /// <remarks>
    /// If the value of TransitionEffect, a dependency property of this class, is not set to Custom, it is ignored.
    /// </remarks>
    public static readonly DependencyProperty BackwardStoryboardProperty
        = DependencyProperty.Register(
            nameof(BackwardStoryboard),
            typeof(Storyboard),
            typeof(TransitionEffectContentControl),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for BackwardStoryboard, a dependency property of type Storyboard that contains an animation to be executed during a state transition in the reverse direction
    /// </summary>
    /// <remarks>
    /// If the value of TransitionEffect, a dependency property of this class, is not set to Custom, it is ignored.
    /// </remarks>
    public Storyboard? BackwardStoryboard
    {
        get => GetValue(BackwardStoryboardProperty) as Storyboard;
        set => SetValue(BackwardStoryboardProperty, value);
    }
    #endregion

    #region Dependency properties of type Storyboard, including animations executed on reload
    /// <summary>
    /// Dependency properties of type Storyboard, including animations executed on reload
    /// </summary>
    /// <remarks>
    /// If the value of TransitionEffect, a dependency property of this class, is not set to Custom, it is ignored.
    /// </remarks>
    public static readonly DependencyProperty ReloadStoryboardProperty
        = DependencyProperty.Register(
            nameof(ReloadStoryboard),
            typeof(Storyboard),
            typeof(TransitionEffectContentControl),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for ReloadStoryboardProperty, a dependency property of type Storyboard that contains the animation to be executed when reloading
    /// </summary>
    /// <remarks>
    /// If the value of TransitionEffect, a dependency property of this class, is not set to Custom, it is ignored.
    /// </remarks>
    public Storyboard? ReloadStoryboard
    {
        get => GetValue(ReloadStoryboardProperty) as Storyboard;
        set => SetValue(ReloadStoryboardProperty, value);
    }
    #endregion

    #region A set of properties for retrieving child elements in a template
    private TransformContentControl _mainTc => (TransformContentControl)GetTemplateChild("mainTc");

    private TransformContentControl _imageTc => (TransformContentControl)GetTemplateChild("imageTc");

    private Image _image => (Image)GetTemplateChild("image");

    private Grid _rootPanel => (Grid)GetTemplateChild("rootPanel");
    #endregion

    #region Property for specifying the effect to be executed when the state changes
    /// <summary>
    /// Property for specifying the effect to be executed when the state changes
    /// </summary>
    public static readonly DependencyProperty TransitionEffectProperty
        = DependencyProperty.Register(
            nameof(TransitionEffect),
            typeof(TransitionEffects),
            typeof(TransitionEffectContentControl),
            new PropertyMetadata(TransitionEffects.None)
        );

    /// <summary>
    /// CLR property for TransitionEffectProperty, a property for specifying the effect to be performed during a state change
    /// </summary>
    public TransitionEffects TransitionEffect
    {
        get => (TransitionEffects)GetValue(TransitionEffectProperty);
        set => SetValue(TransitionEffectProperty, value);
    }
    #endregion

    #region Dependency property that is True if the effect is being executed
    /// <summary>
    /// Dependency property that is True if the effect is being executed
    /// </summary>
    public static readonly DependencyProperty IsCompletedProperty
        = DependencyProperty.Register(
            nameof(IsCompleted),
            typeof(bool),
            typeof(TransitionEffectContentControl),
            new PropertyMetadata(true)
        );

    /// <summary>
    /// CLR property for IsCompletedProperty, a dependency property that is True if the effect is running
    /// </summary>
    public bool IsCompleted
    {
        get => (bool)GetValue(IsCompletedProperty);
        set => SetValue(IsCompletedProperty, value);
    }
    #endregion

    /// <summary>
    /// Method to convert the currently displayed Content into an image and load it into the Image source
    /// </summary>
    public void Snapshot()
    {
        var bitmap = new RenderTargetBitmap(
            (int)ActualWidth,
            (int)ActualHeight,
            SnapshotDpiX, SnapshotDpiY, PixelFormats.Pbgra32
        );

        bitmap.Render((Visual)this.Content);
        _image.Source = bitmap;
    }

    /// <summary>
    /// A method that executes an animation for a forward state transition. Before executing this method, be sure to register a snapshot of the state before the update using the Snapshot method before calling this method.
    /// </summary>
    public void RunForwardEffect()
    {
        var storyBoard = GetForwardStoryboard(TransitionEffect);
        if (storyBoard is null) return;
        RunStoryboard(storyBoard);
    }

    /// <summary>
    /// A method that executes an animation for a backward state transition. Before executing this method, be sure to register a snapshot of the state before the update using the Snapshot method before calling this method.
    /// </summary>
    public void RunBackwardEffect()
    {
        var storyBoard = GetBackwardStoryboard(TransitionEffect);
        if (storyBoard is null) return;
        RunStoryboard(storyBoard);
    }

    /// <summary>
    /// A method that executes an animation for reloading. Before executing this method, be sure to register a snapshot of the state before the update using the Snapshot method before calling this method.
    /// </summary>
    public void RunReloadwardEffect()
    {
        var storyBoard = GetReloadStoryboard(TransitionEffect);
        if (storyBoard is null) return;
        RunStoryboard(storyBoard);
    }

    private void RunStoryboard(Storyboard storyboard)
    {
        EventHandler? completedEvent = null;
        completedEvent = (sender, e) =>
        {
            _image.Visibility = Visibility.Hidden;
            this.IsHitTestVisible = true;
            storyboard.Completed -= completedEvent;
            IsCompleted = true;
        };

        IsCompleted = false;
        storyboard.Completed += completedEvent;
        _image.Visibility = Visibility.Visible;
        this.IsHitTestVisible = false;
        storyboard.Begin(_rootPanel);
    }

    private Storyboard? GetForwardStoryboard(TransitionEffects transitionEffects)
    {
        return transitionEffects == TransitionEffects.None ? null :
               transitionEffects == TransitionEffects.Custom ? ForwardStoryboard :
               transitionEffects == TransitionEffects.HorizontalModernSlide ? (Storyboard)_rootPanel.Resources[ModernSlideAnimationNames.ModernSlideToLeftAnimation] :
               transitionEffects == TransitionEffects.VerticalModernSlide ? (Storyboard)_rootPanel.Resources[ModernSlideAnimationNames.ModernSlideToDownAnimation] :
               transitionEffects == TransitionEffects.HorizontalSlide ? (Storyboard)_rootPanel.Resources[SlideAnimationNames.SlideToLeftAnimation] :
               transitionEffects == TransitionEffects.VerticalSlide ? (Storyboard)_rootPanel.Resources[SlideAnimationNames.SlideToDownAnimation] :
               transitionEffects == TransitionEffects.FadeOnly ? (Storyboard)_rootPanel.Resources[FadeAnimationNames.FadeAnimation] : null;
    }

    private Storyboard? GetBackwardStoryboard(TransitionEffects transitionEffects)
    {
        return transitionEffects == TransitionEffects.None ? null :
               transitionEffects == TransitionEffects.Custom ? BackwardStoryboard :
               transitionEffects == TransitionEffects.HorizontalModernSlide ? (Storyboard)_rootPanel.Resources[ModernSlideAnimationNames.ModernSlideToRightAnimation] :
               transitionEffects == TransitionEffects.VerticalModernSlide ? (Storyboard)_rootPanel.Resources[ModernSlideAnimationNames.ModernSlideToUpAnimation] :
               transitionEffects == TransitionEffects.HorizontalSlide ? (Storyboard)_rootPanel.Resources[SlideAnimationNames.SlideToRightAnimation] :
               transitionEffects == TransitionEffects.VerticalSlide ? (Storyboard)_rootPanel.Resources[SlideAnimationNames.SlideToUpAnimation] :
               transitionEffects == TransitionEffects.FadeOnly ? (Storyboard)_rootPanel.Resources[FadeAnimationNames.FadeAnimation] : null;
    }

    private Storyboard? GetReloadStoryboard(TransitionEffects transitionEffects)
    {
        return transitionEffects == TransitionEffects.None ? null :
               transitionEffects == TransitionEffects.Custom ? ReloadStoryboard : 
               (Storyboard)_rootPanel.Resources[FadeAnimationNames.FadeAnimation];
    }

    /// <summary>
    /// Processing to be performed when the template is applied
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    static TransitionEffectContentControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(TransitionEffectContentControl),
            new FrameworkPropertyMetadata(typeof(TransitionEffectContentControl))
        );
    }
}