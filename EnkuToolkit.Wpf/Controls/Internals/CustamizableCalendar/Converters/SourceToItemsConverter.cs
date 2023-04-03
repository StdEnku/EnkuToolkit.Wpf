namespace EnkuToolkit.Wpf.Controls.Internals.CustamizableCalendar.Converters;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using UiIndependent.CustamizableCalendarDatas;

internal class SourceToItemsConverter : IMultiValueConverter
{
    // インスタンス取得用のプロパティ
    public static SourceToItemsConverter Instance => new SourceToItemsConverter();

    // ある月のカレンダーに表示するすべての日付を取得するためのメソッド
    private static IEnumerable<DateTime> DatesOnAPage(int year, int month, DayOfWeek startDayOfWeek)
    {
        var firstDateOfMonth = new DateTime(year, month, 1);
        var firstDayOfWeek = firstDateOfMonth.DayOfWeek;
        var subLastMonday = startDayOfWeek - firstDayOfWeek;

        var firstDate = firstDateOfMonth.AddDays(subLastMonday);
        var endDate = firstDate.AddDays(42);

        for (var dateTime = firstDate; dateTime < endDate; dateTime = dateTime.AddDays(1))
            yield return dateTime;
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var calendarSources = (IEnumerable<CalendarSource>)values[0];
        var isStartWeekMonday = (bool)values[1];
        var cellTemplate = (DataTemplate)values[2];
        var autoGenCellTemplate = (DataTemplate)values[3];
        var yearAndMonth = (YearAndMonth)values[4];
        var targetYear = yearAndMonth.Year;
        var targetMonth = yearAndMonth.Month;
        
        var allDates = DatesOnAPage(targetYear, targetMonth, isStartWeekMonday ? DayOfWeek.Monday : DayOfWeek.Sunday);
        var result = new List<CalendarCellFullSource>(42);

        bool isTargetYearAndMonth;
        CalendarSource? tempSource;
        CalendarSource source;
        CalendarCellFullSource fullSource;
        foreach (var datetime in allDates)
        {
            isTargetYearAndMonth = datetime.Year == targetYear && datetime.Month == targetMonth;

            tempSource = (from calendarSource in calendarSources
                          where calendarSource.Date == datetime.Date
                          select calendarSource).FirstOrDefault();

            source = tempSource ?? new CalendarSource(datetime);

            fullSource = new CalendarCellFullSource(source, isTargetYearAndMonth, tempSource is null ? autoGenCellTemplate : cellTemplate);
            result.Add(fullSource);
        }

        return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}