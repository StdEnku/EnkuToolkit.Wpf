namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using System.Collections;
using System.Collections.Generic;

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
        this._sources = new();
        this._sources.Add("alpha");
        this._sources.Add("beta");
        this._sources.Add("chary");
        this._sources.Add("delta");
    }

    [ObservableProperty]
    private IList? _selectedSources;

    [ObservableProperty]
    private List<string> _sources;

    [RelayCommand]
    private void ToNextPageButtonClicked()
    {
        this._navigationService.NavigateRootBase("Views/Page2.xaml");
    }

    [RelayCommand]
    private void ShowSelectedItemsClicked()
    {

    }
}