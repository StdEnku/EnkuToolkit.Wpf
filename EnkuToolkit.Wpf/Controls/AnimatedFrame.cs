/*
 * MIT License
 * 
 * Copyright (c) 2023 StdEnku
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
namespace EnkuToolkit.Wpf.Controls;

using EnkuToolkit.Wpf.Constants;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

/// <summary>
/// Frame capable of executing effects during screen transitions
/// </summary>
public class AnimatedFrame : Frame
{
    #region Dependency property for specifying the horizontal dpi value of the image created when this control is imaged in the effect executed at update time.
    /// <summary>
    /// Dependency property for specifying the horizontal dpi value of the image created when this control is imaged in the effect executed at update time.
    /// </summary>
    public static readonly DependencyProperty SnapshotDpiXProperty
        = DependencyProperty.Register(
            nameof(SnapshotDpiX),
            typeof(int),
            typeof(AnimatedFrame),
            new PropertyMetadata(96)
        );

    /// <summary>
    /// CLR property for SnapshotDpiXProperty, which is a dependency property for specifying the horizontal dpi value of the image created when this control is imaged in the effect executed at update time.
    /// </summary>
    public int SnapshotDpiX
    {
        get => (int)GetValue(SnapshotDpiXProperty);
        set => SetValue(SnapshotDpiXProperty, value);
    }
    #endregion

    #region Dependency property for specifying the vertical dpi value of the image created when this control is imaged in the effect executed at update time.
    /// <summary>
    /// Dependency property for specifying the vertical dpi value of the image created when this control is imaged in the effect executed at update time.
    /// </summary>
    public static readonly DependencyProperty SnapshotDpiYProperty
        = DependencyProperty.Register(
            nameof(SnapshotDpiY),
            typeof(int),
            typeof(AnimatedFrame),
            new PropertyMetadata(96)
        );

    /// <summary>
    /// CLR property for SnapshotDpiYProperty, which is a dependency property for specifying the vertical dpi value of the image created by the effect that is executed when updating this control to create an image.
    /// </summary>
    public int SnapshotDpiY
    {
        get => (int)GetValue(SnapshotDpiYProperty);
        set => SetValue(SnapshotDpiYProperty, value);
    }
    #endregion

    #region Dependency property to specify the Storyboard to be executed when the Navigate or Forward method is executed
    /// <summary>
    /// Dependency property to specify the Storyboard to be executed when the Navigate or Forward method is executed
    /// </summary>
    /// <remarks>
    /// Ignored if Custom is not specified for the TransitionEffect value
    /// </remarks>
    public static readonly DependencyProperty ForwardStoryboardProperty
        = DependencyProperty.Register(
            nameof(ForwardStoryboard),
            typeof(Storyboard),
            typeof(AnimatedFrame),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for ForwardStoryboardProperty, a dependency property to specify the Storyboard to be executed when the Navigate or Forward method is executed
    /// </summary>
    public Storyboard? ForwardStoryboard
    {
        get => GetValue(ForwardStoryboardProperty) as Storyboard;
        set => SetValue(ForwardStoryboardProperty, value);
    }
    #endregion

    #region Dependency property to specify the Storyboard to be executed when the GoBack method is executed
    /// <summary>
    /// Dependency property to specify the Storyboard to be executed when the GoBack method is executed
    /// </summary>
    /// <remarks>
    /// Ignored if Custom is not specified for the TransitionEffect value
    /// </remarks>
    public static readonly DependencyProperty BackwardStoryboardProperty
        = DependencyProperty.Register(
            nameof(BackwardStoryboard),
            typeof(Storyboard),
            typeof(AnimatedFrame),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for BackwardStoryboardProperty, a dependency property to specify the Storyboard to be executed when the GoBack method is executed
    /// </summary>
    public Storyboard? BackwardStoryboard
    {
        get => GetValue(BackwardStoryboardProperty) as Storyboard;
        set => SetValue(BackwardStoryboardProperty, value);
    }
    #endregion

    #region Dependency property to specify the Storyboard to run when the page is refresh
    /// <summary>
    /// Dependency property to specify the Storyboard to run when the page is refresh
    /// </summary>
    /// <remarks>
    /// Ignored if Custom is not specified for the TransitionEffect value
    /// </remarks>
    public static readonly DependencyProperty ReloadStoryboardProperty
        = DependencyProperty.Register(
            nameof(ReloadStoryboard),
            typeof(Storyboard),
            typeof(AnimatedFrame),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for ReloadStoryboardProperty, a dependency property for specifying the Storyboard to run when the page is refresh
    /// </summary>
    public Storyboard? ReloadStoryboard
    {
        get => GetValue(ReloadStoryboardProperty) as Storyboard;
        set => SetValue(ReloadStoryboardProperty, value);
    }
    #endregion

    #region Dependency property for specifying the type of effect to be executed when the screen is refreshed
    /// <summary>
    /// Dependency property for specifying the type of effect to be executed when the screen is refreshed
    /// </summary>
    public static readonly DependencyProperty TransitionEffectProperty
        = DependencyProperty.Register(
            nameof(TransitionEffect),
            typeof(TransitionEffects),
            typeof(AnimatedFrame),
            new PropertyMetadata(TransitionEffects.None)
        );

    /// <summary>
    /// CLR property for TransitionEffectProperty, a dependency property for specifying the type of effect to be performed during screen updates
    /// </summary>
    public TransitionEffects TransitionEffect
    {
        get => (TransitionEffects)GetValue(TransitionEffectProperty);
        set => SetValue(TransitionEffectProperty, value);
    }
    #endregion

    /// <summary>
    /// Methods to be executed when loading templates
    /// </summary>
    public override void OnApplyTemplate()
    {
        Navigated += OnNavigated;
        Navigating += OnNavigating;
        NavigationUIVisibility = NavigationUIVisibility.Hidden;
        base.OnApplyTemplate();
    }

    private NavigationMode _navigationMode;

    private void OnNavigating(object sender, NavigatingCancelEventArgs e)
    {
        _transitionEffectContentControl.Snapshot();
        _navigationMode = e.NavigationMode;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (_navigationMode == NavigationMode.New || _navigationMode == NavigationMode.Forward)
            _transitionEffectContentControl.RunForwardEffect();
        else if (_navigationMode == NavigationMode.Back)
            _transitionEffectContentControl.RunBackwardEffect();
        else if (_navigationMode == NavigationMode.Refresh)
            _transitionEffectContentControl.RunReloadEffect();
    }

    private TransitionEffectContentControl _transitionEffectContentControl => (TransitionEffectContentControl)GetTemplateChild("transitionEffectContentControl");

    static AnimatedFrame()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(AnimatedFrame),
            new FrameworkPropertyMetadata(typeof(AnimatedFrame))
        );
    }
}