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
    /// <returns>
    /// 発見されたリソースのオブジェクト
    /// 発見されなかった場合nullを返す
    /// </returns>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        object? result;

        try
        {
            result = Application.Current.FindResource(this._key);
        }
        catch (ResourceReferenceKeyNotFoundException)
        {
            result = null;
        }

        return result;
    }
}