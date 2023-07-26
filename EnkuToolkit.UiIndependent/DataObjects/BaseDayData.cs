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
namespace EnkuToolkit.UiIndependent.DataObjects;

using System.Diagnostics;
using EnkuToolkit.UiIndependent.Collections;

/// <summary>
/// Type representing data source for CustamizableCalendar
/// </summary>
public class BaseDayData
{
    /// <summary>
    /// Property to get the day
    /// </summary>
    public virtual int Day { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="day">Target Date</param>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown if the date specified in the argument is not a value between 1 and 31</exception>
    public BaseDayData(int day)
    {
        if (day < 1 || day > 31)
            throw new ArgumentOutOfRangeException(nameof(day), "Date must be in the range of 1 to 31");
        Day = day;
    }

    /// <summary>
    /// Property to get a weak reference to the DaySourceCollection object that is the parent managing this object
    /// </summary>
    internal WeakReference<DayDataCollection>? ParentWeakReference { get; set; }

    private DayDataCollection Parent
    {
        get
        {
            if (ParentWeakReference is null) throw new NullReferenceException("This object has no parent DaySourceCollection object. This property must be added to the DaySourceCollection object before it can be used.");
            var assetFlag = ParentWeakReference.TryGetTarget(out var parent);
            Debug.Assert(assetFlag);
            Debug.Assert(parent is not null);
            return parent;
        }
    }

    /// <summary>
    /// Property to get whether the target date matches today's date
    /// </summary>
    public virtual bool IsToday => DateTime.Date == DateTime.Now.Date;

    /// <summary>
    /// Property to get whether the target day is a holiday or not
    /// </summary>
    public virtual bool IsHoliday
    {
        get
        {
            var date = DateTime;
            var dayOfWeek = date.DayOfWeek;
            return dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Saturday;
        }
    }

    /// <summary>
    /// Property to get a DateTime object for the target date
    /// </summary>
    public virtual DateTime DateTime
    {
        get
        {
            var parent = Parent;
            var date = new DateTime(parent.Year, parent.Month, Day);
            return date;
        }
    }

    /// <summary>
    /// Property to get the year
    /// </summary>
    public virtual int Year => DateTime.Year;

    /// <summary>
    /// Property to get the month
    /// </summary>
    public virtual int Month => DateTime.Month;
}