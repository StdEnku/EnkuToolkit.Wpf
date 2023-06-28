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
namespace EnkuToolkit.Wpf.Converters;

using System;
using System.Globalization;
using System.Windows.Data;

/// <summary>
/// Converter that returns the value specified by the ResultValueIfNull property if the binding source value is null
/// </summary>
public class NullCoalescingOperatorConverter : IValueConverter
{
    /// <summary>
    /// Property for specifying the value that will be the result of the conversion when the binding source is null
    /// </summary>
    public object? ResultValueIfNull { get; init; }

    /// <summary>
    /// Methods for forward conversion processing
    /// </summary>
    /// <param name="value">Value of the binding source to be converted</param>
    /// <param name="targetType">Not used with this converter</param>
    /// <param name="parameter">Not used with this converter</param>
    /// <param name="culture">Not used with this converter</param>
    /// <returns>
    /// If the binding source is null, the value specified by the ResultValueIfNull property is returned.
    /// If the binding source is not null, the value of the binding source is returned as is.
    /// </returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value ?? ResultValueIfNull;
    }

    /// <summary>
    /// Methods for processing conversions in the reverse direction
    /// </summary>
    /// <param name="value">Not used with this converter</param>
    /// <param name="targetType">Not used with this converter</param>
    /// <param name="parameter">Not used with this converter</param>
    /// <param name="culture">Not used with this converter</param>
    /// <returns>No return value because an exception is always raised when executed</returns>
    /// <exception cref="InvalidOperationException">Exception thrown when an attempt is made to perform a conversion operation from a binding source to a binding target.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("NullCoalescingOperatorConverter does not support conversion from binding source to binding target, so an exception is thrown if such an operation is attempted.");
    }
}