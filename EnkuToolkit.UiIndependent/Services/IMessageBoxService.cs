/*
 * MIT License
 * 
 * Copyright (c) 2023 StdEnku
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
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