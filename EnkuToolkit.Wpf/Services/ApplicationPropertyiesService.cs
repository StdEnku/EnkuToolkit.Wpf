﻿namespace EnkuToolkit.Wpf.Services;

using EnkuToolkit.UiIndependent.Services;
using System.Collections;
using System.Windows;

/// <summary>
/// ViewService to make the Application.Properties property operable from the ViewModel
/// </summary>
public class ApplicationPropertyiesService : IApplicationPropertyiesService
{
    /// <summary>
    /// Properties to get the Application.Properties property
    /// </summary>
    public IDictionary Properties => Application.Current.Properties;
}