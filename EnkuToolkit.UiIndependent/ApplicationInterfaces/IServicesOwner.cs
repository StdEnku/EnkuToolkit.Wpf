namespace EnkuToolkit.UiIndependent.ApplicationInterfaces;

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