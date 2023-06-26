namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;

[DiRegister]
public partial class SubPageViewModel : ObservableObject
{
    private INavigationService _navigationService;

    public SubPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}