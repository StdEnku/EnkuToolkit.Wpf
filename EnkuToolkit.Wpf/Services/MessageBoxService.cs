namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows;

/// <summary>
/// ViewServce to allow message box operations to be performed from the ViewModel
/// </summary>
public class MessageBoxService : IMessageBoxService
{
    /// <summary>
    /// Method for displaying a message box with no choices
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="title">Title</param>
    public void ShowOk(string message, string? title = null)
    {
        MessageBox.Show(message, title ?? string.Empty);
    }

    /// <summary>
    /// Method for displaying a message box with a Yes or No choice
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="title">Title</param>
    /// <returns>Returns true if the user selects Yes; returns false if the user selects No</returns>
    public bool ShowYesNo(string message, string? title = null)
    {
        var button = MessageBoxButton.YesNo;
        var response = MessageBox.Show(message, title ?? string.Empty, button);
        return response == MessageBoxResult.Yes ? true : false;
    }
}