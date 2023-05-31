namespace EnkuToolkit.UiIndependent.DataObjects;

using System;

/// <summary>
/// Data that serves as the source for the day-of-week row in the CustamizableCalendar
/// </summary>
public class DayOfWeekData
{
    /// <summary>
    /// Property to get the target day of the week
    /// </summary>
    public DayOfWeek DayOfWeek { get; init; }

    /// <summary>
    /// Text property to display as the name of the day of the week
    /// </summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// Property to get whether the target day of the week is a holiday or not
    /// </summary>
    public bool IsHoliday => DayOfWeek == DayOfWeek.Sunday || DayOfWeek == DayOfWeek.Saturday;
}