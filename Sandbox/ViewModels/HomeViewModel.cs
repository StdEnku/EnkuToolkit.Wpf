namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using EnkuToolkit.UiIndependent.Attributes;
using EnkuToolkit.UiIndependent.Collections;
using System;
using Sandbox.DataObjects;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;

[DiRegister(DiRegisterMode.Singleton)]
public partial class HomeViewModel : ObservableObject
{
    private IMessageBoxService _messageBoxService;

    public HomeViewModel(IMessageBoxService messageBoxService)
    {
        _messageBoxService = messageBoxService;
    }

    [ObservableProperty]
    private DayDataCollection _source = new(DateTime.Now.Year, DateTime.Now.Month);

    [ObservableProperty]
    private IEnumerable<DateTime> _selectedDates = new List<DateTime>();

    [RelayCommand]
    private void DoubleClicked(object? date)
    {
        _messageBoxService.ShowOk(date?.ToString() ?? "null", "DoubleClicked Date");
    }

    [RelayCommand]
    private void ShowSelectedDates()
    {
        var text = string.Empty;

        foreach (var date in SelectedDates)
            text += date.ToString() + '\n';

        _messageBoxService.ShowOk(text, "Selected Dates");
    }

    [RelayCommand]
    private void SelectedDatesClear()
    {
        SelectedDates = new List<DateTime>();
    }

    [RelayCommand]
    private void LeftArrowClicked()
    {
        Source = Source.CreateLastMonth();
    }

    [RelayCommand]
    private void RightArrowClicked()
    {
        Source = Source.CreateNextMonth();
    }

    [RelayCommand]
    private void AddMenus()
    {
        var nextSource = Source.CreateSameMonth();
        nextSource.Add(new DayData(1, "bread", "fish", "beef"));
        nextSource.Add(new DayData(3, "bread", "fish", "beef"));
        nextSource.Add(new DayData(5, "bread", "fish", "beef"));
        nextSource.Add(new DayData(7, "bread", "fish", "beef"));
        nextSource.Add(new DayData(9, "bread", "fish", "beef"));
        Source = nextSource;
    }
}