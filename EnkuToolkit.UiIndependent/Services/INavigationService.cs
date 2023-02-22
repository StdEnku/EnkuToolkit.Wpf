namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// Application.Current.MainWindowがNavigationWindowの場合使用可能な、
/// ViewModelから画面遷移を行うためのViewService用インターフェース
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// プロジェクトのルートフォルダをベースURIとして遷移先のURIを指定できる画面遷移用メソッド
    /// </summary>
    /// <param name="uriStr">遷移先ページへの相対URI</param>
    /// <param name="extraData">遷移先に渡したいデータ</param>
    /// <returns>画面遷移がキャンセルされたらfalse、キャンセルされなければtrueを返す</returns>
    bool NavigateRootBase(string uriStr, object? extraData = null);

    /// <summary>
    /// 遷移先のURIを指定して画面遷移を行うためのメソッド
    /// </summary>
    /// <param name="uri">遷移先ページへURI</param>
    /// <param name="extraData">遷移先に渡したいデータ</param>
    /// <returns>画面遷移がキャンセルされたらfalse、キャンセルされなければtrueを返す</returns>
    bool Navigate(Uri uri, object? extraData = null);

    /// <summary>
    /// 履歴を元に表示されているページを進めるためのメソッド
    /// </summary>
    void GoForward();

    /// <summary>
    /// 履歴を元に表示されているページを戻すためのメソッド
    /// </summary>
    void GoBack();

    /// <summary>
    /// 表示されているページの再読み込みを行うためのメソッド
    /// </summary>
    void Refresh();

    /// <summary>
    /// 履歴から前回表示されていたページを削除するためのメソッド
    /// </summary>
    void RemoveBackEntry();

    /// <summary>
    /// 画面遷移を中断するためのメソッド
    /// </summary>
    void StopLoading();

    /// <summary>
    /// ナビゲーション履歴をすべて削除するためのメソッド
    /// </summary>
    void RemoveAllHistory();

    /// <summary>
    /// 画面遷移対象のFrameやNavigationWindowでGoBackメソッドが実行可能かを表すプロパティ
    /// </summary>
    bool CanGoBack { get; }

    /// <summary>
    /// 画面遷移対象のFrameやNavigationWindowでGoForwardメソッドが実行可能かを表すプロパティ
    /// </summary>
    bool CanGoForward { get; }
}