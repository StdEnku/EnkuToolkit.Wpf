namespace Sandbox.ViewModels;

using EnkuToolkit.UiIndependent.Collections;
using EnkuToolkit.UiIndependent.Navigation;
using Sandbox.DataObjects;
using Sandbox.MvvmUtil;
using System;
using System.Diagnostics;

public class Page1ViewModel : ViewModelsBase, INavigationAware
{
    public void OnNavigated(object? param, NavigationMode navigationMode)
    {
        Debug.WriteLine(navigationMode.ToString());
        var p = (string?)param;
        Debug.WriteLine(p);
    }

    private DayDataCollection _dayDataCollection = new(DateTime.Now.Year, DateTime.Now.Month)
    {
        new DayData(1, "Tater tots", "Cobb salad", "Pot roast"),
        new DayData(3, "bread", "fish", "Fajitas"),
        new DayData(5, "salmon", "fish", "beef"),
        new DayData(7, "bread", "fish", "Jambalaya"),
    };

    public DayDataCollection DayDataCollection
    {
        get => _dayDataCollection;
        set
        {
            _dayDataCollection = value;
            NotifyPropertyChanged();
        }
    }
}
