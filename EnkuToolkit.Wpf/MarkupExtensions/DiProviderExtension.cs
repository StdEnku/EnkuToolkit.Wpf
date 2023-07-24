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
namespace EnkuToolkit.Wpf.MarkupExtensions;

using System;
using System.Windows;
using System.Windows.Markup;
using System.Diagnostics;

/// <summary>
/// It is assumed to be implemented in a child class of Application, which has an interface with a property to obtain the ServiceProvider of the DI container and performs the initial configuration of the DI container.
/// </summary>
public interface IServicesOwner
{
    /// <summary>
    /// Property to get the ServiceProvider of the DI container
    /// </summary>
    IServiceProvider Services { get; }
}

/// <summary>
/// Markup extension to retrieve ViewModel from DI container, available only when the App class generated when creating a WPF project implements IServicesOwner
/// </summary>
[MarkupExtensionReturnType(typeof(object))]
public class DiProviderExtension : MarkupExtension
{
    private Type _type;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="type">Type object indicating the ViewModel type</param>
    public DiProviderExtension(Type type)
    {
        _type = type;
    }

    /// <summary>
    /// Methods to create return values for this markup extension
    /// </summary>
    /// <returns>ViewModel object generated from a DI container</returns>
    /// <param name="serviceProvider">Not used in this markup extension.</param>
    /// <exception cref="InvalidOperationException">
    /// Exception thrown when DI container cannot create ViewModel or cast Application.Current to IServicesOwner
    /// </exception>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        var servicesOwner = Application.Current as IServicesOwner;

        if (servicesOwner is null)
            throw new InvalidOperationException("Cannot cast Application.Current to IServicesOwner.");

        var services = servicesOwner.Services;

        Debug.Assert(_type is not null);
        var result = services.GetService(_type);

        if (result is null)
            throw new InvalidOperationException($"DI container could not generate type {_type.FullName}.");

        return result;
    }
}