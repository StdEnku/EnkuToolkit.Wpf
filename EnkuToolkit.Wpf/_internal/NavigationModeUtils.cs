namespace EnkuToolkit.Wpf._internal;

using SystemNavigationMode = System.Windows.Navigation.NavigationMode;
using EnkuNavigationMode = UiIndependent.Navigation.NavigationMode;

internal static class NavigationModeUtils
{
    public static EnkuNavigationMode EnkuNaviMode2WpfNaviMode(SystemNavigationMode navigationMode)
    {
        var result = navigationMode == SystemNavigationMode.New ? EnkuNavigationMode.New :
                     navigationMode == SystemNavigationMode.Forward ? EnkuNavigationMode.Forward :
                     navigationMode == SystemNavigationMode.Back ? EnkuNavigationMode.Back :
                     EnkuNavigationMode.Refresh;

        return result;
    }
}