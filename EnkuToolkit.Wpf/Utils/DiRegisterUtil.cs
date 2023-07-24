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
namespace EnkuToolkit.Wpf.Utils;

using EnkuToolkit.UiIndependent.Attributes;
using EnkuToolkit.Wpf._internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

/// <summary>
/// DiRegister attribute-related utility classes
/// </summary>
public static class DiRegisterUtil
{
    /// <summary>
    /// Method to retrieve all types attached to the EnkuToolkit.UiIndependent.Attributes.DiRegister attribute
    /// </summary>
    /// <returns>All types with attached DiRegister attribute</returns>
    public static IEnumerable<DiRegisteredType> AllDiRegisterAttributeAttachedTypes()
    {
        var types = from type in AssemblyUtils.GetAllClientDefinedTypes()
                    where type.GetCustomAttributes(typeof(DiRegisterAttribute), false).Count() == 1
                    select type;

        DiRegisterAttribute? attrib;
        DiRegisterMode mode;
        foreach (var type in types)
        {
            attrib = type.GetCustomAttribute(typeof(DiRegisterAttribute)) as DiRegisterAttribute;
            Debug.Assert(attrib is not null);
            mode = attrib.DiRegisterMode;
            yield return new DiRegisteredType(type, mode);
        }
    }
}

/// <summary>
/// Data object to retrieve type information and registration form information attached to DiRegister attribute
/// </summary>
public class DiRegisteredType
{
    /// <summary>
    /// type information
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// registration type information
    /// </summary>
    public DiRegisterMode Mode { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="type">type information</param>
    /// <param name="mode">registration type information</param>
    public DiRegisteredType(Type type, DiRegisterMode mode)
    {
        Type = type;
        Mode = mode;
    }
}