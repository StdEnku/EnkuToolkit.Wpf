namespace _03_BindingViewModelPropertyToSelectedItems;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ViewModel]
public partial class MainWindowViewModel : ObservableObject
{
    private IMessageBoxService _messageBoxService;

    public MainWindowViewModel(IMessageBoxService messageBoxService)
    {
        _messageBoxService = messageBoxService;
    }

    [ObservableProperty]
    private IList _itemsSource = new List<string>()
    {
        "apple",
        "banana",
        "grape",
        "lemon",
        "strawberry",
        "peach",
        "mango",
        "papaya"
    };

    [ObservableProperty]
    private IList _selectedItems = new List<string>();

    [RelayCommand]
    private void ShowSelectedItem()
    {
        var text = string.Empty;
        var stringItems = SelectedItems.Cast<string>();
        foreach (var item in stringItems)
        {
            text += item + '\n';
        }

        _messageBoxService.ShowOk(text, "selected items");
    }

    [RelayCommand]
    private void AllSelect()
    {
        var nextSelectedItem = new List<object>()
        {
            "apple",
            "banana",
            "grape",
            "lemon",
            "strawberry",
            "peach",
            "mango",
            "papaya"
        };

        SelectedItems = nextSelectedItem;
    }
}