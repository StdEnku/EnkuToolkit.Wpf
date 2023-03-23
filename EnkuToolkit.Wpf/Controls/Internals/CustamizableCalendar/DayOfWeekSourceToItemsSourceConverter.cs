namespace EnkuToolkit.Wpf.Controls.Internals.CustamizableCalendar;

using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System;
using UiIndependent.CustamizableCalendarDatas;
using System.Linq;

internal class DayOfWeekSourceToItemsSourceConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (IEnumerable<CalendarDayOfWeekSource>)values[0];
        var isStartWeekMonday = (bool)values[1];
        var result = new List<CalendarDayOfWeekSource>();

        var startSundayDayOfWeeks = Enum.GetValues<DayOfWeek>();
        var startMondayDayOfWeeks = new DayOfWeek[]
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday,
        };

        foreach (var dayOfWeek in isStartWeekMonday ? startMondayDayOfWeeks : startSundayDayOfWeeks)
        {
            var foundDayOfWeekSource = (from dayOfWeekSource in input
                                        where dayOfWeekSource.TargetDayOfWeek == dayOfWeek
                                        select dayOfWeekSource).FirstOrDefault();

            foundDayOfWeekSource = foundDayOfWeekSource ?? new CalendarDayOfWeekSource() { TargetDayOfWeek = dayOfWeek, Text = dayOfWeek.ToString()[0..3] };
            result.Add(foundDayOfWeekSource);
        }

        return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}