namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using EnkuToolkit.UiIndependent.Attributes;
using EnkuToolkit.UiIndependent.Navigation;
using EnkuToolkit.UiIndependent.Services;

[DiRegister]
public partial class SubPageViewModel : ObservableObject, INavigationAware
{
    private INavigationService _navigationService;

    public SubPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void OnNavigatedFrom(object? param, NavigationMode navigationMode)
    {
        System.Diagnostics.Debug.WriteLine("-------------------------------------");
        System.Diagnostics.Debug.WriteLine("SubPageViewModel : OnNavigatedFrom");
        System.Diagnostics.Debug.WriteLine($"param : {param ?? "null"}");
        System.Diagnostics.Debug.WriteLine($"navigationMode : {navigationMode}");
    }

    public void OnNavigatedTo(object? param, NavigationMode navigationMode)
    {
        System.Diagnostics.Debug.WriteLine("-------------------------------------");
        System.Diagnostics.Debug.WriteLine("SubPageViewModel : OnNavigatedTo");
        System.Diagnostics.Debug.WriteLine($"param : {param ?? "null"}");
        System.Diagnostics.Debug.WriteLine($"navigationMode : {navigationMode}");
    }

    public bool OnNavigatingFrom(object? param, NavigationMode navigationMode)
    {
        System.Diagnostics.Debug.WriteLine("-------------------------------------");
        System.Diagnostics.Debug.WriteLine("SubPageViewModel : OnNavigatingFrom");
        System.Diagnostics.Debug.WriteLine($"param : {param ?? "null"}");
        System.Diagnostics.Debug.WriteLine($"navigationMode : {navigationMode}");
        return false;
    }
}