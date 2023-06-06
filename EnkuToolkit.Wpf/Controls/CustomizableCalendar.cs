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
namespace EnkuToolkit.Wpf.Controls;

using EnkuToolkit.UiIndependent.Collections;
using EnkuToolkit.UiIndependent.DataObjects;
using EnkuToolkit.Wpf.Constants;
using EnkuToolkit.Wpf.DataObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Timers;
using System.Diagnostics;

/// <summary>
/// Customizable calendar control for cells in the calendar
/// </summary>
public class CustomizableCalendar : Control
{
    #region Dependency property to specify whether the first day of the week in the column should be Monday
    /// <summary>
    /// Dependency property to specify whether the first day of the week in the column should be Monday
    /// </summary>
    public static readonly DependencyProperty IsStartMondayProperty
        = DependencyProperty.Register(
            nameof(IsStartMonday),
            typeof(bool),
            typeof(CustomizableCalendar), 
            new PropertyMetadata(true, OnIsStartMondayChanged)
        );

    /// <summary>
    /// CLR property for IsStartMondayProperty, a dependency property for specifying whether the first day of the week in a column is Monday
    /// </summary>
    public bool IsStartMonday
    {
        get => (bool)GetValue(IsStartMondayProperty);
        set => SetValue(IsStartMondayProperty, value);
    }

    private static void OnIsStartMondayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var customizableCalendar = (CustomizableCalendar)d;

        if (customizableCalendar.IsLoaded)
        {
            customizableCalendar.UpdateDayOfWeeksLine(UpdateEffectType.None);
            customizableCalendar.UpdateDayOfCell(UpdateEffectType.Reflesh);
        }
    }
    #endregion

    #region Dependency property for specifying CultureInfo to determine the name of the day of the week
    /// <summary>
    /// Dependency property for specifying CultureInfo to determine the name of the day of the week
    /// </summary>
    public static readonly DependencyProperty DayOfWeeksCultureProperty
        = DependencyProperty.Register(
            nameof(DayOfWeeksCulture),
            typeof(CultureInfo),
            typeof(CustomizableCalendar),
            new PropertyMetadata(CultureInfo.CurrentCulture, OnDayOfWeeksCultureChanged)
        );

    /// <summary>
    /// CLR property for DayOfWeeksCultureProperty, a dependency property for specifying CultureInfo to determine the name of the day of the week
    /// </summary>
    public CultureInfo DayOfWeeksCulture
    {
        get => (CultureInfo)GetValue(DayOfWeeksCultureProperty);
        set => SetValue(DayOfWeeksCultureProperty, value);
    }

    private static void OnDayOfWeeksCultureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var customizableCalendar = (CustomizableCalendar)d;

        if (customizableCalendar.IsLoaded)
            customizableCalendar.UpdateDayOfWeeksLine(UpdateEffectType.Reflesh);
    }
    #endregion

    #region Dependency property for specifying the horizontal dpi value of the image created when this control is imaged in the effect executed at update time.
    /// <summary>
    /// Dependency property for specifying the horizontal dpi value of the image created when this control is imaged in the effect executed at update time.
    /// </summary>
    public static readonly DependencyProperty SnapshotDpiXProperty
        = DependencyProperty.Register(
            nameof(SnapshotDpiX),
            typeof(int),
            typeof(CustomizableCalendar),
            new PropertyMetadata(96)
        );

    /// <summary>
    /// CLR property for SnapshotDpiXProperty, which is a dependency property for specifying the horizontal dpi value of the image created when this control is imaged in the effect executed at update time.
    /// </summary>
    public int SnapshotDpiX
    {
        get => (int)GetValue(SnapshotDpiXProperty);
        set => SetValue(SnapshotDpiXProperty, value);
    }
    #endregion

    #region Dependency property for specifying the vertical dpi value of the image created when this control is imaged in the effect executed at update time.
    /// <summary>
    /// Dependency property for specifying the vertical dpi value of the image created when this control is imaged in the effect executed at update time.
    /// </summary>
    public static readonly DependencyProperty SnapshotDpiYProperty
        = DependencyProperty.Register(
            nameof(SnapshotDpiY),
            typeof(int),
            typeof(CustomizableCalendar),
            new PropertyMetadata(96)
        );

    /// <summary>
    /// CLR property for SnapshotDpiYProperty, which is a dependency property for specifying the vertical dpi value of the image created by the effect that is executed when updating this control to create an image.
    /// </summary>
    public int SnapshotDpiY
    {
        get => (int)GetValue(SnapshotDpiYProperty);
        set => SetValue(SnapshotDpiYProperty, value);
    }
    #endregion

    #region Dependency property to specify the Storyboard to be executed as an effect when attempting to display the year/month after the currently displayed month
    /// <summary>
    /// Dependency property to specify the Storyboard to be executed as an effect when attempting to display the year/month after the currently displayed month
    /// </summary>
    /// <remarks>
    /// Ignored if Custom is not specified for the TransitionEffect value
    /// </remarks>
    public static readonly DependencyProperty ForwardStoryboardProperty
        = DependencyProperty.Register(
            nameof(ForwardStoryboard),
            typeof(Storyboard),
            typeof(CustomizableCalendar),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for ForwardStoryboardProperty, a dependency property for specifying the Storyboard to run as an effect when attempting to display the year/month after the currently displayed month
    /// </summary>
    public Storyboard? ForwardStoryboard
    {
        get => GetValue(ForwardStoryboardProperty) as Storyboard;
        set => SetValue(ForwardStoryboardProperty, value);
    }
    #endregion

    #region Dependency property to specify the Storyboard to run as an effect when attempting to display a year/month prior to the currently displayed month
    /// <summary>
    /// Dependency property to specify the Storyboard to run as an effect when attempting to display a year/month prior to the currently displayed month
    /// </summary>
    /// <remarks>
    /// Ignored if Custom is not specified for the TransitionEffect value
    /// </remarks>
    public static readonly DependencyProperty BackwardStoryboardProperty
        = DependencyProperty.Register(
            nameof(BackwardStoryboard),
            typeof(Storyboard),
            typeof(CustomizableCalendar),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for BackwardStoryboardProperty, a dependency property for specifying the Storyboard to run as an effect when attempting to display a year/month prior to the currently displayed month
    /// </summary>
    public Storyboard? BackwardStoryboard
    {
        get => GetValue(BackwardStoryboardProperty) as Storyboard;
        set => SetValue(BackwardStoryboardProperty, value);
    }
    #endregion

    #region Dependency property to specify the Storyboard to be executed as an effect when an update is made to the screen without changing the month being displayed
    /// <summary>
    /// Dependency property to specify the Storyboard to be executed as an effect when an update is made to the screen without changing the month being displayed
    /// </summary>
    /// <remarks>
    /// Ignored if Custom is not specified for the TransitionEffect value
    /// </remarks>
    public static readonly DependencyProperty ReloadStoryboardProperty
        = DependencyProperty.Register(
            nameof(ReloadStoryboard),
            typeof(Storyboard),
            typeof(CustomizableCalendar),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CLR property for ReloadStoryboardProperty, a dependency property for specifying the Storyboard to be executed as an effect when an update is made to the screen without changing the month being displayed
    /// </summary>
    public Storyboard? ReloadStoryboard
    {
        get => GetValue(ReloadStoryboardProperty) as Storyboard;
        set => SetValue(ReloadStoryboardProperty, value);
    }
    #endregion

    #region Dependency property for specifying the type of effect to be executed when the screen is refreshed
    /// <summary>
    /// Dependency property for specifying the type of effect to be executed when the screen is refreshed
    /// </summary>
    public static readonly DependencyProperty TransitionEffectProperty
        = DependencyProperty.Register(
            nameof(TransitionEffect),
            typeof(TransitionEffects),
            typeof(CustomizableCalendar),
            new PropertyMetadata(TransitionEffects.None)
        );

    /// <summary>
    /// CLR property for TransitionEffectProperty, a dependency property for specifying the type of effect to be performed during screen updates
    /// </summary>
    public TransitionEffects TransitionEffect
    {
        get => (TransitionEffects)GetValue(TransitionEffectProperty);
        set => SetValue(TransitionEffectProperty, value);
    }
    #endregion

    #region Dependency property that is True if the effect to be executed on screen refresh is complete
    /// <summary>
    /// Dependency property that is True if the effect to be executed on screen refresh is complete
    /// </summary>
    public static readonly DependencyProperty IsEffectCompletedProperty
        = DependencyProperty.Register(
            nameof(IsEffectCompleted),
            typeof(bool),
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

    /// <summary>
    /// CLR property for IsCompletedProperty, a dependency property that is True if the effect performed on screen refresh is complete
    /// </summary>
    public bool IsEffectCompleted
    {
        get => (bool)GetValue(IsEffectCompletedProperty);
        set => SetValue(IsEffectCompletedProperty, value);
    }
    #endregion

    #region Dependency property for specifying the selection mode
    /// <summary>
    /// Dependency property for specifying the selection mode
    /// </summary>
    public static readonly DependencyProperty SelectionModeProperty
        = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(SelectionMode),
            typeof(CustomizableCalendar)
        );

    /// <summary>
    /// CLR property for SelectionModeProperty, a dependency property for specifying the selection mode
    /// </summary>
    public SelectionMode SelectionMode
    {
        get => (SelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionModeProperty, value);
    }
    #endregion

    #region Template property to specify the appearance of cells for date display
    /// <summary>
    /// property representing the template to be applied in the day-of-week display line
    /// </summary>
    /// <remarks>
    /// If Null is specified, the standard built-in template is applied
    /// </remarks>
    public DataTemplate? DayOfWeekLineTemplate { get; init; }

    /// <summary>
    /// Template property for specifying the appearance of cells when data is specified in Source
    /// </summary>
    /// <remarks>
    /// If Null is specified, the standard built-in template is applied
    /// </remarks>
    public DataTemplate? HasDataCellTemplate { get; init; }

    /// <summary>
    /// Template property for specifying the appearance of cells when no data is specified in Source
    /// </summary>
    /// <remarks>
    /// If Null is specified, the standard built-in template is applied
    /// </remarks>
    public DataTemplate? AutoGenCellTemplate { get; init; }

    /// <summary>
    /// Template property for specifying the appearance of cells for months not covered
    /// </summary>
    /// <remarks>
    /// If Null is specified, the standard built-in template is applied
    /// </remarks>
    public DataTemplate? OtherMonthCellTemplate { get; init; }
    #endregion

    #region Dependency property that is the source of the calendar cell
    /// <summary>
    /// Dependency property that is the source of the calendar cell
    /// </summary>
    public static readonly DependencyProperty SourceProperty
        = DependencyProperty.Register(
            nameof(Source),
            typeof(DayDataCollection),
            typeof(CustomizableCalendar),
            new PropertyMetadata(new DayDataCollection(DateTime.Now.Year, DateTime.Now.Month), OnSourceChanged)
        );

    /// <summary>
    /// CLR property of the dependency property SourceProperty, which is the Source of the calendar cell
    /// </summary>
    public DayDataCollection Source
    {
        get => (DayDataCollection)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var customizableCalendar = (CustomizableCalendar)d;
        var newValue = (DayDataCollection)e.NewValue;
        var oldValue = (DayDataCollection)e.OldValue;

        newValue.CollectionChanged += customizableCalendar.OnNewValueCollectionChanged;
        oldValue.CollectionChanged -= customizableCalendar.OnNewValueCollectionChanged;

        if (customizableCalendar.IsLoaded)
        {
            var newDate = new DateTime(newValue.Year, newValue.Month, 1).Date;
            var oldDate = new DateTime(oldValue.Year, oldValue.Month, 1).Date;

            var updateType = newDate > oldDate ? UpdateEffectType.Forward :
                             newDate < oldDate ? UpdateEffectType.Backward :
                             UpdateEffectType.Reflesh;

            customizableCalendar.UpdateDayOfCell(updateType);
        }
    }

    private void OnNewValueCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateDayOfCell(UpdateEffectType.Reflesh);
    }
    #endregion

    #region Methods for updating display content
    private void UpdateDayOfWeeksLine(UpdateEffectType updateEffectType)
    {
        if (updateEffectType != UpdateEffectType.None)
            _transitionEffectContentControl.Snapshot();
        var dayOfWeekNames = (IEnumerable<string>)DayOfWeeksCulture.DateTimeFormat.AbbreviatedDayNames;
        var dayOfWeeks = (IEnumerable<DayOfWeek>)Enum.GetValues<DayOfWeek>();

        if (IsStartMonday)
        {
            var dayOfWeekNamesList = dayOfWeekNames.ToList();
            var sunName = dayOfWeekNamesList[0];
            dayOfWeekNamesList.RemoveAt(0);
            dayOfWeekNamesList.Add(sunName);
            dayOfWeekNames = dayOfWeekNamesList;

            var dayOfWeeksList = dayOfWeeks.ToList();
            var sun = dayOfWeeksList[0];
            dayOfWeeksList.RemoveAt(0);
            dayOfWeeksList.Add(sun);
            dayOfWeeks = dayOfWeeksList;
        }

        var daysOfWeekDatas = dayOfWeekNames.Zip(dayOfWeeks, (name, week) => new DayOfWeekData(week, name));
        var datasWithCells = daysOfWeekDatas.Zip(_dayOfWeekLineCells, (dayOfWeekData, cell) => new { dayOfWeekData, cell });

        foreach (var dataWithCell in datasWithCells)
        {
            dataWithCell.cell.Content = dataWithCell.dayOfWeekData;
            dataWithCell.cell.ContentTemplate = DayOfWeekLineTemplate ?? _dayOfWeekLineDefaultTemplate;
        }
        
        RunEffect(updateEffectType);
    }

    private void UpdateDayOfCell(UpdateEffectType updateEffectType)
    {
        if (updateEffectType != UpdateEffectType.None)
            _transitionEffectContentControl.Snapshot();

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

            template = !isTargetYearAndMonth ? OtherMonthCellTemplate ?? _defaultCellTemplate :
                       dayData is null ? AutoGenCellTemplate ?? _defaultCellTemplate :
                       HasDataCellTemplate ?? _defaultCellTemplate;

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

        RunEffect(updateEffectType);
    }
    #endregion

    #region Properties for accessing controls on the template
    private ListBox _dayOfCells => (ListBox)GetTemplateChild("dayOfCells");

    private TransitionEffectContentControl _transitionEffectContentControl => (TransitionEffectContentControl)GetTemplateChild("transitionEffectContentControl");

    private DataTemplate _defaultCellTemplate => (DataTemplate)_dayOfCells.Resources["DefaultCellTemplate"];

    private IEnumerable<ListBoxItem> _calendarCellItems
    {
        get
        {
            foreach (var row in Enumerable.Range(0, CellRowNum))
                foreach (var column in Enumerable.Range(0, CellColumnNum))
                    yield return (ListBoxItem)GetTemplateChild($"calendarCell{row}Row{column}Column");
        }
    }

    private IEnumerable<ContentControl> _dayOfWeekLineCells
    {
        get
        {
            foreach (var column in Enumerable.Range(0, CellColumnNum))
                yield return (ContentControl)GetTemplateChild($"dayOfWeekLine{column}");
        }
    }

    private UniformGrid _dayOfWeekLine => (UniformGrid)GetTemplateChild("dayOfWeekLine");

    private DataTemplate _dayOfWeekLineDefaultTemplate => (DataTemplate)_dayOfWeekLine.Resources["dayOfWeekLineDefaultTemplate"];
    #endregion

    #region Dependency property to get the currently selected item
    /// <summary>
    /// Dependency property to get the currently selected item
    /// </summary>
    public static readonly DependencyProperty SelectedDatesProperty
        = DependencyProperty.Register(
            nameof(SelectedDates),
            typeof(IEnumerable<DateTime>),
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(new List<DateTime>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDatesChanged)
        );

    /// <summary>
    /// CLR property for SelectedDatesProperty, a dependency property for retrieving the currently selected item
    /// </summary>
    public IEnumerable<DateTime> SelectedDates
    {
        get => (IEnumerable<DateTime>)GetValue(SelectedDatesProperty);
        set => SetValue(SelectedDatesProperty, value);
    }

    private bool _isSourceChanged = false;
    private bool _isTargetChanged = false;

    private static void OnSelectedDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var customizableCalendar = (CustomizableCalendar)d;

        if (!customizableCalendar.IsLoaded) return;
        if (customizableCalendar._isTargetChanged) return;

        customizableCalendar._isSourceChanged = true;

        #region Process to be executed if the binding source changes from here
        var newValue = (IEnumerable<DateTime>)e.NewValue;

        if (newValue.Count() > 1 && customizableCalendar.SelectionMode == SelectionMode.Single)
            throw new InvalidOperationException("Multiple values cannot be specified in SelectedDaates if the SelectionMode of the CustamizableCalendar is Single. Please make sure that the number of elements in the property to which you are binding is only one or review the binding mode.");

        BaseDayData currentDayData;
        DateTime currentDateTime;
        bool isExistsInNewValue;
        foreach (var item in customizableCalendar._calendarCellItems)
        {
            currentDayData = (BaseDayData)item.Content;
            currentDateTime = currentDayData.DateTime;

            isExistsInNewValue = (from dateTime in newValue
                                  where dateTime.Date == currentDateTime.Date
                                  select new { }).FirstOrDefault() is not null;

            item.IsSelected = isExistsInNewValue;
        }
        #endregion

        customizableCalendar._isSourceChanged = false;
    }

    private static void OnDayOfCellsSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;
        var custamizableCalendar = (CustomizableCalendar)listBox.TemplatedParent;
        if (custamizableCalendar._isSourceChanged) return;
        custamizableCalendar._isTargetChanged = true;

        #region Processing when the selection of the listBox control in the template changes from here
        var selectedDates = from item in listBox.SelectedItems.Cast<ListBoxItem>()
                            select ((BaseDayData)item.Content).DateTime;

        custamizableCalendar.SelectedDates = selectedDates;
        #endregion

        custamizableCalendar._isTargetChanged = false;
    }
    #endregion

    #region routed event fired when a cell is double-clicked
    /// <summary>
    /// Event argument that returns the date of the double-click for an event that fires when a cell in the CustamizableCalendar is double-clicked.
    /// </summary>
    public class CellDoubleClickedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Date double-clicked
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Type of button on which the double-click was performed
        /// </summary>
        public MouseButton ClickedButton { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="routedEvent">The routed event identifier for this instance of the RoutedEventArgs class</param>
        /// <param name="dateTime">Date double-clicked</param>
        /// <param name="clickedButton">Type of button on which the double-click was performed</param>
        public CellDoubleClickedEventArgs(RoutedEvent routedEvent, DateTime dateTime, MouseButton clickedButton) : base(routedEvent)
        {
            DateTime = dateTime;
            ClickedButton = clickedButton;
        }
    }

    /// <summary>
    /// Handler for CellDoubleClickedEvent, which fires when a calendar cell is double-clicked.
    /// </summary>
    /// <param name="sender">Event Originator</param>
    /// <param name="e">Event argument containing the date of the double click</param>
    public delegate void CellDoubleClickedEventHandler(object sender, CellDoubleClickedEventArgs e);

    /// <summary>
    /// CustomizableCalendarのセルがダブルクリックされた際に実行されるRoted event
    /// </summary>
    public static readonly RoutedEvent CellDoubleClickedEvent
       = EventManager.RegisterRoutedEvent(
           nameof(CellDoubleClicked),
           RoutingStrategy.Bubble,
           typeof(CellDoubleClickedEventHandler),
           typeof(CustomizableCalendar)
       );

    /// <summary>
    /// CustomizableCalendarのセルがダブルクリックされた際に実行されるRoted eventであるCellDoubleClickedEvent用のCLRイベント
    /// </summary>
    public event CellDoubleClickedEventHandler CellDoubleClicked
    {
        add { AddHandler(CellDoubleClickedEvent, value); }
        remove { RemoveHandler(CellDoubleClickedEvent, value); }
    }

    private void RaiseCellDoubleClickedEvent(DateTime dateTime, MouseButton mouseButton)
    {
        var args = new CellDoubleClickedEventArgs(CellDoubleClickedEvent, dateTime, mouseButton);
        RaiseEvent(args);
    }

    private static void OnCellItemsMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var listBoxItem = (ListBoxItem)sender;
        var customizableCalendar = (CustomizableCalendar)listBoxItem.TemplatedParent;
        var dayData = (BaseDayData)listBoxItem.Content;
        var date = dayData.DateTime;
        var mouseButton = e.ChangedButton;
        customizableCalendar.RaiseCellDoubleClickedEvent(date, mouseButton);
        customizableCalendar.CellDoubleClickedCommand?.Execute(date);
    }
    #endregion

    #region Dependency property to specify the command to be executed on double-click
    /// <summary>
    /// Dependency property to specify the command to be executed on double-click
    /// </summary>
    public static readonly DependencyProperty CellDoubleClickedCommandProperty
        = DependencyProperty.Register(
            nameof(CellDoubleClickedCommand),
            typeof(ICommand),
            typeof(CustomizableCalendar)
        );

    /// <summary>
    /// CLR property for CellDoubleClickedCommandProperty, a dependency property for specifying the command to be executed on double-click
    /// </summary>
    public ICommand? CellDoubleClickedCommand
    {
        get => GetValue(CellDoubleClickedCommandProperty) as ICommand;
        set => SetValue(CellDoubleClickedCommandProperty, value);
    }
    #endregion

    #region Dependency property for specifying the width of a cell border
    /// <summary>
    /// Dependency property for specifying the width of a cell border
    /// </summary>
    public static readonly DependencyProperty CellBorderThicknessProperty
        = DependencyProperty.Register(
            nameof(CellBorderThickness),
            typeof(Thickness),
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(new Thickness(0))
        );

    /// <summary>
    /// CLR property for CellBorderThicknessProperty, a dependency property for specifying the width of a cell border
    /// </summary>
    public Thickness CellBorderThickness
    {
        get => (Thickness)GetValue(CellBorderThicknessProperty);
        set => SetValue(CellBorderThicknessProperty, value);
    }
    #endregion

    #region Dependency property for specifying the cell border color
    /// <summary>
    /// Dependency property for specifying the cell border color
    /// </summary>
    public static readonly DependencyProperty CellBorderBrushProperty
        = DependencyProperty.Register(
            nameof(CellBorderBrush),
            typeof(Brush),
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent))
        );

    /// <summary>
    /// CLR property for CellsBorderBrushProperty, a dependency property for specifying the cell border color
    /// </summary>
    public Brush CellBorderBrush
    {
        get => (Brush)GetValue(CellBorderBrushProperty);
        set => SetValue(CellBorderBrushProperty, value);
    }
    #endregion

    #region Dependency property for specifying cell margins
    /// <summary>
    /// Dependency property for specifying cell margins
    /// </summary>
    public static readonly DependencyProperty CellMarginProperty
        = DependencyProperty.Register(
            nameof(CellMargin),
            typeof(Thickness),
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(new Thickness(0))
        );

    /// <summary>
    /// CLR property for CellsMarginProperty, a dependency property for specifying cell margins
    /// </summary>
    public Thickness CellMargin
    {
        get => (Thickness)GetValue(CellMarginProperty);
        set => SetValue(CellMarginProperty, value);
    }
    #endregion

    #region Dependency property for specifying cell padding
    /// <summary>
    /// Dependency property for specifying cell padding
    /// </summary>
    public static readonly DependencyProperty CellPaddingProperty
        = DependencyProperty.Register(
            nameof(CellPadding),
            typeof(Thickness),
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(new Thickness(0))
        );

    /// <summary>
    /// CLR property for CellsPaddingProperty, a dependency property for specifying cell padding
    /// </summary>
    public Thickness CellPadding
    {
        get => (Thickness)GetValue(CellPaddingProperty);
        set => SetValue(CellPaddingProperty, value);
    }
    #endregion

    /// <summary>
    /// Methods for manual updates
    /// </summary>
    public void Update()
    {
        UpdateDayOfCell(UpdateEffectType.Reflesh);
    }

    /// <summary>
    /// Property for specifying whether or not to automatically update when the date changes
    /// </summary>
    public bool IsAutoReloadOnDateChanges { get; init; } = true;

    /// <summary>
    /// Constructor
    /// </summary>
    public CustomizableCalendar()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private bool IsExistsNowDateInShowingDates()
    {
        var showingDates = from item in _calendarCellItems
                           select ((BaseDayData)item.Content).DateTime;

        var isExists = (from date in showingDates
                        where date.Date == DateTime.Now.Date
                        select date).Count() > 0;

        return isExists;
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Debug.Assert(_timer is not null);
        _timer.Stop();
        _timer.Interval = MiliSecondsToTomorrow();

        Dispatcher.Invoke(() =>
        {
            if (IsExistsNowDateInShowingDates())
                Update();
        });

        _timer.Start();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _dayOfCells.SelectionChanged += OnDayOfCellsSelectionChanged;

        foreach (var item in _calendarCellItems)
            item.MouseDoubleClick += OnCellItemsMouseDoubleClick;

        UpdateDayOfWeeksLine(UpdateEffectType.None);
        UpdateDayOfCell(UpdateEffectType.None);
        OnSelectedDatesChanged(this, new DependencyPropertyChangedEventArgs(SelectedDatesProperty, null, this.SelectedDates));

        if (!IsAutoReloadOnDateChanges) return;
        _timer = new Timer();
        _timer.AutoReset = false;
        _timer.Elapsed += OnTimerElapsed;
        _timer.Interval = MiliSecondsToTomorrow();
        _timer.Start();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _dayOfCells.SelectionChanged -= OnDayOfCellsSelectionChanged;

        foreach (var item in _calendarCellItems)
            item.MouseDoubleClick -= OnCellItemsMouseDoubleClick;

        if (_timer is null) return;
        _timer.Elapsed -= OnTimerElapsed;
        _timer.Stop();
        _timer.Dispose();
    }

    private static int MiliSecondsToTomorrow()
    {
        var h = DateTime.Now.Hour;
        var m = DateTime.Now.Minute;
        var s = DateTime.Now.Second;

        var allDaySecond = 60 * 60 * 24;
        var nowSecond = s + m * 60 + h * 60 * 60;
        var margin = 1;

        var secondsToTomorrow = allDaySecond - nowSecond;

        return (secondsToTomorrow + margin) * 1000;
    }

    private Timer? _timer;

    static CustomizableCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(typeof(CustomizableCalendar))
        );
    }

    private enum UpdateEffectType
    {
        Forward,
        Backward,
        Reflesh,
        None,
    }

    private void RunEffect(UpdateEffectType updateEffectType)
    {
        if (updateEffectType == UpdateEffectType.Forward)
        {
            _transitionEffectContentControl.RunForwardEffect();
        }
        else if (updateEffectType == UpdateEffectType.Backward)
        {
            _transitionEffectContentControl.RunBackwardEffect();
        }
        else if (updateEffectType == UpdateEffectType.Reflesh)
        {
            _transitionEffectContentControl.RunReloadEffect();
        }
    }

    // Methods for retrieving a range of dates to be displayed in the calendar
    private static IEnumerable<DateTime> DatesOnAPage(int year, int month, DayOfWeek startDayOfWeek)
    {
        var firstDateOfMonth = new DateTime(year, month, 1);
        var firstDayOfWeek = firstDateOfMonth.DayOfWeek;
        var subLastMonday = startDayOfWeek - firstDayOfWeek;
        var firstDate = firstDateOfMonth.AddDays(subLastMonday);
        var endDate = firstDate.AddDays(CellsNum);

        for (var dateTime = firstDate; dateTime.Date < endDate.Date; dateTime = dateTime.AddDays(1))
            yield return dateTime;
    }

    private const int CellRowNum = 6; //Number of rows in calendar cell
    private const int CellColumnNum = 7; //Number of columns in calendar cell
    private const int CellsNum = CellRowNum * CellColumnNum; //Number of all cells on the calendar
}