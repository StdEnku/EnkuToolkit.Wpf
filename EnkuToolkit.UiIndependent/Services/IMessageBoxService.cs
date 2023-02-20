namespace EnkuToolkit.UiIndependent.Services;

/// <summary>
/// Interface for ViewServce to allow message box operations from ViewModel
/// </summary>
public interface IMessageBoxService
{
    /// <summary>
    /// Method for displaying a message box with no choices
    /// </summary>
    /// <param name="message">message</param>
    /// <param name="title">title</param>
    void ShowOk(string message, string? title = null);

    /// <summary>
    /// Method for displaying a message box with a Yes or No choice
    /// </summary>
    /// <param name="message">message</param>
    /// <param name="title">title</param>
    /// <returns>Returns true if the user selects Yes; returns false if the user selects No</returns>
    bool ShowYesNo(string message, string? title = null);
}