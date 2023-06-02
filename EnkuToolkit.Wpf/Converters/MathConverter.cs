/*
 * These codes are licensed under CC0.
 * https://creativecommons.org/publicdomain/zero/1.0/deed
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