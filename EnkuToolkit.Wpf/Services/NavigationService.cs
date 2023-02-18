namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows.Navigation;
using System;
using System.Windows;

/// <summary>
/// AppクラスのMainWindowがNavigationWindowの場合使用可能な
/// ViewModelから画面遷移を実行するためのViewService
/// </summary>
public class NavigationService : INavigationService
{
    private NavigationWindow MainNavigationWindow => (NavigationWindow)Application.Current.MainWindow;

    /// <summary>
    /// プロジェクトのルートをベースとした相対URIで遷移先を指定可能な画面遷移用メソッド
    /// </summary>
    /// <param name="uriStr">プロジェクトのルートをベースとした場合の遷移先の相対uri</param>
    /// <param name="extraData">遷移先のページへ渡したいデータ</param>
    /// <returns>ナビゲーションがキャンセルされない場合はtrueそれ以外の場合はfalse</returns>
    public bool NavigateRootBase(string uriStr, object? extraData = null)
    {
        var baseUri = new Uri("pack://application:,,,/");
        var uri = new Uri(baseUri, uriStr);

        if (extraData is null)
        {
            return this.MainNavigationWindow.Navigate(uri);
        }
        else
        {
            return this.MainNavigationWindow.Navigate(uri, extraData);
        }
    }

    /// <summary>
    /// URIで遷移先を指定可能な画面遷移用メソッド
    /// </summary>
    /// <param name="uriStr">遷移先のuri</param>
    /// <param name="extraData">遷移先のページへ渡したいデータ</param>
    /// <returns>ナビゲーションがキャンセルされない場合はtrueそれ以外の場合はfalse</returns>
    public bool Navigate(Uri uri, object? extraData = null)
    {
        if (extraData is null)
        {
            return this.MainNavigationWindow.Navigate(uri);
        }
        else
        {
            return this.MainNavigationWindow.Navigate(uri, extraData);
        }
    }

    /// <summary>
    /// ページを進めるためのメソッド
    /// </summary>
    public void GoForward()
    {
        this.MainNavigationWindow.GoForward();
    }

    /// <summary>
    /// ページを戻すためのメソッド
    /// </summary>
    public void GoBack()
    {
        this.MainNavigationWindow.GoBack();
    }

    /// <summary>
    /// ページの再読み込みを行うためのメソッド
    /// </summary>
    public void Refresh()
    {
        this.MainNavigationWindow.Refresh();
    }

    /// <summary>
    /// "戻る" 履歴から最新の履歴項目を削除するためのメソッド
    /// </summary>
    public void RemoveBackEntry()
    {
        this.MainNavigationWindow.RemoveBackEntry();
    }

    /// <summary>
    /// 現在のナビゲーション要求に対応するコンテンツのダウンロードを中止するためのメソッド
    /// </summary>
    public void StopLoading()
    {
        this.MainNavigationWindow.StopLoading();
    }
}