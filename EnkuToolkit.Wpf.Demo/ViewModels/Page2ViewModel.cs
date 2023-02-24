namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using EnkuToolkit.UiIndependent.ViewModelInterfaces;
using EnkuViewModelLocator.Wpf;

[ViewModel]
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
        this._navigationService.NavigateRootBase("Views/Page3.xaml", "From Page2");
    }

    [RelayCommand]
    private void GoBack()
    {
        this._navigationService.GoBack();
    }

    public void Navigated(object? extraData)
    {
        System.Diagnostics.Debug.WriteLine("Page2");
    }
}