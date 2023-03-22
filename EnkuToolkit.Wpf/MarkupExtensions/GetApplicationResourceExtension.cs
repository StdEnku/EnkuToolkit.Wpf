namespace EnkuToolkit.Wpf.MarkupExtensions;

using System;
using System.Windows;
using System.Windows.Markup;

/// <summary>
/// Applicationクラス内のリソースを取得するためのマークアップ拡張
/// </summary>
public class GetApplicationResourceExtension : MarkupExtension
{
    private Object _key;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="key">検索したいリソースのキー</param>
    public GetApplicationResourceExtension(Object key)
        => this._key = key;

    /// <summary>
    /// 本マークアップ拡張の戻り値を作成するメソッド
    /// </summary>
    /// <returns>発見されたリソース</returns>
    public override object? ProvideValue(IServiceProvider serviceProvider)
        => Application.Current.FindResource(this._key);
}