namespace EnkuToolkit.Wpf.Controls.Internals.CustamizableCalendar;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using UiIndependent.CustamizableCalendarDatas;

internal class SourceToItemsConverter : IMultiValueConverter
{
    //ある範囲のDateTimeのIEnumerableを取得するためのメソッド
    private static IEnumerable<DateTime> getDateTimeRange(DateTime startDate, DateTime endDate)
    {
        for (var i = startDate; i < endDate; i = i.AddDays(1))
            yield return i;
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (IEnumerable<CalendarSource>)values[0];
        var isStartWeekMonday = (bool)values[1];
        var cellTemplate = (DataTemplate)values[2];
        var autoGenCellTemplate = (DataTemplate)values[3];
        var yearAndMonth = (YearAndMonth)values[4];
        var targetYear = yearAndMonth.Year;
        var targetMonth = yearAndMonth.Month;

        var result = new List<CalendarSource>(42);

        // 先月の日付を追加
        var firstDay = new DateTime(targetYear, targetMonth, 1);
        var subLastMonday = (isStartWeekMonday ? DayOfWeek.Monday : DayOfWeek.Sunday) - firstDay.DayOfWeek;
        var lastMonthDateTimes = getDateTimeRange(firstDay.AddDays(subLastMonday), firstDay);

        CalendarSource? source;
        foreach (var dateTime in lastMonthDateTimes)
        {
            source = new CalendarSource() { Date = dateTime, IsEnabled = false, TargetTemplate = autoGenCellTemplate };
            result.Add(source);
        }

        // 今月の日付を追加
        var daysInMonthNum = DateTime.DaysInMonth(targetYear, targetMonth);
        foreach (var dayNum in Enumerable.Range(1, daysInMonthNum))
        {
            source = (from i in input
                      where i.Date.Day == dayNum && i.Date.Month == targetMonth && i.Date.Year == targetYear
                      select i).FirstOrDefault();

            if (source is null)
            {
                source = new CalendarSource()
                {
                    Date = new DateTime(targetYear, targetMonth, dayNum),
                    TargetTemplate = autoGenCellTemplate
                };
            }
            else
            {
                source.TargetTemplate = cellTemplate;
            }

            result.Add(source);
        }

        // 来月の日付を追加
        var lastDay = new DateTime(targetYear, targetMonth, daysInMonthNum);
        var numberOfExtraCells = 43 - result.Count;
        var nextMonthDateTimes = getDateTimeRange(lastDay.AddDays(1), lastDay.AddDays(numberOfExtraCells));
        foreach (var dateTime in nextMonthDateTimes)
        {
            source = new CalendarSource() { Date = dateTime, IsEnabled = false, TargetTemplate = autoGenCellTemplate };
            result.Add(source);
        }

        return result;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}