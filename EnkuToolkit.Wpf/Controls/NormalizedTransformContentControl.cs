namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// 変形用プロパティを0~1までの値で指定可能なTransformContentControl
/// </summary>
public class NormalizedTransformContentControl : ContentControl
{
    private readonly ScaleTransform _scaleTransform = new();
    private readonly RotateTransform _rotateTransform = new();
    private readonly TranslateTransform _translateTransform = new();
    private const double MAX_ANGLE = 360;
    private const double DEFAULT_SIZE = 1;

    /// <summary>
    /// コンストラクタ
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

    #region X方向への移動を行うための依存関係プロパティ
    /// <summary>
    /// X方向への移動を行うための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TranslateXProperty
        = DependencyProperty.Register(
            nameof(TranslateX),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(default(double), onTranslateXPropertyChanged)
        );

    /// <summary>
    /// TranslateXPropertyに対応するCLRプロパティ
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

    #region Y方向への移動を行うための依存関係プロパティ
    /// <summary>
    /// Y方向への移動を行うための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TranslateYProperty
        = DependencyProperty.Register(
            nameof(TranslateY),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(default(double), onTranslateYPropertyChanged)
        );

    /// <summary>
    /// TranslateYPropertyに対応するCLRプロパティ
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

    #region 回転角を指定するための依存関係プロパティ
    /// <summary>
    /// 回転角を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty RotateAngleProperty
        = DependencyProperty.Register(
            nameof(RotateAngle),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(default(double), onRotateAnglePropertyChanged)
        );

    /// <summary>
    /// RotateAnglePropertyに対応するCLRプロパティ
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

    #region X方向への拡大倍率を指定するための依存関係プロパティ
    /// <summary>
    /// X方向への拡大倍率を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleXProperty
        = DependencyProperty.Register(
            nameof(ScaleX),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_SIZE, onScaleXPropertyChanged)
        );

    /// <summary>
    /// ScaleXPropertyに対応するCLRプロパティ
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

    #region Y方向への拡大倍率を指定するための依存関係プロパティ
    /// <summary>
    /// Y方向への拡大倍率を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleYProperty
        = DependencyProperty.Register(
            nameof(ScaleY),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_SIZE, onScaleYPropertyChanged)
        );

    /// <summary>
    /// ScaleYPropertyに対応するCLRプロパティ
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