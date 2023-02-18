namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// メッセージボックスの操作をViewModelから行えるようにするためのViewServce用インターフェース
/// </summary>
public interface IMessageBoxService
{
    /// <summary>
    /// 選択肢のないメッセージボックス表示用メソッド
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="title">タイトル</param>
    void ShowOk(string message, string? title = null);

    /// <summary>
    /// 選択肢がYesまたはNoのメッセージボックス表示用メソッド
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="title">タイトル</param>
    /// <returns>ユーザーがYesを選択したらtrue。Noを選択したらfalseを返す</returns>
    bool ShowYesNo(string message, string? title = null);
}