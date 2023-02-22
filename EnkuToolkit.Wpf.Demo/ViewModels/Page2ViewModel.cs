﻿namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using EnkuViewModelLocator.Wpf;

[ViewModel(ViewModelAttribute.ServiceLifeTime.Singleton)]
public partial class Page2ViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IMessageBoxService _messageBoxService;

    public Page2ViewModel(INavigationService navigationService, IMessageBoxService messageBoxService)
    {
        this._navigationService = navigationService;
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

    [RelayCommand]
    private void Navigated(object? extraData)
    {
        var data = extraData as string;
        if (data is not null)
            System.Diagnostics.Debug.WriteLine(data);
    }
}