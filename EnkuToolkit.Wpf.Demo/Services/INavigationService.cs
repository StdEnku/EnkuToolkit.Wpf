namespace EnkuToolkit.Wpf.Demo.Services;

using System;

public interface INavigationService
{
    bool Navigate(string uriStr, object? extraData = null);

    void GoForward();

    void GoBack();
}