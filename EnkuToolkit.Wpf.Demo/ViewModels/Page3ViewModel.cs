namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using EnkuViewModelLocator.Wpf;

[ViewModel(ViewModelAttribute.ServiceLifeTime.Singleton)]
public partial class Page3ViewModel : ObservableObject
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

    [RelayCommand]
    private void Navigated(object? extraData)
    {
        var data = extraData as string;
        if (data is not null)
            System.Diagnostics.Debug.WriteLine(data);
    }
}
