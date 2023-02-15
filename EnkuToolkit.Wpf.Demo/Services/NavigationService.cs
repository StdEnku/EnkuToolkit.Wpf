namespace EnkuToolkit.Wpf.Demo.Services;

using System.Windows.Controls;
using System;

public class NavigationService : INavigationService
{
    private Frame MainFrame
    {
        get
        {
            var window = (MainWindow)App.Current.MainWindow;
            return window.MainFrmae;
        }
    }

    public bool Navigate(Uri uri, object? extraData = null)
    {
        if (extraData is null)
        {
            return this.MainFrame.Navigate(uri);
        }
        else
        {
            return this.MainFrame.Navigate(uri, extraData);
        }
    }

    public void GoForward()
    {
        this.MainFrame.GoForward();
    }

    public void GoBack()
    {
        this.MainFrame.GoBack();
    }
}
