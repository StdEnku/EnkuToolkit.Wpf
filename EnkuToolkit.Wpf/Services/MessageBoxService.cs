namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows;

/// <summary>
/// メッセージボックスの操作をViewModelから行えるようにするためのViewServce
/// </summary>
public class MessageBoxService : IMessageBoxService
{
    /// <summary>
    /// 選択肢のないメッセージボックス表示用メソッド
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="title">タイトル</param>
    public void ShowOk(string message, string? title = null)
    {
        MessageBox.Show(message, title ?? string.Empty);
    }

    /// <summary>
    /// 選択肢がYesまたはNoのメッセージボックス表示用メソッド
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="title">タイトル</param>
    /// <returns>ユーザーがYesを選択したらtrue。Noを選択したらfalseを返す</returns>
    public bool ShowYesNo(string message, string? title = null)
    {
        var button = MessageBoxButton.YesNo;
        var response = MessageBox.Show(message, title ?? string.Empty, button);
        return response == MessageBoxResult.Yes ? true : false;
    }
}