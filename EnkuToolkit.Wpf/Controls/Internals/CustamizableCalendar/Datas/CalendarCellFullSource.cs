namespace EnkuToolkit.Wpf.Controls.Internals.CustamizableCalendar;

using EnkuToolkit.UiIndependent.CustamizableCalendarDatas;
using System.Windows;

internal class CalendarCellFullSource
{
    public CalendarSource? Source { get; }

    public bool IsEnabled { get; }

    public DataTemplate DataTemplate { get; }

    public CalendarCellFullSource(CalendarSource? source, bool isEnabled, DataTemplate dataTemplate)
    {
        this.Source = source;
        this.IsEnabled = isEnabled;
        this.DataTemplate = dataTemplate;
    }
}