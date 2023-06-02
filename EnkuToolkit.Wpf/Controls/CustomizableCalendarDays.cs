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
namespace EnkuToolkit.Wpf.Controls;

using EnkuToolkit.UiIndependent.Collections;
using EnkuToolkit.UiIndependent.DataObjects;
using EnkuToolkit.Wpf.DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

internal class CustomizableCalendarDays : Control
{
    #region Source
    public static readonly DependencyProperty SourceProperty
        = DependencyProperty.Register(
            nameof(Source),
            typeof(DayDataCollection),
            typeof(CustomizableCalendarDays),
            new PropertyMetadata(null, OnSourceChanged)
        );

    public DayDataCollection Source
    {
        get => (DayDataCollection)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var calendarDays = (CustomizableCalendarDays)d;
        var newValue = e.NewValue as DayDataCollection;
        var oldValue = e.OldValue as DayDataCollection;

        if (newValue is not null)
            newValue.CollectionChanged += calendarDays.OnNewValueCollectionChanged;
        if (oldValue is not null)
            oldValue.CollectionChanged -= calendarDays.OnNewValueCollectionChanged;

        if (calendarDays.IsLoaded)
        {
            if (newValue is null || oldValue is null)
            {
                calendarDays.Update(CalendarDaysUpdatedType.Reload);
                return;
            }
            

            var newDate = new DateTime(newValue.Year, newValue.Month, 1).Date;
            var oldDate = new DateTime(oldValue.Year, oldValue.Month, 1).Date;

            var updateType = newDate > oldDate ? CalendarDaysUpdatedType.Forward :
                             newDate < oldDate ? CalendarDaysUpdatedType.Backward :
                             CalendarDaysUpdatedType.Reload;

            calendarDays.Update(updateType);
        }
    }

    private void OnNewValueCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        Update(CalendarDaysUpdatedType.Reload);
    }
    #endregion

    #region IsStartMonday
    public static readonly DependencyProperty IsStartMondayProperty
        = DependencyProperty.Register(
            nameof(IsStartMonday),
            typeof(bool),
            typeof(CustomizableCalendarDays),
            new PropertyMetadata(true, OnIsStartMondayChanged)
        );

    public bool IsStartMonday
    {
        get => (bool)GetValue(IsStartMondayProperty);
        set => SetValue(IsStartMondayProperty, value);
    }

    private static void OnIsStartMondayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var calendarDays = (CustomizableCalendarDays)d;
        if (calendarDays.IsLoaded)
            calendarDays.Update(CalendarDaysUpdatedType.Reload);
    }
    #endregion

    #region HasDataCellTemplate
    public static readonly DependencyProperty HasDataCellTemplateProperty
        = DependencyProperty.Register(
            nameof(HasDataCellTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendarDays),
            new PropertyMetadata(null, OnHasDataCellTemplateChanged)
        );

    public DataTemplate HasDataCellTemplate
    {
        get => (DataTemplate)GetValue(HasDataCellTemplateProperty);
        set => SetValue(HasDataCellTemplateProperty, value);
    }

    private static void OnHasDataCellTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var calendarDays = (CustomizableCalendarDays)d;
        if (calendarDays.IsLoaded)
            calendarDays.Update(CalendarDaysUpdatedType.Reload);
    }
    #endregion

    #region AutoGenCellTemplate
    public static readonly DependencyProperty AutoGenCellTemplateProperty
        = DependencyProperty.Register(
            nameof(AutoGenCellTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendarDays),
            new PropertyMetadata(null, OnAutoGenCellTemplateChanged)
        );

    public DataTemplate AutoGenCellTemplate
    {
        get => (DataTemplate)GetValue(AutoGenCellTemplateProperty);
        set => SetValue(AutoGenCellTemplateProperty, value);
    }

    private static void OnAutoGenCellTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var calendarDays = (CustomizableCalendarDays)d;
        if (calendarDays.IsLoaded)
            calendarDays.Update(CalendarDaysUpdatedType.Reload);
    }
    #endregion

    #region OtherMonthCellTemplate
    public static readonly DependencyProperty OtherMonthCellTemplateProperty
        = DependencyProperty.Register(
            nameof(OtherMonthCellTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendarDays),
            new PropertyMetadata(null, OnOtherMonthCellTemplateChanged)
        );

    public DataTemplate OtherMonthCellTemplate
    {
        get => (DataTemplate)GetValue(OtherMonthCellTemplateProperty);
        set => SetValue(OtherMonthCellTemplateProperty, value);
    }

    private static void OnOtherMonthCellTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var calendarDays = (CustomizableCalendarDays)d;
        if (calendarDays.IsLoaded)
            calendarDays.Update(CalendarDaysUpdatedType.Reload);
    }
    #endregion

    #region For update methods
    private const int CellRowNum = 6; //Number of rows in calendar cell
    private const int CellColumnNum = 7; //Number of columns in calendar cell
    private const int CellsNum = CellRowNum * CellColumnNum; //Number of all cells on the calendar

    // Methods for retrieving a range of dates to be displayed in the calendar
    internal static IEnumerable<DateTime> DatesOnAPage(int year, int month, DayOfWeek startDayOfWeek)
    {
        var firstDateOfMonth = new DateTime(year, month, 1);
        var firstDayOfWeek = firstDateOfMonth.DayOfWeek;
        var subLastMonday = startDayOfWeek - firstDayOfWeek;
        var firstDate = firstDateOfMonth.AddDays(subLastMonday);
        var endDate = firstDate.AddDays(CellsNum);

        for (var dateTime = firstDate; dateTime.Date < endDate.Date; dateTime = dateTime.AddDays(1))
            yield return dateTime;
    }

    private void Update(CalendarDaysUpdatedType updatedType)
    {
        var custamizableCalendar = (CustomizableCalendar)TemplatedParent;
        if (custamizableCalendar.IsLoaded)
            Updating?.Invoke(this, EventArgs.Empty);

        var targetYear = Source.Year;
        var targetMonth = Source.Month;
        var startWeek = IsStartMonday ? DayOfWeek.Monday : DayOfWeek.Sunday;
        var datesOnAPage = DatesOnAPage(targetYear, targetMonth, startWeek);

        bool isTargetYearAndMonth;
        object? dayData;
        DataTemplate template;
        ListBoxItem currentCell;
        foreach (var itr in datesOnAPage.Zip(_calendarCellItems, (dateTime, cell) => new { DateTime = dateTime, Cell = cell }))
        {
            isTargetYearAndMonth = itr.DateTime.Year == targetYear && itr.DateTime.Month == targetMonth;

            dayData = (from sourceItem in Source
                       where sourceItem.DateTime.Date == itr.DateTime.Date
                       select sourceItem).FirstOrDefault();

            template = !isTargetYearAndMonth ? OtherMonthCellTemplate :
                       dayData is null ? AutoGenCellTemplate : 
                       HasDataCellTemplate;

            dayData ??= new AutoGenDayData(itr.DateTime, isTargetYearAndMonth);

            /*======================================================================================
            // The following will give very good performance without any binding errors, 
            // but perhaps it is a defect on the WPF side, so we will not adopt this method, 
            // but leave it as a comment.
            currentCell = itr.Cell;
            currentCell.ContentTemplate = template;
            currentCell.Content = dayData;
            currentCell.IsEnabled = isTargetYearAndMonth;
            currentCell.IsSelected = false;
            ======================================================================================*/

            currentCell = itr.Cell;
            currentCell.ContentTemplate = AutoGenCellTemplate; // Description to avoid binding errors in the template
            currentCell.Content = dayData;
            currentCell.ContentTemplate = template;
            currentCell.IsEnabled = isTargetYearAndMonth;
            currentCell.IsSelected = false;
        }

        if (custamizableCalendar.IsLoaded)
            Updated?.Invoke(this, new CalendarDaysUpdatedEventArgs(updatedType));
    }
    #endregion

    #region CellMargin
    public static readonly DependencyProperty CellMarginProperty
        = DependencyProperty.Register(
            nameof(CellMargin),
            typeof(Thickness),
            typeof(CustomizableCalendarDays),
            new FrameworkPropertyMetadata(new Thickness(0))
        );

    public Thickness CellMargin
    {
        get => (Thickness)GetValue(CellMarginProperty);
        set => SetValue(CellMarginProperty, value);
    }
    #endregion

    #region CellPadding
    public static readonly DependencyProperty CellPaddingProperty
        = DependencyProperty.Register(
            nameof(CellPadding),
            typeof(Thickness),
            typeof(CustomizableCalendarDays),
            new FrameworkPropertyMetadata(new Thickness(0))
        );

    public Thickness CellPadding
    {
        get => (Thickness)GetValue(CellPaddingProperty);
        set => SetValue(CellPaddingProperty, value);
    }
    #endregion

    #region CellBorderThickness
    public static readonly DependencyProperty CellBorderThicknessProperty
        = DependencyProperty.Register(
            nameof(CellBorderThickness),
            typeof(Thickness),
            typeof(CustomizableCalendarDays),
            new FrameworkPropertyMetadata(new Thickness(0))
        );

    public Thickness CellBorderThickness
    {
        get => (Thickness)GetValue(CellBorderThicknessProperty);
        set => SetValue(CellBorderThicknessProperty, value);
    }
    #endregion

    #region CellBorderBrush
    public static readonly DependencyProperty CellBorderBrushProperty
        = DependencyProperty.Register(
            nameof(CellBorderBrush),
            typeof(Brush),
            typeof(CustomizableCalendarDays),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent))
        );

    public Brush CellBorderBrush
    {
        get => (Brush)GetValue(CellBorderBrushProperty);
        set => SetValue(CellBorderBrushProperty, value);
    }
    #endregion

    #region SelectedDates
    public static readonly DependencyProperty SelectedDatesProperty
        = DependencyProperty.Register(
            nameof(SelectedDates),
            typeof(IEnumerable<DateTime>),
            typeof(CustomizableCalendarDays),
            new PropertyMetadata(new List<DateTime>(), OnSelectedDatesChaned)
        );

    public IEnumerable<DateTime> SelectedDates
    {
        get => (IEnumerable<DateTime>)GetValue(SelectedDatesProperty);
        set => SetValue(SelectedDatesProperty, value);
    }

    private bool _isSourceChanged = false;
    private bool _isTargetChanged = false;

    private static void OnSelectedDatesChaned(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var custamizableCalendarDays = (CustomizableCalendarDays)d;

        if (!custamizableCalendarDays.IsLoaded) return;
        if (custamizableCalendarDays._isTargetChanged) return;

        custamizableCalendarDays._isSourceChanged = true;

        #region Process to be executed if the binding source changes from here
        var newValue = (IEnumerable<DateTime>)e.NewValue;

        if (newValue.Count() > 1 && custamizableCalendarDays.SelectionMode == SelectionMode.Single)
            throw new InvalidOperationException("Multiple values cannot be specified in SelectedDaates if the SelectionMode of the CustamizableCalendar is Single. Please make sure that the number of elements in the property to which you are binding is only one or review the binding mode.");

        BaseDayData currentDayData;
        DateTime currentDateTime;
        bool isExistsInNewValue;
        foreach (var item in custamizableCalendarDays._calendarCellItems)
        {
            currentDayData = (BaseDayData)item.Content;
            currentDateTime = currentDayData.DateTime;
            
            isExistsInNewValue = (from dateTime in newValue
                                  where dateTime.Date == currentDateTime.Date
                                  select new { }).FirstOrDefault() is not null;

            item.IsSelected = isExistsInNewValue;
        }
        #endregion

        custamizableCalendarDays._isSourceChanged = false;
    }

    private static void OnListboxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;
        var custamizableCalendarDays = (CustomizableCalendarDays)listBox.TemplatedParent;
        if (custamizableCalendarDays._isSourceChanged) return;
        custamizableCalendarDays._isTargetChanged = true;

        #region Processing when the selection of the listBox control in the template changes from here
        var selectedDates = from item in listBox.SelectedItems.Cast<ListBoxItem>()
                            select ((BaseDayData)item.Content).DateTime;

        custamizableCalendarDays.SelectedDates = selectedDates;
        #endregion

        custamizableCalendarDays._isTargetChanged = false;
    }
    #endregion

    #region SelectionMode
    public static readonly DependencyProperty SelectionModeProperty
        = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(SelectionMode),
            typeof(CustomizableCalendarDays),
            new PropertyMetadata(OnSelectionMode)
        );

    public SelectionMode SelectionMode
    {
        get => (SelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionModeProperty, value);
    }

    private static void OnSelectionMode(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var custamizableCalenarDays = (CustomizableCalendarDays)d;
        custamizableCalenarDays.SelectedDates = new List<DateTime>();
    }
    #endregion

    #region CellDoubleClicked
    public static readonly RoutedEvent CellDoubleClickedEvent
        = EventManager.RegisterRoutedEvent(
            nameof(CellDoubleClicked),
            RoutingStrategy.Bubble,
            typeof(CustomizableCalendar.CellDoubleClickedEventHandler),
            typeof(CustomizableCalendarDays)
        );

    public event CustomizableCalendar.CellDoubleClickedEventHandler CellDoubleClicked
    {
        add { AddHandler(CellDoubleClickedEvent, value); }
        remove { RemoveHandler(CellDoubleClickedEvent, value); }
    }

    private void RaiseCellDoubleClickedEvent(DateTime dateTime)
    {
        var args = new CustomizableCalendar.CellDoubleClickedEventArgs(CellDoubleClickedEvent, dateTime);
        RaiseEvent(args);
    }
    #endregion

    #region Events that occur during updates used by the internal mechanism
    internal enum CalendarDaysUpdatedType
    {
        Forward,
        Backward,
        Reload,
    }

    internal class CalendarDaysUpdatedEventArgs : EventArgs
    {
        public CalendarDaysUpdatedType UpdatedType { get; }

        public CalendarDaysUpdatedEventArgs(CalendarDaysUpdatedType updateType)
            => UpdatedType = updateType;
    }

    internal delegate void CalendarDaysUpdatedEventHandler(DependencyObject d, CalendarDaysUpdatedEventArgs args);

    internal event CalendarDaysUpdatedEventHandler? Updated;

    internal event EventHandler? Updating;
    #endregion

    internal void Reload()
    {
        Update(CalendarDaysUpdatedType.Reload);
    }

    internal bool IsExistsNowDateInShowingDates()
    {
        var showingDates = from item in _calendarCellItems
                           select ((BaseDayData)item.Content).DateTime;

        var isExists = (from date in showingDates
                        where date.Date == DateTime.Now.Date
                        select date).Count() > 0;

        return isExists;
    }

    private IEnumerable<ListBoxItem> _calendarCellItems
    {
        get
        {
            foreach (var row in Enumerable.Range(0, CellRowNum))
                foreach (var column in Enumerable.Range(0, CellColumnNum))
                    yield return (ListBoxItem)GetTemplateChild($"calendarCell{row}Row{column}Column");
        }
    }

    private ListBox _listBoxInTemplate => (ListBox)GetTemplateChild("listBox");

    public override void OnApplyTemplate()
    {
        Update(CalendarDaysUpdatedType.Reload);
        var listbox = _listBoxInTemplate;
        OnSelectedDatesChaned(this, new DependencyPropertyChangedEventArgs(SelectedDatesProperty, null, this.SelectedDates));
        listbox.SelectionChanged += OnListboxSelectionChanged;

        foreach (var item in _calendarCellItems)
            item.MouseDoubleClick += OnCellItemsMouseDoubleClick;

        base.OnApplyTemplate();
    }

    private static void OnCellItemsMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
        {
            var listBoxItem = (ListBoxItem)sender;
            var custamizableCalendarDays = (CustomizableCalendarDays)listBoxItem.TemplatedParent;
            var dayData = (BaseDayData)listBoxItem.Content;
            var date = dayData.DateTime;
            custamizableCalendarDays.RaiseCellDoubleClickedEvent(date);
        }
    }

    static CustomizableCalendarDays()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomizableCalendarDays),
            new FrameworkPropertyMetadata(typeof(CustomizableCalendarDays))
        );
    }
}