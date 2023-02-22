namespace EnkuToolkit.UiIndependent.Services;

using System.Collections;


/// <summary>
/// Application.PropertiesプロパティをViewModelから操作可能にするためのViewService用のインターフェース
/// </summary>
public interface IApplicationPropertyiesService
{
    /// <summary>
    /// Application.Propertiesプロパティを取得するためのプロパティ
    /// </summary>
    IDictionary Properties { get; }
}