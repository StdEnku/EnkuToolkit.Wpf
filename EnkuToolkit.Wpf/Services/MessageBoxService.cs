/*
 * Copyright (c) 2022 StdEnku
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
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