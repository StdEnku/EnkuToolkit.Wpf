namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// ViewModelからMessageBoxを操作可能にするためのViewService用インターフェース
/// </summary>
public interface IMessageBoxService
{
    /// <summary>
    /// 選択肢のないOKボタンのみのメッセージボックスを表示するためのメソッド
    /// </summary>
    /// <param name="message">メッセージのテキスト</param>
    /// <param name="title">タイトルのテキスト</param>
    void ShowOk(string message, string? title = null);

    /// <summary>
    /// YesとNoボタンを持つメッセージボックスを表示するためのメソッド
    /// </summary>
    /// <param name="message">メッセージのテキスト</param>
    /// <param name="title">タイトルのテキスト</param>
    /// <returns>ユーザーがYesボタンを押したらtrueを返し、Noを押したらfalseを返す</returns>
    bool ShowYesNo(string message, string? title = null);
}