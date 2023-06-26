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
/// A converter that returns the value specified in the ResultValueIfTrue property if the binding source is true, like a ternary operator, and returns the value specified in the ResultValueIfFalse property if false.
/// </summary>
public class TernaryOperatorConverter : IValueConverter
{
    /// <summary>
    /// Value returned if binding source is true
    /// </summary>
    public object? ResultValueIfTrue { get; set; }

    /// <summary>
    /// Value returned if binding source is false
    /// </summary>
    public object? ResultValueIfFalse { get; set; }

    /// <summary>
    /// Methods for forward conversion processing
    /// </summary>
    /// <param name="value">must be a binding source value and castable to a bool value</param>
    /// <param name="targetType">Not used with this converter</param>
    /// <param name="parameter">Not used with this converter</param>
    /// <param name="culture">Not used with this converter</param>
    /// <returns>
    /// Returns the value of the ResultValueIfTrue property if the value of the binding source is True
    /// Returns the value of the ResultValueIfFalse property if the value of the binding source is False
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Exception thrown when the binding source value cannot be cast to a bool value
    /// </exception>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var flag = value as bool?;

        if (flag is null)
            throw new ArgumentException(nameof(value), "Binding source could not be cast to a bool value.");

        return flag.Value ? ResultValueIfTrue : ResultValueIfFalse;
    }

    /// <summary>
    /// Methods for processing conversions in the reverse direction
    /// </summary>
    /// <param name="value">Not used with this converter</param>
    /// <param name="targetType">Not used with this converter</param>
    /// <param name="parameter">Not used with this converter</param>
    /// <param name="culture">Not used with this converter</param>
    /// <returns>No return value because an exception is always raised when executed</returns>
    /// <exception cref="InvalidOperationException">Exceptions thrown when this method is executed</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("TernaryOperatorConverter does not support binding source to binding target conversion.");
    }
}