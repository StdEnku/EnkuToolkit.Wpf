namespace EnkuToolkit.Wpf.Behaviors;

using EnkuToolkit.UiIndependent.ViewModelInterfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;

/// <summary>
/// 画面遷移時に読み込まれたPageオブジェクトのDataContextがINavigatedPraramReceiveを実装している場合
/// DataContextのNavigatedメソッドを呼び出し元の画面からパラメータを送信できるようにするためのビヘイビア。
/// </summary>
public class NavigatedParamSendBehavior
{
    #region NavigationWindowに添付できる添付プロパティ
    /// <summary>
    /// NavigationWindow用のパラメータの送信を行うか指定するための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty IsSendFromNavigationWindowProperty
        = DependencyProperty.RegisterAttached(
            "IsSendFromNavigationWindow",
            typeof(bool),
            typeof(NavigatedParamSendBehavior),
            new PropertyMetadata(false, onIsSendChanged)
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
            new PropertyMetadata(false, onIsSendChanged)
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
    #endregion

    private static void onIsSendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var value = (bool)e.NewValue;

        if (d is NavigationWindow nWindow)
        {
            if (!value) return;
            nWindow.NavigationService.Navigated += onTargetNavigated;
        }
        else if (d is Frame frame)
        {
            if (!value) return;
            frame.NavigationService.Navigated += onTargetNavigated;
        }
        else
        {
            Debug.Assert(false, "The only attached properties that use this method are Frame or NaigationWindow, so it should not pass through here.");
        }
    }

    private static void onTargetNavigated(object sender, NavigationEventArgs e)
    {
        var nextPage = (Page)e.Content;
        var extraData = e.ExtraData;

        if (nextPage.DataContext is INavigatedParamReceive dc)
            dc.Navigated(extraData);
    }
}