namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows;

/// <summary>
/// ViewModelからMessageBoxを操作可能にするためのViewServiceの実装
/// </summary>
public class MessageBoxService : IMessageBoxService
{
    /// <summary>
    /// 選択肢のないOKボタンのみのメッセージボックスを表示するためのメソッド
    /// </summary>
    /// <param name="message">メッセージのテキスト</param>
    /// <param name="title">タイトルのテキスト</param>
    public void ShowOk(string message, string? title = null)
    {
        MessageBox.Show(message, title ?? string.Empty);
    }

    /// <summary>
    /// YesとNoボタンを持つメッセージボックスを表示するためのメソッド
    /// </summary>
    /// <param name="message">メッセージのテキスト</param>
    /// <param name="title">タイトルのテキスト</param>
    /// <returns>ユーザーがYesボタンを押したらtrueを返し、Noを押したらfalseを返す</returns>
    public bool ShowYesNo(string message, string? title = null)
    {
        var button = MessageBoxButton.YesNo;
        var response = MessageBox.Show(message, title ?? string.Empty, button);
        return response == MessageBoxResult.Yes ? true : false;
    }
}