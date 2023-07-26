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
using System.Windows.Media.Animation;

/// <summary>
/// CustomizableCalendar to support animation effects when updating
/// </summary>
public class AnimatedCustomizableCalendar : CustomizableCalendar
{
    #region Dependency property for setting the horizontal Dpi value specified when creating a snapshot of Content
    /// <summary>
    /// Dependency property for setting the horizontal Dpi value specified when creating a snapshot of Content
    /// </summary>
    public static readonly DependencyProperty SnapshotDpiXProperty
        = DependencyProperty.Register(
            nameof(SnapshotDpiX),
            typeof(int),
            typeof(AnimatedCustomizableCalendar),
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
            typeof(AnimatedCustomizableCalendar),
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
            typeof(AnimatedCustomizableCalendar),
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
            typeof(AnimatedCustomizableCalendar),
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
            typeof(AnimatedCustomizableCalendar),
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

    #region Dependency property for specifying the type of effect to be executed when the screen is refreshed
    /// <summary>
    /// Dependency property for specifying the type of effect to be executed when the screen is refreshed
    /// </summary>
    public static readonly DependencyProperty TransitionEffectProperty
        = DependencyProperty.Register(
            nameof(TransitionEffect),
            typeof(TransitionEffects),
            typeof(AnimatedCustomizableCalendar),
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

    #region Dependency property that is True if the effect is being executed
    /// <summary>
    /// Dependency property that is True if the effect is being executed
    /// </summary>
    public static readonly DependencyProperty IsCompletedEffectProperty
        = DependencyProperty.Register(
            nameof(IsCompletedEffect),
            typeof(bool),
            typeof(AnimatedCustomizableCalendar),
            new PropertyMetadata(true)
        );

    /// <summary>
    /// CLR property for IsCompletedProperty, a dependency property that is True if the effect is running
    /// </summary>
    public bool IsCompletedEffect
    {
        get => (bool)GetValue(IsCompletedEffectProperty);
        set => SetValue(IsCompletedEffectProperty, value);
    }
    #endregion

    TransitionEffectContentControl _transitionEffectContentControl => (TransitionEffectContentControl)GetTemplateChild("transitionEffectContentControl");

    /// <summary>
    /// Constructor
    /// </summary>
    public AnimatedCustomizableCalendar()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        CellsUpdating -= OnCellsUpdating;
        CellsUpdated -= OnCellsUpdated;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        CellsUpdating += OnCellsUpdating;
        CellsUpdated += OnCellsUpdated;
    }

    private void OnCellsUpdated(object sender, CellsUpdateEventArgs e)
    {
        if (e.UpdateMode == UpdateMode.Forward)
        {
            _transitionEffectContentControl.RunForwardEffect();
        }
        else if (e.UpdateMode == UpdateMode.Backward)
        {
            _transitionEffectContentControl.RunBackwardEffect();
        }
        else if (e.UpdateMode == UpdateMode.Reflesh)
        {
            _transitionEffectContentControl.RunReloadEffect();
        }
    }

    private void OnCellsUpdating(object sender, CellsUpdateEventArgs e)
    {
        _transitionEffectContentControl.Snapshot();
    }

    static AnimatedCustomizableCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(AnimatedCustomizableCalendar),
            new FrameworkPropertyMetadata(typeof(AnimatedCustomizableCalendar))
        );
    }
}