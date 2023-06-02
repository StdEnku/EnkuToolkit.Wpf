![logo](./images/logo.png)

![Nuget](https://img.shields.io/nuget/v/EnkuToolkit.Wpf) ![.Net: 7 (shields.io)](https://img.shields.io/badge/.Net-7-blueviolet)<br/>

# Summary

This library implements features that the author felt were lacking in the standard WPF functionality. This library is an OSS library under the ZLib license.
Therefore, when you use this library or copy a part of the code of this library to create other libraries or applications, you do not need to display the credit of this library in the repository where you manage your deliverables or in the credit screen of your application.

Specifically, it includes the following features

- Custom controls
- Unique animation effect mechanism
- Attached behaviors to extend existing controls
- Value converters
- MarkupExtensions
- ViewServices

# Explanation of the two assemblies

![Nuget](https://img.shields.io/nuget/dt/EnkuToolkit.Wpf?label=EnkuToolkit.Wpf&logo=Nuget&style=social) : [NuGet Gallery | EnkuToolkit.Wpf](https://www.nuget.org/packages/EnkuToolkit.Wpf)<br/>
![Nuget](https://img.shields.io/nuget/dt/EnkuToolkit.UiIndependent?label=EnkuToolkit.UiIndependent&logo=Nuget&style=social) : [NuGet Gallery | EnkuToolkit.UiIndependent](https://www.nuget.org/packages/EnkuToolkit.UiIndependent)<br/>

This library consists of two assemblies:<br/>EnkuToolkit.Wpf, which depends on the WPF assembly, and EnkuToolkit.UiIndependent, <br/>which can be called from ViewModel and does not depend on the WPF assembly.

When installing from Nuget, please note the following

If you want to manage View and ViewModel in the same project, <br/>install only EnkuToolkit.Wpf from Nuget because EnkuToolkit.Wpf <br/>depends on EnkuToolkit.UiIndependent.

If you want to manage View and ViewModel in different projects, <br/>install EnkuToolkit.Wpf in the project for View and EnkuToolkit.UiIndependent <br/>in the project for ViewModel.

# To call classes in this library from xaml

To access the classes of this library from xaml, use the following xml namespace.

> ```xaml
> xmlns:et="https://github.com/StdEnku/EnkuToolkit"
> ```
