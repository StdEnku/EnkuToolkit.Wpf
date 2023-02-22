namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Collections;
using System.Windows;

/// <summary>
/// Application.PropertiesプロパティをViewModelから操作可能にするためのViewService
/// </summary>
public class ApplicationPropertyiesService : IApplicationPropertyiesService
{
    /// <summary>
    /// Application.Propertiesプロパティを取得するためのプロパティ
    /// </summary>
    public IDictionary Properties => Application.Current.Properties;
}