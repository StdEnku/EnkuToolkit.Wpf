namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using System.Net.Security;

public partial class Page1ViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IApplicationPropertyiesService _applicationPropertyiesService;
    private readonly IMessageBoxService _messageBoxService;

    public Page1ViewModel(INavigationService navigationService, 
                          IApplicationPropertyiesService applicationPropertyiesService,
                          IMessageBoxService messageBoxService)
    {
        this._navigationService = navigationService;
        this._applicationPropertyiesService = applicationPropertyiesService;
        this._messageBoxService = messageBoxService;
    }

    [ObservableProperty]
    private string _text = "Hello Wrold";

    [RelayCommand]
    private void NextPage()
    {
        if (this._navigationService.CanGoForward)
            this._navigationService.GoForward();
        else
        {
            this._navigationService.NavigateRootBase("Views/Page2.xaml", this.Text);
        }
    }
}