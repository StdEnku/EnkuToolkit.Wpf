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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// ContentControl that allows transform operations to be manipulated with dependency properties
/// </summary>
public class TransformContentControl : ContentControl
{
    private readonly ScaleTransform _scaleTransform = new();
    private readonly RotateTransform _rotateTransform = new();
    private readonly TranslateTransform _translateTransform = new();
    private readonly Point DefaultRenderTransformOrigin = new Point(0.5, 0.5);
    private const double MaxAngle = 360;
    private const double DefaultSize = 1;

    /// <summary>
    /// Constructor
    /// </summary>
    public TransformContentControl()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(_scaleTransform);
        transformGroup.Children.Add(_rotateTransform);
        transformGroup.Children.Add(_translateTransform);
        RenderTransform = transformGroup;
        RenderTransformOrigin = DefaultRenderTransformOrigin;
    }

    #region Dependency property for moving in the X direction
    /// <summary>
    /// Dependency property for moving in the X direction
    /// </summary>
    /// <remarks>
    /// Set to the original position at 0.
    /// Moves to the same position as ActualWidth at 1
    /// Moves to the same position as -ActualWidth with -1
    /// </remarks>
    public static readonly DependencyProperty TranslateXProperty
        = DependencyProperty.Register(
            nameof(TranslateX),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onTranslateXPropertyChanged)
        );

    /// <summary>
    /// CLR property for TranslateXProperty, a dependency property for moving in the X direction
    /// </summary>
    /// <remarks>
    /// Set to the original position at 0.
    /// Moves to the same position as ActualWidth at 1
    /// Moves to the same position as -ActualWidth with -1
    /// </remarks>
    public double TranslateX
    {
        get => (double)GetValue(TranslateXProperty);
        set => SetValue(TranslateXProperty, value);
    }

    private static void onTranslateXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var transformContentControl = (TransformContentControl)d;
        var value = (double)e.NewValue;
        var width = transformContentControl.ActualWidth;
        var scale = transformContentControl.ScaleX;
        transformContentControl._translateTransform.X = value * width * scale;
    }
    #endregion

    #region Dependency property for moving in the Y direction
    /// <summary>
    /// Dependency property for moving in the Y direction
    /// </summary>
    /// <remarks>
    /// Set to original height at 0.
    /// Moves to the same height as ActualHeight with 1
    /// Moves to a height equal to -1 to -ActualHeight
    /// </remarks>
    public static readonly DependencyProperty TranslateYProperty
        = DependencyProperty.Register(
            nameof(TranslateY),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onTranslateYPropertyChanged)
        );

    /// <summary>
    /// CLR property for TranslateYProperty, a dependency property for moving in the Y direction
    /// </summary>
    /// <remarks>
    /// Set to original height at 0.
    /// Moves to the same height as ActualHeight with 1
    /// Moves to a height equal to -1 to -ActualHeight
    /// </remarks>
    public double TranslateY
    {
        get => (double)GetValue(TranslateYProperty);
        set => SetValue(TranslateYProperty, value);
    }

    private static void onTranslateYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var transformContentControl = (TransformContentControl)d;
        var value = (double)e.NewValue;
        var height = transformContentControl.ActualHeight;
        var scale = transformContentControl.ScaleY;
        transformContentControl._translateTransform.Y = value * height * scale;
    }
    #endregion

    #region Dependency property for specifying rotation angle
    /// <summary>
    /// Dependency property for specifying rotation angle
    /// </summary>
    /// <remarks>
    /// No rotation at 0
    /// 360° rotation at 1
    /// Rotation of -1 to -360°.
    /// </remarks>
    public static readonly DependencyProperty RotateAngleProperty
        = DependencyProperty.Register(
            nameof(RotateAngle),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onRotateAnglePropertyChanged)
        );

    /// <summary>
    /// CLR property for RotateAngleProperty, a dependency property for specifying the angle of rotation
    /// </summary>
    /// <remarks>
    /// No rotation at 0
    /// 360° rotation at 1
    /// Rotation of -1 to -360°.
    /// </remarks>
    public double RotateAngle
    {
        get => (double)GetValue(RotateAngleProperty);
        set => SetValue(RotateAngleProperty, value);
    }

    private static void onRotateAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var transformContentControl = (TransformContentControl)d;
        transformContentControl._rotateTransform.Angle = (double)e.NewValue * MaxAngle;
    }
    #endregion

    #region Dependency property for specifying the magnification factor in the x-direction
    /// <summary>
    /// Dependency property for specifying the magnification factor in the x-direction
    /// </summary>
    public static readonly DependencyProperty ScaleXProperty
        = DependencyProperty.Register(
            nameof(ScaleX),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(DefaultSize, onScaleXPropertyChanged)
        );

    /// <summary>
    /// CLR property for ScaleXProperty, a dependency property for specifying the magnification factor in the X direction
    /// </summary>
    public double ScaleX
    {
        get => (double)GetValue(ScaleXProperty);
        set => SetValue(ScaleXProperty, value);
    }

    private static void onScaleXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var transformContentControl = (TransformContentControl)d;
        transformContentControl._scaleTransform.ScaleX = (double)e.NewValue;
    }
    #endregion

    #region Dependency property for specifying the magnification factor in the Y direction
    /// <summary>
    /// Dependency property for specifying the magnification factor in the Y direction
    /// </summary>
    public static readonly DependencyProperty ScaleYProperty
        = DependencyProperty.Register(
            nameof(ScaleY),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(DefaultSize, onScaleYPropertyChanged)
        );

    /// <summary>
    /// CLR property for ScaleYProperty, a dependency property for specifying the magnification factor in the Y direction
    /// </summary>
    public double ScaleY
    {
        get => (double)GetValue(ScaleYProperty);
        set => SetValue(ScaleYProperty, value);
    }

    private static void onScaleYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var transformContentControl = (TransformContentControl)d;
        transformContentControl._scaleTransform.ScaleY = (double)e.NewValue;
    }
    #endregion
}