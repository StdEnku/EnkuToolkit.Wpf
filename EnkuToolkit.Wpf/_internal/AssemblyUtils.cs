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
namespace EnkuToolkit.Wpf._internal;

using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

internal static class AssemblyUtils
{
    public static IEnumerable<Type> GetAllClientDefinedTypes()
    {
        var entryAsm = Assembly.GetEntryAssembly();
        var refelencedAsmNames = entryAsm?.GetReferencedAssemblies();

        if (entryAsm is null) yield break;
        foreach (var definedTypeInfo in entryAsm.DefinedTypes)
        {
            yield return definedTypeInfo.AsType();
        }

        if (refelencedAsmNames is null) yield break;
        Assembly asm;
        foreach (var asmName in refelencedAsmNames)
        {
            asm = Assembly.Load(asmName);
            foreach (var definedTypeInfo in asm.DefinedTypes)
            {
                yield return definedTypeInfo.AsType();
            }
        }
    }

    public static Type? SearchAllClientDefinedTypes(string fullTypeName)
    {
        var types = GetAllClientDefinedTypes();
        var result = (from type in types
                      where type.FullName == fullTypeName
                      select type).FirstOrDefault();
        return result;
    }
}