namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// AppクラスのMainWindowがNavigationWindowの場合使用可能な
/// ViewModelから画面遷移を実行するためのViewService用のインターフェース
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// プロジェクトのルートをベースとした相対URIで遷移先を指定可能な画面遷移用メソッド
    /// </summary>
    /// <param name="uriStr">プロジェクトのルートをベースとした場合の遷移先の相対uri</param>
    /// <param name="extraData">遷移先のページへ渡したいデータ</param>
    /// <returns>ナビゲーションがキャンセルされない場合はtrueそれ以外の場合はfalse</returns>
    bool NavigateRootBase(string uriStr, object? extraData = null);

    /// <summary>
    /// URIで遷移先を指定可能な画面遷移用メソッド
    /// </summary>
    /// <param name="uri">遷移先のuri</param>
    /// <param name="extraData">遷移先のページへ渡したいデータ</param>
    /// <returns>ナビゲーションがキャンセルされない場合はtrueそれ以外の場合はfalse</returns>
    bool Navigate(Uri uri, object? extraData = null);

    /// <summary>
    /// ページを進めるためのメソッド
    /// </summary>
    void GoForward();

    /// <summary>
    /// ページを戻すためのメソッド
    /// </summary>
    void GoBack();

    /// <summary>
    /// ページの再読み込みを行うためのメソッド
    /// </summary>
    void Refresh();

    /// <summary>
    /// "戻る" 履歴から最新の履歴項目を削除するためのメソッド
    /// </summary>
    void RemoveBackEntry();

    /// <summary>
    /// 現在のナビゲーション要求に対応するコンテンツのダウンロードを中止するためのメソッド
    /// </summary>
    void StopLoading();
}