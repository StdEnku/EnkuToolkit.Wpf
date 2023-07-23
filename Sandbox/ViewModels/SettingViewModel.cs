namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using EnkuToolkit.UiIndependent.Attributes;

[DiRegister(DiRegisterMode.Singleton)]
public partial class SettingViewModel : ObservableObject
{
    [ObservableProperty]
    private string _text = "Setting";
}