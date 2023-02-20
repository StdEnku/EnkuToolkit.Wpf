namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// 変形用プロパティを持つContentControl
/// </summary>
public class TransformContentControl : ContentControl
{
    private readonly ScaleTransform _scaleTransform = new();
    private readonly RotateTransform _rotateTransform = new();
    private readonly TranslateTransform _translateTransform = new();
    private const double DEFAULT_SIZE = 1;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public TransformContentControl()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scaleTransform);
        transformGroup.Children.Add(this._rotateTransform);
        transformGroup.Children.Add(this._translateTransform);
        this.RenderTransform = transformGroup;
        this.RenderTransformOrigin = new Point(0.5, 0.5);
    }

    #region X方向への移動用依存関係プロパティ
    /// <summary>
    /// X方向への移動用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TranslateXProperty
        = DependencyProperty.Register(
            nameof(TranslateX),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onTranslateXPropertyChanged)
        );

    /// <summary>
    /// TranslateXProperty依存関係プロパティに対応するCLRプロパティ
    /// </summary>
    public double TranslateX
    {
        get => (double)this.GetValue(TranslateXProperty);
        set => this.SetValue(TranslateXProperty, value);
    }

    private static void onTranslateXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._translateTransform.X = (double)e.NewValue;
    }
    #endregion

    #region Y方向への移動用依存関係プロパティ
    /// <summary>
    /// Y方向への移動用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TranslateYProperty
        = DependencyProperty.Register(
            nameof(TranslateY),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onTranslateYPropertyChanged)
        );

    /// <summary>
    /// TranslateYProperty依存関係プロパティに対応するCLRプロパティ
    /// </summary>
    public double TranslateY
    {
        get => (double)this.GetValue(TranslateYProperty);
        set => this.SetValue(TranslateYProperty, value);
    }

    private static void onTranslateYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._translateTransform.Y = (double)e.NewValue;
    }
    #endregion

    #region 回転の角度を指定するための依存関係プロパティ
    /// <summary>
    /// 回転の角度を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty RotateAngleProperty
        = DependencyProperty.Register(
            nameof(RotateAngle),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onRotateAnglePropertyChanged)
        );

    /// <summary>
    /// RotateAngleProperty依存関係プロパティに対応するCLRプロパティ
    /// </summary>
    public double RotateAngle
    {
        get => (double)this.GetValue(RotateAngleProperty);
        set => this.SetValue(RotateAngleProperty, value);
    }

    private static void onRotateAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._rotateTransform.Angle = (double)e.NewValue;
    }
    #endregion

    #region X方向への拡大率指定用プロパティ
    /// <summary>
    /// X方向への拡大率指定用プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleXProperty
        = DependencyProperty.Register(
            nameof(ScaleX),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(DEFAULT_SIZE, onScaleXPropertyChanged)
        );

    /// <summary>
    /// ScaleXProperty用依存関係プロパティ
    /// </summary>
    public double ScaleX
    {
        get => (double)this.GetValue(ScaleXProperty);
        set => this.SetValue(ScaleXProperty, value);
    }

    private static void onScaleXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._scaleTransform.ScaleX = (double)e.NewValue;
    }
    #endregion

    #region Y方向への拡大率指定用プロパティ
    /// <summary>
    /// Y方向への拡大率指定用プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleYProperty
        = DependencyProperty.Register(
            nameof(ScaleY),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(DEFAULT_SIZE, onScaleYPropertyChanged)
        );

    /// <summary>
    /// ScaleYProperty用依存関係プロパティ
    /// </summary>
    public double ScaleY
    {
        get => (double)this.GetValue(ScaleYProperty);
        set => this.SetValue(ScaleYProperty, value);
    }

    private static void onScaleYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._scaleTransform.ScaleY = (double)e.NewValue;
    }
    #endregion
}