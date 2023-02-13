namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// 各変形用プロパティを0~1までの値で操作できるようにした
/// 変形用プロパティを持つContentControl
/// </summary>
public class NormalizedTransformContentControl : ContentControl
{
    private readonly ScaleTransform _scaleTransform = new();
    private readonly RotateTransform _rotateTransform = new();
    private readonly TranslateTransform _translateTransform = new();
    private const double MAX_ANGLE = 360;
    private const double DEFAULT_CENTER = 0.5;
    private const double DEFAULT_SIZE = 1;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public NormalizedTransformContentControl()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scaleTransform);
        transformGroup.Children.Add(this._translateTransform);
        transformGroup.Children.Add(this._rotateTransform);
        this.RenderTransform = transformGroup;
        this.SizeChanged += onSizeChanged;
    }

    private void onSizeChanged(object sender, RoutedEventArgs e)
    {
        this._translateTransform.X = this.TranslateX * this.ActualWidth;
        this._translateTransform.Y = this.TranslateY * this.ActualHeight;
        this._rotateTransform.CenterX = this.RotateCenterX * this.ActualWidth;
        this._rotateTransform.CenterY = this.RotateCenterY * this.ActualHeight;
        this._scaleTransform.CenterX = this.ScaleCenterX * this.ActualWidth;
        this._scaleTransform.CenterY = this.ScaleCenterY * this.ActualHeight;
    }

    #region X方向への移動用依存関係プロパティ
    /// <summary>
    /// X方向への移動用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TranslateXProperty
        = DependencyProperty.Register(
            nameof(TranslateX),
            typeof(double),
            typeof(NormalizedTransformContentControl),
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
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._translateTransform.X = (double)e.NewValue * ntcc.ActualWidth;
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
            typeof(NormalizedTransformContentControl),
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
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._translateTransform.Y = (double)e.NewValue * ntcc.ActualHeight;
    }
    #endregion

    #region 回転の中心点とするx座標を指定するための依存関係プロパティ
    /// <summary>
    /// 回転の中心点とするx座標を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty RotateCenterXProperty
        = DependencyProperty.Register(
            nameof(RotateCenterX),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_CENTER, onRotateCenterXPropertyChanged)
        );

    /// <summary>
    /// RotateCenterXProperty依存関係プロパティに対応するCLRプロパティ
    /// </summary>
    public double RotateCenterX
    {
        get => (double)this.GetValue(RotateCenterXProperty);
        set => this.SetValue(RotateCenterXProperty, value);
    }

    private static void onRotateCenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._rotateTransform.CenterX = (double)e.NewValue * ntcc.ActualWidth;
    }
    #endregion

    #region 回転の中心点とするY座標を指定するための依存関係プロパティ
    /// <summary>
    /// 回転の中心点とするY座標を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty RotateCenterYProperty
        = DependencyProperty.Register(
            nameof(RotateCenterY),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_CENTER, onRotateCenterYPropertyChanged)
        );

    /// <summary>
    /// RotateCenterYProperty依存関係プロパティに対応するCLRプロパティ
    /// </summary>
    public double RotateCenterY
    {
        get => (double)this.GetValue(RotateCenterYProperty);
        set => this.SetValue(RotateCenterYProperty, value);
    }

    private static void onRotateCenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._rotateTransform.CenterY = (double)e.NewValue * ntcc.ActualHeight;
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
            typeof(NormalizedTransformContentControl),
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
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._rotateTransform.Angle = (double)e.NewValue * MAX_ANGLE;
    }
    #endregion

    #region X方向への拡大での中心点定用依存関係プロパティ
    /// <summary>
    /// X方向への拡大での中心点定用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleCenterXProperty
        = DependencyProperty.Register(
            nameof(ScaleCenterX),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_CENTER, onScaleCenterXPropertyChanged)
        );

    /// <summary>
    /// ScaleCenterXProperty用依存関係プロパティ
    /// </summary>
    public double ScaleCenterX
    {
        get => (double)this.GetValue(ScaleCenterXProperty);
        set => this.SetValue(ScaleCenterXProperty, value);
    }

    private static void onScaleCenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._scaleTransform.CenterX = (double)e.NewValue * ntcc.ActualWidth;
    }
    #endregion

    #region Y方向への拡大での中心点定用依存関係プロパティ
    /// <summary>
    /// Y方向への拡大での中心点定用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleCenterYProperty
        = DependencyProperty.Register(
            nameof(ScaleCenterY),
            typeof(double),
            typeof(NormalizedTransformContentControl),
            new PropertyMetadata(DEFAULT_CENTER, onScaleCenterYPropertyChanged)
        );

    /// <summary>
    /// ScaleCenterYProperty用依存関係プロパティ
    /// </summary>
    public double ScaleCenterY
    {
        get => (double)this.GetValue(ScaleCenterYProperty);
        set => this.SetValue(ScaleCenterYProperty, value);
    }

    private static void onScaleCenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._scaleTransform.CenterY = (double)e.NewValue * ntcc.ActualHeight;
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
            typeof(NormalizedTransformContentControl),
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
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._scaleTransform.ScaleX = (double)e.NewValue;
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
            typeof(NormalizedTransformContentControl),
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
        var ntcc = (NormalizedTransformContentControl)d;
        ntcc._scaleTransform.ScaleY = (double)e.NewValue;
    }
    #endregion
}