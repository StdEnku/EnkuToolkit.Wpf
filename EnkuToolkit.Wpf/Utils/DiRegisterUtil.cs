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