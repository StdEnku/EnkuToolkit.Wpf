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
using System.IO;
using System.Windows.Markup;

/// <summary>
/// Markup extension to retrieve all strings from a text file
/// </summary>
[MarkupExtensionReturnType(typeof(string))]
public class ReadTextFileExtension : MarkupExtension
{
    private readonly string _path;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="path">file path</param>
    public ReadTextFileExtension(string path) => _path = path;

    /// <summary>
    /// Methods to create return values for this markup extension
    /// </summary>
    /// <param name="serviceProvider">Not used in this markup extension.</param>
    /// <returns>Text file string specified in the constructor</returns>
    public override object? ProvideValue(IServiceProvider serviceProvider) => File.ReadAllText(_path);
}