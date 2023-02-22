namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using EnkuViewModelLocator.Wpf;
using System;

[ViewModel(ViewModelAttribute.ServiceLifeTime.Singleton)]
public partial class Page1ViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public Page1ViewModel(INavigationService navigationService)
    {
        this._navigationService = navigationService;
    }

    [RelayCommand]
    private void NextPage()
    {
        this._navigationService.NavigateRootBase("Views/Page2.xaml", "From Page1");
    }
}