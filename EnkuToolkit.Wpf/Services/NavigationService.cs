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
    /// 画面遷移用メソッド
    /// </summary>
    /// <param name="uriStr">uri</param>
    /// <param name="extraData">遷移先のページへ渡したいデータ</param>
    /// <returns>ナビゲーションがキャンセルされない場合はtrueそれ以外の場合はfalse</returns>
    public bool Navigate(string uriStr, object? extraData = null)
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
}