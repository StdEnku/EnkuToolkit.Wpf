namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Attributes;

[DiRegister(DiRegisterMode.Singleton)]
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isHomeSelected = true;

    [ObservableProperty]
    private bool _isAboutSelected = false;

    [ObservableProperty]
    private int _selectedIndex = 0;

    [RelayCommand]
    private void Selected()
    {
        var nextIndex = IsHomeSelected ? 0 : 1;

        SelectedIndex = nextIndex;
    }
}