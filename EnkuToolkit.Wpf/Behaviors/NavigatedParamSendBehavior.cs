namespace EnkuToolkit.Wpf.Behaviors;

using EnkuToolkit.UiIndependent.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

/// <summary>
/// 画面遷移時に読み込まれたPageオブジェクトのDataContextがINavigatedPraramReceiveを実装している場合
/// DataContextのNavigatedメソッドを呼び出し元の画面からパラメータを送信できるようにするためのビヘイビア。
/// </summary>
public class NavigatedParamSendBehavior
{
    private static void onTargetNavigated(object sender, NavigationEventArgs e)
    {
        var nextPage = (Page)e.Content;
        var extraData = e.ExtraData;

        if (nextPage.DataContext is INavigatedParamReceive dc)
            dc.Navigated(extraData);
    }

    #region NavigationWindowに添付できる添付プロパティ
    /// <summary>
    /// NavigationWindow用のパラメータの送信を行うか指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty IsSendFromNavigationWindowProperty
        = DependencyProperty.RegisterAttached(
            "IsSendFromNavigationWindow",
            typeof(bool),
            typeof(NavigatedParamSendBehavior),
            new PropertyMetadata(false, onIsSendFromNavigationWindowChanged)
        );

    /// <summary>
    /// IsSendFromNavigationWindowProperty用のセッター
    /// </summary>
    /// <param name="target">添付対象のNavigationWindowオブジェクト</param>
    /// <param name="value">
    /// パラメータの送信をするのならばTrueを指定
    /// しないのならばFalseを指定
    /// </param>
    public static void SetIsSendFromNavigationWindow(NavigationWindow target, bool value)
        => target.SetValue(IsSendFromNavigationWindowProperty, value);

    /// <summary>
    /// IsSendFromNavigationWindowProperty用のゲッター
    /// </summary>
    /// <param name="target">添付対象のNavigationWindowオブジェクト</param>
    /// <returns>
    /// パラメータの送信をするのならばTrueを返す。
    /// しないのならばFalseを返す。
    /// </returns>
    public static bool GetIsSendFromNavigationWindow(NavigationWindow target)
        => (bool)target.GetValue(IsSendFromNavigationWindowProperty);

    private static void onIsSendFromNavigationWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var value = (bool)e.NewValue;
        if (!value) return;

        var target = (NavigationWindow)d;
        target.NavigationService.Navigated += onTargetNavigated;
    }
    #endregion

    #region Frameに添付できる添付プロパティ
    /// <summary>
    /// Frame用のパラメータの送信を行うか指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty IsSendFromFrameProperty
        = DependencyProperty.RegisterAttached(
            "IsSendFromFrame",
            typeof(bool),
            typeof(NavigatedParamSendBehavior),
            new PropertyMetadata(false, onIsSendFromFrameChanged)
        );

    /// <summary>
    /// IsSendFromFrameProperty用のセッター
    /// </summary>
    /// <param name="target">添付対象のNavigationWindowオブジェクト</param>
    /// <param name="value">
    /// パラメータの送信をするのならばTrueを指定
    /// しないのならばFalseを指定
    /// </param>
    public static void SetIsSendFromFrame(Frame target, bool value)
        => target.SetValue(IsSendFromFrameProperty, value);

    /// <summary>
    /// IsSendFromFrameProperty用のゲッター
    /// </summary>
    /// <param name="target">添付対象のNavigationWindowオブジェクト</param>
    /// <returns>
    /// パラメータの送信をするのならばTrueを返す。
    /// しないのならばFalseを返す。
    /// </returns>
    public static bool GetIsSendFromFrame(Frame target)
        => (bool)target.GetValue(IsSendFromFrameProperty);

    private static void onIsSendFromFrameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var value = (bool)e.NewValue;
        if (!value) return;

        var target = (Frame)d;
        target.NavigationService.Navigated += onTargetNavigated;
    }
    #endregion
}