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