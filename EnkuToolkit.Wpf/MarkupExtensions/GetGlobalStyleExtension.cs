namespace EnkuToolkit.Wpf.MarkupExtensions;

using System;
using System.Windows;
using System.Windows.Markup;

public class GetGlobalStyleExtension : MarkupExtension
{
    private Type _type;

    public GetGlobalStyleExtension(Type type)
    {
        this._type = type;
    }

    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        return Application.Current.FindResource(this._type);
    }
}