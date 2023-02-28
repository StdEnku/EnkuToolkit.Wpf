namespace EnkuToolkit.Wpf.MarkupExtensions;

using System;
using System.Windows;
using System.Windows.Markup;
using System.Diagnostics;

/// <summary>
/// DIコンテナのServiceProviderを取得するためのプロパティを持つインターフェース
/// DIコンテナの初期設定を行うApplicationの子クラスにて実装することを想定している。
/// </summary>
public interface IServicesOwner
{
    /// <summary>
    /// DIコンテナのServiceProviderを取得するためのプロパティ
    /// </summary>
    IServiceProvider Services { get; }
}

/// <summary>
/// DIコンテナからViewModelを取得するためのマークアップ拡張
/// WPFプロジェクト作成時に生成されるAppクラスがIServicesOwnerを実装しているときのみ使用可能
/// </summary>
[MarkupExtensionReturnType(typeof(object))]
public class ViewModelProviderExtension : MarkupExtension
{
    private Type _type;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type">ViewModelの型を示すTypeオブジェクト</param>
    public ViewModelProviderExtension(Type type)
    {
        this._type = type;
    }

    /// <summary>
    /// 本マークアップ拡張の戻り値を作成するメソッド
    /// </summary>
    /// <param name="serviceProvider">マークアップ拡張機能のサービスを提供できるサービスプロバイダーのヘルパー</param>
    /// <returns>DIコンテナから生成されたViewModelオブジェクト</returns>
    /// <exception cref="InvalidOperationException">
    /// DIコンテナでViewModelを生成できなかった場合に投げられる例外
    /// </exception>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        var servicesOwner = (IServicesOwner)Application.Current;
        var services = servicesOwner.Services;

        Debug.Assert(this._type is not null);
        var result = services.GetService(this._type);

        if (result is null)
            throw new InvalidOperationException($"DI container could not generate type {this._type.FullName}.");

        return result;
    }
}