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
        if (e.Action == NotifyCollectionChangedAction.Add || 
            e.Action == NotifyCollectionChangedAction.Replace)
        {
            var newItem = e.NewItems?.Cast<BaseDayData>().FirstOrDefault();
            Debug.Assert(newItem is not null);
            ValidItem(newItem);
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

    /// <summary>
    /// Method to retrieve a new DayDataCollection object equal to the current year and month.
    /// </summary>
    /// <returns>New DayDataCollection object equal to the current year and month</returns>
    public DayDataCollection CreateSameMonth()
    {
        return new DayDataCollection(Year, Month);
    }

    /// <summary>
    /// Method for adding multiple items at once
    /// </summary>
    /// <param name="addItems">Items to be added</param>
    public void AddRange(IEnumerable<BaseDayData> addItems)
    {
        foreach (var item in addItems)
        {
            Items.Add(item);
            ValidItem(item);
            item.ParentWeakReference = new WeakReference<DayDataCollection>(this);
        }

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void ValidItem(BaseDayData newItem)
    {
        if (!DateTime.TryParse($"{Year}/{Month}/{newItem.Day}", out var _))
            throw new InvalidOperationException($"Added {Year}/{Month}/{newItem.Day} is an invalid date");

        var isDupplicated = (from i in this
                             group i by i.Day into g
                             where g.Count() > 1
                             select g).FirstOrDefault() is not null;

        if (isDupplicated)
            throw new InvalidOperationException("Duplicate dates are about to be registered");
    }
}