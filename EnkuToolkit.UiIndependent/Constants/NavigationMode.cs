namespace EnkuToolkit.UiIndependent.Constants;

/// <summary>
/// WPFのアセンブリに依存しないViewModelで
/// System.Windows.Navigation.NavigationModeを使用するためのenum。
/// メンバはSystem.Windows.Navigation.NavigationModeと同じです。
/// </summary>
public enum NavigationMode
{
    /// <summary>
    /// "戻る"ナビゲーション履歴の一番最近のコンテンツへの移動。
    /// GoBack メソッドが呼び出されたときに行われます。
    /// </summary>
    Back,

    /// <summary>
    /// "進む"ナビゲーション履歴の一番最近のコンテンツへの移動。 
    /// GoForward メソッドが呼び出されたときに行われます。
    /// </summary>
    Forward,

    /// <summary>
    /// 新しいコンテンツへの移動。 
    /// Navigate メソッドが呼び出されたとき、
    /// またはSource プロパティが設定されたときに行われます。
    /// </summary>
    New,

    /// <summary>
    /// 現在のコンテンツの再読み込み。 
    /// Refresh メソッドが呼び出されたときに行われます。
    /// </summary>
    Reflesh,
}