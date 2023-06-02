/*
 * Copyright (c) 2022 StdEnku
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would be
 *    appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 *    misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
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