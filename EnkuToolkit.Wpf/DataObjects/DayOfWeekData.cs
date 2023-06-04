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

using System;

/// <summary>
/// Data that serves as the source for the day-of-week row in the CustamizableCalendar
/// </summary>
public class DayOfWeekData
{
    /// <summary>
    /// Property to get the target day of the week
    /// </summary>
    public DayOfWeek DayOfWeek { get; }

    /// <summary>
    /// Text property to display as the name of the day of the week
    /// </summary>
    public string DayOfWeekName { get; }

    /// <summary>
    /// Property to get whether the target day of the week is a holiday or not
    /// </summary>
    public bool IsHoliday => DayOfWeek == DayOfWeek.Sunday || DayOfWeek == DayOfWeek.Saturday;

    internal DayOfWeekData(DayOfWeek dayOfWeek, string dayOfWeekName)
    {
        DayOfWeek = dayOfWeek;
        DayOfWeekName = dayOfWeekName;
    }
}