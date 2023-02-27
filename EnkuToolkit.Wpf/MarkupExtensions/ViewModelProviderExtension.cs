namespace EnkuToolkit.Wpf.MarkupExtensions;

using EnkuToolkit.UiIndependent.ApplicationInterfaces;
using System;
using System.Windows;
using System.Windows.Markup;
using System.Diagnostics;

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
    /// 派生クラスで実装された場合、このマークアップ拡張機能のターゲット プロパティの値として提供されるオブジェクトを返します。
    /// </summary>
    /// <param name="serviceProvider">マークアップ拡張機能のサービスを提供できるサービス プロバイダーのヘルパー。</param>
    /// <returns>拡張機能が適用されたプロパティで設定するオブジェクト値。</returns>
    /// <exception cref="InvalidOperationException">
    /// DIコンテナViewModelを生成できなかった場合に投げられる例外
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