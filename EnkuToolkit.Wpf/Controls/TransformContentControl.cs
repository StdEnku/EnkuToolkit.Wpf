namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// 変形用プロパティを持つContentControl
/// </summary>
public class TransformContentControl : ContentControl
{
    private ScaleTransform _scaleTransform = new();
    private RotateTransform _rotateTransform = new();
    private TranslateTransform _translateTransform = new();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public TransformContentControl()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scaleTransform);
        transformGroup.Children.Add(this._translateTransform);
        transformGroup.Children.Add(this._rotateTransform);
        this.RenderTransform = transformGroup;
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

    #region 回転の中心点とするx座標を指定するための依存関係プロパティ
    /// <summary>
    /// 回転の中心点とするx座標を指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty RotateCenterXProperty
        = DependencyProperty.Register(
            nameof(RotateCenterX),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onRotateCenterXPropertyChanged)
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
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._rotateTransform.CenterX = (double)e.NewValue;
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
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onRotateCenterYPropertyChanged)
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
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._rotateTransform.CenterY = (double)e.NewValue;
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

    #region X方向への拡大での中心点定用依存関係プロパティ
    /// <summary>
    /// X方向への拡大での中心点定用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleCenterXProperty
        = DependencyProperty.Register(
            nameof(ScaleCenterX),
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onScaleCenterXPropertyChanged)
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
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._scaleTransform.CenterX = (double)e.NewValue;
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
            typeof(TransformContentControl),
            new PropertyMetadata(default(double), onScaleCenterYPropertyChanged)
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
        var movableContentControl = (TransformContentControl)d;
        movableContentControl._scaleTransform.CenterY = (double)e.NewValue;
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
            new PropertyMetadata(default(double), onScaleXPropertyChanged)
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
            new PropertyMetadata(default(double), onScaleYPropertyChanged)
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
        movableContentControl._scaleTransform.ScaleX = (double)e.NewValue;
    }
    #endregion
}