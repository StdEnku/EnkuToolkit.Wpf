namespace EnkuToolkit.UiIndependent.ViewModelInterfaces;

using EnkuToolkit.UiIndependent.Constants;

/// <summary>
/// NavigatedParamSendBehaviorが適用されたFrameやNavigationWidnowでの画面遷移時に
/// パラメータの受け取りを行うためのNavigatedメソッドを含むインターフェース。
/// </summary>
public interface INavigatedParamReceive
{
    /// <summary>
    /// NavigatedParamSendBehaviorが適用されたFrameやNavigationWidnowでの画面遷移時に呼ばれるメソッド
    /// </summary>
    /// <param name="extraData">以前表示されていたPageやそのViewModelから渡されたパラメータ</param>
    /// <param name="mode">実行された画面遷移の種類</param>
    void Navigated(object? extraData, NavigationMode mode);
}