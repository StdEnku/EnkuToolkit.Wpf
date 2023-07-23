namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EnkuToolkit.UiIndependent.Attributes;

[DiRegister(DiRegisterMode.Singleton)]
public partial class AboutViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isWpfSelected = true;

    [ObservableProperty]
    private bool _isBehaviorsSelected = false;

    [ObservableProperty]
    private bool _isMaterialDesignSelected = false;

    [ObservableProperty]
    private bool _isEnkuToolkitSelected = false;

    [ObservableProperty]
    private bool _isRuntimeSelected = false;

    [ObservableProperty]
    private bool _isMvvmToolkitSelected = false;

    [ObservableProperty]
    private int _selectedIndex = 0;

    [RelayCommand]
    private void Selected()
    {
        var nextIndex = IsWpfSelected ? 0 :
                        IsBehaviorsSelected ? 1 :
                        IsMaterialDesignSelected ? 2 :
                        IsEnkuToolkitSelected ? 3 :
                        IsRuntimeSelected ? 4 :
                        5;

        SelectedIndex = nextIndex;
    }
}