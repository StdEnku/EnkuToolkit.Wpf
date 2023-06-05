namespace EnkuToolkit.Wpf.Converters;

using System;
using System.Globalization;
using System.Windows.Data;

/// <summary>
/// Converter for converting integers from 1 to 12 to strings with month names, such as January and February
/// </summary>
[ValueConversion(typeof(int), typeof(String))]
public class NumberToMonthNameConverter : IValueConverter
{
    /// <summary>
    /// Property to get an instance from x:Static
    /// </summary>
    public static NumberToMonthNameConverter Instance => new NumberToMonthNameConverter();

    /// <summary>
    /// Methods for Forward Conversion
    /// </summary>
    /// <param name="value">Binding Source Value</param>
    /// <param name="targetType">Not used</param>
    /// <param name="parameter">Not used</param>
    /// <param name="culture">Specify the ClutureInfo you wish to use</param>
    /// <returns>Names of months such as January and February</returns>
    /// <exception cref="ArgumentException">Exception thrown if the value of the binding source exceeds the range of values from 1 to 12</exception>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var num = (int)value;
        if (num < 1 || num > 12)
            throw new ArgumentOutOfRangeException(nameof(value), "When using the NumberToMonthNameConverter, the binding source can only have values from 1 to 12.");

        return culture.DateTimeFormat.GetMonthName(num);
    }

    /// <summary>
    /// Methods for reverse direction
    /// This converter does not support reverse conversion
    /// </summary>
    /// <param name="value">Not used</param>
    /// <param name="targetType">Not used</param>
    /// <param name="parameter">Not used</param>
    /// <param name="culture">Not used</param>
    /// <returns>Not used</returns>
    /// <exception cref="NotImplementedException">Exception thrown when a reverse transformation is performed</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("NumberToCurrentCultureMonthNameConverter does not support reverse return");
    }
}