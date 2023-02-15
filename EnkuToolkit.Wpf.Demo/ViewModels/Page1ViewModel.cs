namespace EnkuToolkit.Wpf.Demo.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.Wpf.Demo.Services;
using EnkuViewModelLocator.Wpf;
using System;

[ViewModel]
public partial class Page1ViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public Page1ViewModel(INavigationService navigationService)
    {
        this._navigationService = navigationService;
    }

    [ObservableProperty]
    private string _titleText = "Page1";

    [RelayCommand]
    private void Clicked()
    {
        var uri = new Uri("pack://application:,,,/Views/Page2.xaml", UriKind.Absolute);
        this._navigationService.Navigate(uri);
    }
}