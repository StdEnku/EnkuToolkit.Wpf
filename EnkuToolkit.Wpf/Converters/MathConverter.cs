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
/// Types of arithmetic operations
/// </summary>
public enum MathOperations
{
    /// <summary>
    /// addition
    /// </summary>
    Add,

    /// <summary>
    /// subtraction
    /// </summary>
    Sub,

    /// <summary>
    /// multiplication
    /// </summary>
    Mul,

    /// <summary>
    /// division
    /// </summary>
    Div,
}

/// <summary>
/// Four arithmetic operations possible converter
/// </summary>
public class MathConverter : IMultiValueConverter
{
    private static dynamic DynamicCast(object obj, Type castTo)
        => System.Convert.ChangeType(obj, castTo);

    /// <summary>
    /// Property to get instance
    /// </summary>
    public static MathConverter Instance => new MathConverter();

    /// <summary>
    /// Processing for forward conversion
    /// </summary>
    /// <param name="values">binding source</param>
    /// <param name="targetType">Target Type</param>
    /// <param name="parameter">Specify the operation mode</param>
    /// <param name="culture">Not used</param>
    /// <exception cref="ArgumentNullException">Exception thrown when parameter argument is not specified</exception>
    /// <returns>operation result</returns>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is null)
            throw new ArgumentNullException(nameof(parameter), "EnkuToolkit.Wpf.Converters.MathConverter cannot be executed without parameters.");

        var operation = (MathOperations)parameter;

        var result = DynamicCast(0, targetType);
        dynamic castedValue;

        for (int i = 0; i < values.Length; i++)
        {
            castedValue = DynamicCast(values[i], targetType);
            if (i == 0)
            {
                result = castedValue;
            }
            else
            {
                result = operation == MathOperations.Add ? result + castedValue :
                         operation == MathOperations.Sub ? result - castedValue :
                         operation == MathOperations.Mul ? result * castedValue :
                         operation == MathOperations.Div ? result / castedValue :
                         result;
            }
        }

        return result;
    }

    /// <summary>
    /// Processing for backward conversion
    /// </summary>
    /// <remarks>
    /// If used, an InvalidOperationException will be thrown since it is not supported.
    /// </remarks>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new InvalidOperationException("EnkuToolkit.Wpf.Converters.MathConverter does not support reverse returns.");
}