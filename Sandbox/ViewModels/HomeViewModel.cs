namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using EnkuToolkit.UiIndependent.Attributes;

[DiRegister(DiRegisterMode.Singleton)]
public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string _text = "Hello World!";
}