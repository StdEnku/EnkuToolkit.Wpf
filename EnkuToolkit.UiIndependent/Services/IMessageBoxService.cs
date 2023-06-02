/*
 * These codes are licensed under CC0.
 * https://creativecommons.org/publicdomain/zero/1.0/deed
 */
namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// Interface for ViewService to enable manipulation of MessageBox from ViewModel
/// </summary>
public interface IMessageBoxService
{
    /// <summary>
    /// Method for displaying an OK button-only message box with no choices
    /// </summary>
    /// <param name="message">Message Text</param>
    /// <param name="title">Title text</param>
    void ShowOk(string message, string? title = null);

    /// <summary>
    /// Method to display a message box with Yes and No buttons
    /// </summary>
    /// <param name="message">Message Text</param>
    /// <param name="title">Title text</param>
    /// <returns>Returns true if the user presses the Yes button and returns false if the user presses No</returns>
    bool ShowYesNo(string message, string? title = null);
}