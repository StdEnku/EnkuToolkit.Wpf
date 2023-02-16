namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// AppクラスのMainWindowがNavigationWindowの場合使用可能な
/// ViewModelから画面遷移を実行するためのViewService用のインターフェース
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// 画面遷移用メソッド
    /// </summary>
    /// <param name="uriStr">uri</param>
    /// <param name="extraData">遷移先のページへ渡したいデータ</param>
    /// <returns>ナビゲーションがキャンセルされない場合はtrueそれ以外の場合はfalse</returns>
    bool Navigate(string uriStr, object? extraData = null);

    /// <summary>
    /// ページを進めるためのメソッド
    /// </summary>
    void GoForward();

    /// <summary>
    /// ページを戻すためのメソッド
    /// </summary>
    void GoBack();
}