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
namespace EnkuToolkit.UiIndependent.Collections;

using EnkuToolkit.UiIndependent.DataObjects;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

/// <summary>
/// Type representing a collection to manage the BaseDaySource object, the data source for the CustamizableCalendar
/// </summary>
public class DayDataCollection : ObservableCollection<BaseDayData>
{
    /// <summary>
    /// Property to get the year
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Property to get the month
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="year">The year you want to display in the calendar</param>
    /// <param name="month">The month you want to display in the calendar</param>
    /// <exception cref="ArgumentOutOfRangeException">Exception thrown when a year and month outside the range allowed by DateTime are specified</exception>
    public DayDataCollection(int year, int month)
    {
        if (year > DateTime.MaxValue.Year || year < DateTime.MinValue.Year)
            throw new ArgumentOutOfRangeException(nameof(year), $"The possible values for the argument year must be between {DateTime.MinValue.Year} and {DateTime.MaxValue.Year}.");

        if (month > DateTime.MaxValue.Month || month < DateTime.MinValue.Month)
            throw new ArgumentOutOfRangeException(nameof(month), $"The argument month must be a value between {DateTime.MinValue.Month} and {DateTime.MaxValue.Month}.");

        Year = year;
        Month = month;
    }

    /// <summary>
    /// Processing that occurs when a collection is modified
    /// </summary>
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
        {
            var newItem = e.NewItems?.Cast<BaseDayData>().FirstOrDefault();
            Debug.Assert(newItem is not null);

            if (!DateTime.TryParse($"{Year}/{Month}/{newItem.Day}", out var _))
                throw new InvalidOperationException($"Added {Year}/{Month}/{newItem.Day} is an invalid date");

            var isDupplicated = (from i in this
                                 group i by i.Day into g
                                 where g.Count() > 1
                                 select g).FirstOrDefault() is not null;

            if (isDupplicated)
                throw new InvalidOperationException("Duplicate dates are about to be registered");

            newItem.ParentWeakReference = new WeakReference<DayDataCollection>(this);
        }
        base.OnCollectionChanged(e);
    }

    /// <summary>
    /// Method to retrieve a new DayDataCollection object for the next month
    /// </summary>
    /// <returns>New DayDataCollection object for the next month</returns>
    /// <exception cref="InvalidOperationException">Exceptions thrown when called with the largest year and month state that can be handled</exception>
    public DayDataCollection CreateNextMonth()
    {
        var dateTime = new DateTime(Year, Month, 1);

        if (dateTime.Year >= DateTime.MaxValue.Year && dateTime.Month >= DateTime.MaxValue.Month)
            throw new InvalidOperationException($"Dates after {DateTime.MaxValue.Date.ToString()} are not supported in DayDatacollection.");

        var nextMonth = dateTime.AddMonths(1);
        return new DayDataCollection(nextMonth.Year, nextMonth.Month);
    }

    /// <summary>
    /// Method to retrieve a new DayDataCollection object for the previous month
    /// </summary>
    /// <returns>New DayDataCollection object for the previous month</returns>
    /// <exception cref="InvalidOperationException">Exception thrown when called with the smallest year and month that can be handled</exception>
    public DayDataCollection CreateLastMonth()
    {
        var dateTime = new DateTime(Year, Month, 1);

        if (dateTime.Year <= DateTime.MinValue.Year && dateTime.Month <= DateTime.MinValue.Month)
            throw new InvalidOperationException($"Dates prior {DateTime.MinValue.Date.ToString()} are not supported in DayDatacollection.");

        var nextMonth = dateTime.AddMonths(-1);
        return new DayDataCollection(nextMonth.Year, nextMonth.Month);
    }
}