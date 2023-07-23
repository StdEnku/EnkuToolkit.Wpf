namespace Sandbox.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using EnkuToolkit.UiIndependent.Attributes;

[DiRegister]
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _text = "Hello World!";
}