namespace Sandbox.Services;

using EnkuToolkit.Wpf.Services;
using System.Windows.Controls;

public class MainFrameNavigationService : AbstractFrameNavigationService
{
    protected override Frame TargetFrame
    {
        get
        {
            var mw = (MainWindow)App.Current.MainWindow;
            return mw.mainFrame;
        }
    }
}