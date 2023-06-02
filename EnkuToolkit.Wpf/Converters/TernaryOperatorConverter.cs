/*
 * These codes are licensed under CC0.
 * https://creativecommons.org/publicdomain/zero/1.0/deed
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
    public object? ResultValueIfTrue { get; init; }

    /// <summary>
    /// Value returned if binding source is false
    /// </summary>
    public object? ResultValueIfFalse { get; init; }

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
    /// <returns>
    /// Returns True if the value of the binding target is equal to the value of the ResultValueIfTrue property
    /// ReturnsFalse if the value of the binding target is equal to the value of the ResultValueIfFalse property
    /// </returns>
    /// <exception cref="ArgumentException">
    /// If the value of the binding target is
    /// ResultValueIfTrue property and the value of the
    /// ResultValueIfFalse property.
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("TernaryOperatorConverter does not support binding source to binding target conversion.");
    }
}