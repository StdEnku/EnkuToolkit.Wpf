namespace _00_TransitionEffect.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;

[ViewModel(true)]
public partial class MainPageViewModel : ObservableObject
{
    private INavigationService _navigationService;
    private IMessageBoxService _messageBoxService;

    public MainPageViewModel(INavigationService navigationService, IMessageBoxService messageBoxService)
    {
        _navigationService = navigationService;
        _messageBoxService = messageBoxService;
    }

    [ObservableProperty]
    private string _text = "Hello World!";

    [RelayCommand]
    private void Clicked()
    {
        _navigationService.NavigateRootBase("Views/SubPage.xaml", Text);
    }
}