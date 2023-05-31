namespace Sandbox.ViewModels;

using EnkuToolkit.UiIndependent.Navigation;
using Sandbox.MvvmUtil;
using System.Diagnostics;

public class Page1ViewModel : ViewModelsBase, INavigationAware
{
    public void OnNavigated(object? param, NavigationMode navigationMode)
    {
        Debug.WriteLine(navigationMode.ToString());
        var p = (string?)param;
        Debug.WriteLine(p);
    }
}
