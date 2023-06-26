namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;

[DiRegister]
public partial class MainPageViewModel : ObservableObject
{
    private INavigationService _navigationService;

    public MainPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void Clicked()
    {
        _navigationService.NavigateRootBase("Views/SubPage.xaml");
    }
}