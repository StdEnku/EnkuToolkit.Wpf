/*
 * These codes are licensed under CC0.
 * https://creativecommons.org/publicdomain/zero/1.0/deed
 */
namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Windows;

/// <summary>
/// Implementation of ViewService to enable manipulation of MessageBox from ViewModel
/// </summary>
public class MessageBoxService : IMessageBoxService
{
    /// <summary>
    /// Method for displaying an OK button-only message box with no choices
    /// </summary>
    /// <param name="message">Message Text</param>
    /// <param name="title">Title text</param>
    public void ShowOk(string message, string? title = null)
    {
        MessageBox.Show(message, title ?? string.Empty);
    }

    /// <summary>
    /// Method to display a message box with Yes and No buttons
    /// </summary>
    /// <param name="message">Message Text</param>
    /// <param name="title">Title text</param>
    /// <returns>Returns true if the user presses the Yes button and returns false if the user presses No</returns>
    public bool ShowYesNo(string message, string? title = null)
    {
        var button = MessageBoxButton.YesNo;
        var response = MessageBox.Show(message, title ?? string.Empty, button);
        return response == MessageBoxResult.Yes ? true : false;
    }
}