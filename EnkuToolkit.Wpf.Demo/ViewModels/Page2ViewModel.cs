namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using EnkuViewModelLocator.Wpf;

[ViewModel]
public partial class Page2ViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IMessageBoxService _messageBoxService;

    public Page2ViewModel(INavigationService navigationService, IMessageBoxService messageBoxService)
    {
        this._navigationService = navigationService;
        this._messageBoxService = messageBoxService;
    }

    [ObservableProperty]
    private string _titleText = "Page2";

    [RelayCommand]
    private void Clicked()
    {
        this._navigationService.GoBack();
    }

    [RelayCommand]
    private void ShowMessageBox()
    {
        this._messageBoxService.ShowOk("hello message box", "title");
    }
}