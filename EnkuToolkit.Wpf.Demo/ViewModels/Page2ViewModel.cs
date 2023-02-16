namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using EnkuViewModelLocator.Wpf;

[ViewModel]
public partial class Page2ViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public Page2ViewModel(INavigationService navigationService)
    {
        this._navigationService = navigationService;
    }

    [ObservableProperty]
    private string _titleText = "Page2";

    [RelayCommand]
    private void Clicked()
    {
        this._navigationService.GoBack();
    }
}