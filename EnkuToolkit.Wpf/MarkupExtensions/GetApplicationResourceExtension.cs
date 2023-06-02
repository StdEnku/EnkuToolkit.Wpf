/*
 * Copyright (c) 2022 StdEnku
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
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