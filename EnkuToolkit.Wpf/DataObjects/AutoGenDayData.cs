/*
 * These codes are licensed under CC0.
 * https://creativecommons.org/publicdomain/zero/1.0/deed
 */
namespace EnkuToolkit.Wpf.DataObjects;

using EnkuToolkit.UiIndependent.DataObjects;
using System;

/// <summary>
/// Type of object that will be the DataContext in the DataTemplate in the auto-generated cell used in the CustamizableCalendar
/// </summary>
public class AutoGenDayData : BaseDayData
{
    /// <summary>
    /// Property to get the day
    /// </summary>
    public override int Day { get; }

    /// <summary>
    /// Property to get the year
    /// </summary>
    public override int Year { get; }

    /// <summary>
    /// Property to get the month
    /// </summary>
    public override int Month { get; }

    /// <summary>
    /// Property to get whether the target date matches today's date
    /// </summary>
    public override bool IsToday { get; }

    /// <summary>
    /// Property to get whether the target day is a holiday or not
    /// </summary>
    public override bool IsHoliday { get; }

    /// <summary>
    /// Property to get a DateTime object for the target date
    /// </summary>
    public override DateTime DateTime { get; }

    /// <summary>
    /// Property indicating whether the month is the target month or not
    /// </summary>
    public bool IsTargetMonth { get; }

    internal AutoGenDayData(DateTime dateTime, bool isTargetMonth) : base(dateTime.Day)
    {
        Year = dateTime.Year;
        Month = dateTime.Month;
        Day = dateTime.Day;
        IsToday = dateTime.Date == DateTime.Now.Date;
        IsHoliday = dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday;
        DateTime = dateTime;
        IsTargetMonth = isTargetMonth;
    }
}