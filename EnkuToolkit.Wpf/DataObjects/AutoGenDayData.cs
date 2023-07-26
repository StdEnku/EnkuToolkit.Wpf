/*
* MIT License
* 
* Copyright (c) 2023 StdEnku
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
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