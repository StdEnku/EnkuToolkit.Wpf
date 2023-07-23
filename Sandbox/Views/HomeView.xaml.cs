namespace Sandbox.Views;

using EnkuToolkit.UiIndependent.Attributes;
using System.Windows.Controls;

[DiRegister(DiRegisterMode.Singleton)]
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }
}
