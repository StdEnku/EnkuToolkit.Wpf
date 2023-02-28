namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Constants;
using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.UiIndependent.ViewModelInterfaces;

public partial class Page2ViewModel : ObservableObject, INavigatedParamReceive
{
    private readonly INavigationService _navigationService;
    private readonly IApplicationPropertyiesService _applicationPropertyiesService;
    private readonly IMessageBoxService _messageBoxService;

    public Page2ViewModel(INavigationService navigationService,
                          IApplicationPropertyiesService applicationPropertyiesService,
                          IMessageBoxService messageBoxService)
    {
        this._navigationService = navigationService;
        this._applicationPropertyiesService = applicationPropertyiesService;
        this._messageBoxService = messageBoxService;
    }

    [RelayCommand]
    private void NextPage()
    {
        if (this._navigationService.CanGoForward)
            this._navigationService.GoForward();
        else
            this._navigationService.NavigateRootBase("Views/Page3.xaml", "From Page2");
    }

    [RelayCommand]
    private void GoBack()
    {
        this._navigationService.GoBack();
    }

    public void Navigated(object? extraData, NavigationMode mode)
    {
        System.Diagnostics.Debug.WriteLine(mode);
    }
}