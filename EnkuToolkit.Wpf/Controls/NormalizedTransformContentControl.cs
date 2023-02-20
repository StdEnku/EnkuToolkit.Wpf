namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// Each deformation property can be manipulated with a value from 0 to 1.
/// ContentControl with properties for deformation
/// </summary>
public class NormalizedTransformContentControl : ContentControl
{
    private readonly ScaleTransform _scaleTransform = new();
    private readonly RotateTransform _rotateTransform = new();
    private readonly TranslateTransform _translateTransform = new();
    private const double MAX_ANGLE = 360;
    private const double DEFAULT_SIZE = 1;

    /// <summary>
    /// constructor
    /// </summary>
    public NormalizedTransformContentControl()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scaleTransform);
        transformGroup.Children.Add(this._rotateTransform);
        transformGroup.Children.Add(this._translateTransform);
        this.RenderTransform = transformGroup;
        this.RenderTransformOrigin = new Point(0.5, 0.5);
        this.SizeChanged += onSizeChanged;
    }

    private void onSizeChanged(object sender, RoutedEventArgs e)
    {
        this._translateTransform.X = this.TranslateX * this.ActualWidth;
        this._translateTransform.Y = this.TranslateY * this.ActualHeight;
    }

    #region Dependency property for movement in X direction
    /// <summary>
    /// Dependency property for movement in X direction
    /// </summary>
    public static readonly DependencyProperty TranslateXProperty
        = DependencyProperty.Register(
            nameof(TranslateX),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(default(double), onTranslateXPropertyChanged)
        );

    /// <summary>
    /// CLR property corresponding to the TranslateXProperty dependency property
    /// </summary>
    public double TranslateX
    {
        get => (double)this.GetValue(TranslateXProperty);
        set => this.SetValue(TranslateXProperty, value);
    }

    private static void onTranslateXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._translateTransform.X = (double)e.NewValue * ntcc.ActualWidth;
    }
    #endregion

    #region Dependency property for movement in Y direction
    /// <summary>
    /// Dependency property for movement in Y direction
    /// </summary>
    public static readonly DependencyProperty TranslateYProperty
        = DependencyProperty.Register(
            nameof(TranslateY),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(default(double), onTranslateYPropertyChanged)
        );

    /// <summary>
    /// CLR property corresponding to the TranslateYProperty dependency property
    /// </summary>
    public double TranslateY
    {
        get => (double)this.GetValue(TranslateYProperty);
        set => this.SetValue(TranslateYProperty, value);
    }

    private static void onTranslateYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._translateTransform.Y = (double)e.NewValue * ntcc.ActualHeight;
    }
    #endregion

    #region Dependency property for specifying the angle of rotation
    /// <summary>
    /// Dependency property for specifying the angle of rotation
    /// </summary>
    public static readonly DependencyProperty RotateAngleProperty
        = DependencyProperty.Register(
            nameof(RotateAngle),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(default(double), onRotateAnglePropertyChanged)
        );

    /// <summary>
    /// CLR property corresponding to the RotateAngleProperty dependency property
    /// </summary>
    public double RotateAngle
    {
        get => (double)this.GetValue(RotateAngleProperty);
        set => this.SetValue(RotateAngleProperty, value);
    }

    private static void onRotateAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._rotateTransform.Angle = (double)e.NewValue * MAX_ANGLE;
    }
    #endregion

    #region Property for specifying the magnification rate in the x direction
    /// <summary>
    /// Property for specifying the magnification rate in the x direction
    /// </summary>
    public static readonly DependencyProperty ScaleXProperty
        = DependencyProperty.Register(
            nameof(ScaleX),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_SIZE, onScaleXPropertyChanged)
        );

    /// <summary>
    /// CLR properties for ScaleXProperty
    /// </summary>
    public double ScaleX
    {
        get => (double)this.GetValue(ScaleXProperty);
        set => this.SetValue(ScaleXProperty, value);
    }

    private static void onScaleXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._scaleTransform.ScaleX = (double)e.NewValue;
    }
    #endregion

    #region Property for specifying the magnification rate in the Y direction
    /// <summary>
    /// Property for specifying the magnification rate in the Y direction
    /// </summary>
    public static readonly DependencyProperty ScaleYProperty
        = DependencyProperty.Register(
            nameof(ScaleY),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_SIZE, onScaleYPropertyChanged)
        );

    /// <summary>
    /// CLR properties for ScaleYProperty
    /// </summary>
    public double ScaleY
    {
        get => (double)this.GetValue(ScaleYProperty);
        set => this.SetValue(ScaleYProperty, value);
    }

    private static void onScaleYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._scaleTransform.ScaleY = (double)e.NewValue;
    }
    #endregion
}