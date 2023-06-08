namespace _00_TransitionEffect.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using EnkuToolkit.UiIndependent.Navigation;
using EnkuToolkit.UiIndependent.Services;

[ViewModel]
public partial class SubPageViewModel : ObservableObject, INavigationAware
{
    private INavigationService _navigationService;
    private IMessageBoxService _messageBoxService;

    public SubPageViewModel(INavigationService navigationService, IMessageBoxService messageBoxService)
    {
        _navigationService = navigationService;
        _messageBoxService = messageBoxService;
    }

    [ObservableProperty]
    private string _text = string.Empty;

    public void OnNavigated(object? param, NavigationMode navigationMode)
    {
        if (navigationMode == NavigationMode.New || navigationMode == NavigationMode.Forward)
        {
            Text = param as string ?? "null";
        }
    }
}