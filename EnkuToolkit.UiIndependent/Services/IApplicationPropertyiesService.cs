namespace EnkuToolkit.UiIndependent.Services;

using System.Collections;

/// <summary>
/// Interface for ViewService to make Application.Properties properties operable from ViewModel
/// </summary>
public interface IApplicationPropertyiesService
{
    /// <summary>
    /// Properties to get the Application.Properties property
    /// </summary>
    IDictionary Properties { get; }
}