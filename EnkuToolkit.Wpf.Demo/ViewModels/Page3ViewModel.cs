namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.UiIndependent.ViewModelInterfaces;

public partial class Page3ViewModel : ObservableObject, INavigatedParamReceive
{
    private readonly INavigationService _navigationService;
    private readonly IApplicationPropertyiesService _applicationPropertyiesService;
    private readonly IMessageBoxService _messageBoxService;

    public Page3ViewModel(INavigationService navigationService,
                          IApplicationPropertyiesService applicationPropertyiesService,
                          IMessageBoxService messageBoxService)
    {
        this._navigationService = navigationService;
        this._applicationPropertyiesService = applicationPropertyiesService;
        this._messageBoxService = messageBoxService;
    }

    [RelayCommand]
    private void GoBack()
    {
        this._navigationService.GoBack();
    }

    public void Navigated(object? extraData)
    {
        System.Diagnostics.Debug.WriteLine("Page3");
    }
}
