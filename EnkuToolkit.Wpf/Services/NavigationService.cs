namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows.Navigation;
using System;
using System.Windows;

/// <summary>
/// Application.Current.MainWindowがNavigationWindowの場合使用可能な、
/// ViewModelから画面遷移を行うためのViewServiceの実装
/// </summary>
public class NavigationService : INavigationService
{
    private NavigationWindow MainNavigationWindow => (NavigationWindow)Application.Current.MainWindow;

    /// <summary>
    /// プロジェクトのルートフォルダをベースURIとして遷移先のURIを指定できる画面遷移用メソッド
    /// </summary>
    /// <param name="uriStr">遷移先ページへの相対URI</param>
    /// <param name="extraData">遷移先に渡したいデータ</param>
    /// <returns>画面遷移がキャンセルされたらfalse、キャンセルされなければtrueを返す</returns>
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
    /// 遷移先のURIを指定して画面遷移を行うためのメソッド
    /// </summary>
    /// <param name="uri">遷移先ページへURI</param>
    /// <param name="extraData">遷移先に渡したいデータ</param>
    /// <returns>画面遷移がキャンセルされたらfalse、キャンセルされなければtrueを返す</returns>
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
    /// 履歴を元に表示されているページを進めるためのメソッド
    /// </summary>
    public void GoForward()
    {
        this.MainNavigationWindow.GoForward();
    }

    /// <summary>
    /// 履歴を元に表示されているページを戻すためのメソッド
    /// </summary>
    public void GoBack()
    {
        this.MainNavigationWindow.GoBack();
    }

    /// <summary>
    /// 表示されているページの再読み込みを行うためのメソッド
    /// </summary>
    public void Refresh()
    {
        this.MainNavigationWindow.Refresh();
    }

    /// <summary>
    /// 履歴から前回表示されていたページを削除するためのメソッド
    /// </summary>
    public void RemoveBackEntry()
    {
        this.MainNavigationWindow.RemoveBackEntry();
    }

    /// <summary>
    /// 画面遷移を中断するためのメソッド
    /// </summary>
    public void StopLoading()
    {
        this.MainNavigationWindow.StopLoading();
    }
}