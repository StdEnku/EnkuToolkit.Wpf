namespace EnkuToolkit.Wpf.MarkupExtensions;

using System;
using System.Windows;
using System.Windows.Markup;

/// <summary>
/// Markup extension to retrieve resources in the Application class.
/// </summary>
public class GetApplicationResourceExtension : MarkupExtension
{
    private Object _key;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="key">Key of the resource you want to search.</param>
    public GetApplicationResourceExtension(Object key)
        => _key = key;

    /// <summary>
    /// Methods to create return values for this markup extension
    /// </summary>
    /// <returns>
    /// Object of the discovered resource
    /// Return null if not found
    /// </returns>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        object? result;

        try
        {
            result = Application.Current.FindResource(_key);
        }
        catch (ResourceReferenceKeyNotFoundException)
        {
            result = null;
        }

        return result;
    }
}