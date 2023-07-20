namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Attributes;
using EnkuToolkit.UiIndependent.Navigation;
using EnkuToolkit.UiIndependent.Services;

[DiRegister]
public partial class MainPageViewModel : ObservableObject, INavigationAware
{
    private INavigationService _navigationService;

    public MainPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void Clicked()
    {
        _navigationService.NavigateDi("Sandbox.Views.SubPage");
    }

    public void OnNavigatedFrom(object? param, NavigationMode navigationMode)
    {
        System.Diagnostics.Debug.WriteLine("-------------------------------------");
        System.Diagnostics.Debug.WriteLine("MainPageViewModel : OnNavigatedFrom");
        System.Diagnostics.Debug.WriteLine($"param : {param ?? "null"}");
        System.Diagnostics.Debug.WriteLine($"navigationMode : {navigationMode}");
    }

    public void OnNavigatedTo(object? param, NavigationMode navigationMode)
    {
        System.Diagnostics.Debug.WriteLine("-------------------------------------");
        System.Diagnostics.Debug.WriteLine("MainPageViewModel : OnNavigatedTo");
        System.Diagnostics.Debug.WriteLine($"param : {param ?? "null"}");
        System.Diagnostics.Debug.WriteLine($"navigationMode : {navigationMode}");
    }

    public bool OnNavigatingFrom(object? param, NavigationMode navigationMode)
    {
        System.Diagnostics.Debug.WriteLine("-------------------------------------");
        System.Diagnostics.Debug.WriteLine("MainPageViewModel : OnNavigatingFrom");
        System.Diagnostics.Debug.WriteLine($"param : {param ?? "null"}");
        System.Diagnostics.Debug.WriteLine($"navigationMode : {navigationMode}");
        return false;
    }
}