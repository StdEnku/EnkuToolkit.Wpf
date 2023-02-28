namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows.Navigation;
using System;

/// <summary>
/// ViewModelから画面遷移を行うためのViewServiceの抽象クラス
/// 継承してカスタマイズする際はTargetNavigationServiceプロパティのゲッターをオーバーライドして
/// 画面遷移の対象となるFrameやNavigationWindowのNavigationServiceプロパティを返す処理を記してください。
/// </summary>
public abstract class AbstractNavigationService : INavigationService
{
    /// <summary>
    /// 画面遷移の対象となるFrameやNavigationWindowのNavigationServiceプロパティを返す処理を記したプロパティ
    /// </summary>
    protected abstract NavigationService TargetNavigationService { get; }

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
            return this.TargetNavigationService.Navigate(uri);
        }
        else
        {
            return this.TargetNavigationService.Navigate(uri, extraData);
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
            return this.TargetNavigationService.Navigate(uri);
        }
        else
        {
            return this.TargetNavigationService.Navigate(uri, extraData);
        }
    }

    /// <summary>
    /// 履歴を元に表示されているページを進めるためのメソッド
    /// </summary>
    public void GoForward()
    {
        this.TargetNavigationService.GoForward();
    }

    /// <summary>
    /// 履歴を元に表示されているページを戻すためのメソッド
    /// </summary>
    public void GoBack()
    {
        this.TargetNavigationService.GoBack();
    }

    /// <summary>
    /// 表示されているページの再読み込みを行うためのメソッド
    /// </summary>
    public void Refresh()
    {
        this.TargetNavigationService.Refresh();
    }

    /// <summary>
    /// 履歴から前回表示されていたページを削除するためのメソッド
    /// </summary>
    public void RemoveBackEntry()
    {
        this.TargetNavigationService.RemoveBackEntry();
    }

    /// <summary>
    /// 画面遷移を中断するためのメソッド
    /// </summary>
    public void StopLoading()
    {
        this.TargetNavigationService.StopLoading();
    }

    /// <summary>
    /// 履歴から前回表示されていたページの履歴をすべて削除するためのメソッド
    /// </summary>
    public void RemoveAllBackEntry()
    {
        var ns = this.TargetNavigationService;
        while (ns.CanGoBack)
        {
            ns.RemoveBackEntry();
        }
    }

    /// <summary>
    /// 画面遷移対象のFrameやNavigationWindowでGoBackメソッドが実行可能かを表すプロパティ
    /// </summary>
    public bool CanGoBack => this.TargetNavigationService.CanGoBack;

    /// <summary>
    /// 画面遷移対象のFrameやNavigationWindowでGoForwardメソッドが実行可能かを表すプロパティ
    /// </summary>
    public bool CanGoForward => this.TargetNavigationService.CanGoForward;
}