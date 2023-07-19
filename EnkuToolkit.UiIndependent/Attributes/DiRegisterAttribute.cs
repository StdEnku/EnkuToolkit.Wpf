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

namespace EnkuToolkit.UiIndependent.Attributes;

/// <summary>
/// Attribute to mark registration to the DI container
/// </summary>
/// <remarks>
/// EnkuToolkit.Wpf.Utils.DiRegisterUtil.AllDiRegisterAttributeAttachedTypes method 
/// can be used to obtain a list of types to which this attribute is attached. 
/// However, the process of registering a DI container itself is not supported by this library.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class DiRegisterAttribute : Attribute
{
    /// <summary>
    /// Property for registration form acquisition
    /// </summary>
    public DiRegisterMode DiRegisterMode { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="diRegisterMode">Registration Form</param>
    public DiRegisterAttribute(DiRegisterMode diRegisterMode = DiRegisterMode.Transient)
        => DiRegisterMode = diRegisterMode;
}

/// <summary>
/// Enum value representing the registration type to the DI container
/// </summary>
public enum DiRegisterMode
{
    /// <summary>
    /// A new object is created each time the DI container calls an object
    /// </summary>
    Transient,

    /// <summary>
    /// Each time the DI container calls an object, 
    /// it checks to see if an object of that type is alive, and if so, 
    /// returns that object, and if it has already been freed Returns the newly created object.
    /// </summary>
    Scoped,

    /// <summary>
    /// Each time the DI container calls an object, it returns a singleton identical object.
    /// </summary>
    Singleton,
}