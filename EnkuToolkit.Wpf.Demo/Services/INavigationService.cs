namespace EnkuToolkit.Wpf.Demo.Services;

using System;

public interface INavigationService
{
    bool Navigate(Uri uri, object? extraData = null);

    void GoForward();

    void GoBack();
}