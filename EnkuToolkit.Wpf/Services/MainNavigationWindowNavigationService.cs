namespace EnkuToolkit.Wpf.Services;

using System.Windows;
using System.Windows.Navigation;

/// <summary>
/// Application.Current.MainWindowがNavigationWindowの場合のみ使用可能なNavigationService
/// </summary>
public class MainNavigationWindowNavigationService : AbstractNavigationService
{
    /// <summary>
    /// Application.Current.MainWindowをNavigationWindowにキャストしてそのNavigationServcieプロパティを返すプロパティ
    /// </summary>
    protected override NavigationService TargetNavigationService
    {
        get
        {
            var nw = (NavigationWindow)Application.Current.MainWindow;
            return nw.NavigationService;
        }
    }
}
