namespace Sandbox.Views;

using System.Windows.Controls;

public partial class SubPage : Page
{
    public SubPage()
    {
        InitializeComponent();
    }

    public override void OnApplyTemplate()
    {
        System.Threading.Tasks.Task.Delay(200).Wait();
        base.OnApplyTemplate();
    }
}
