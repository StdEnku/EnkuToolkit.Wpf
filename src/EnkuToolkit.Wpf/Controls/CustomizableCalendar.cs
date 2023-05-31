namespace EnkuToolkit.Wpf.Controls;

using EnkuToolkit.Wpf.DataObjects;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using EnkuToolkit.UiIndependent.Collections;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Animation;
using EnkuToolkit.Wpf.Constants;
using System.Diagnostics;
using System.Timers;

/// <summary>
/// Customizable calendar control with customizable cells
/// </summary>
public class CustomizableCalendar : Control
{
    #region Dependency property to specify whether the first day of the week is Monday
    /// <summary>
    /// Dependency property to specify whether the first day of the week is Monday
    /// </summary>
    public static readonly DependencyProperty IsStartMondayProperty
        = DependencyProperty.Register(
            nameof(IsStartMonday),
            typeof(bool),
            typeof(CustomizableCalendar),
            new PropertyMetadata(true)
        );

    /// <summary>
    /// CLR property for IsStartMondayProperty, a dependency property for specifying whether the first day of the week is Monday
    /// </summary>
    public bool IsStartMonday
    {
        get => (bool)GetValue(IsStartMondayProperty);
        set => SetValue(IsStartMondayProperty, value);
    }
    #endregion

    #region Dependency property representing the template to be applied in the day-of-week display line
    /// <summary>
    /// Dependency property representing the template to be applied in the day-of-week display line
    /// </summary>
    public static readonly DependencyProperty DayOfWeekLineTemplateProperty
        = DependencyProperty.Register(
            nameof(DayOfWeekLineTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendar)
        );

    /// <summary>
    /// CLR property for the DaysOfWeekRowTemplateProperty, a dependency property that represents the template to be applied in the weekday row
    /// </summary>
    public DataTemplate DayOfWeekLineTemplate
    {
        get => (DataTemplate)GetValue(DayOfWeekLineTemplateProperty);
        set => SetValue(DayOfWeekLineTemplateProperty, value);
    }
    #endregion

    #region Dependency property that is the source to the day-of-week display line
    /// <summary>
    /// Dependency property that is the source to the day-of-week display line
    /// </summary>
    public static readonly DependencyProperty DayOfWeekLineSourceProperty
        = DependencyProperty.Register(
            nameof(DayOfWeekLineSource),
            typeof(List<DayOfWeekData>),
            typeof(CustomizableCalendar),
            new PropertyMetadata(new List<DayOfWeekData>())
        );

    /// <summary>
    /// CLR property for the DayOfWeekLineSourceProperty, a dependency property that is the source to the weekday display line
    /// </summary>
    public List<DayOfWeekData> DayOfWeekLineSource
    {
        get => (List<DayOfWeekData>)GetValue(DayOfWeekLineSourceProperty);
        set => SetValue(DayOfWeekLineSourceProperty, value);
    }
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
            new PropertyMetadata(new DayDataCollection(DateTime.Now.Year, DateTime.Now.Month))
        );

    /// <summary>
    /// CLR property of the dependency property SourceProperty, which is the Source of the calendar cell
    /// </summary>
    public DayDataCollection Source
    {
        get => (DayDataCollection)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
    #endregion

    #region Template dependency properties for cells for which data is provided in the Source property
    /// <summary>
    /// Template dependency properties for cells for which data is provided in the Source property
    /// </summary>
    public static readonly DependencyProperty HasDataCellTemplateProperty
        = DependencyProperty.Register(
            nameof(HasDataCellTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendar)
        );

    /// <summary>
    /// CLR property for HasDataCellTemplateProperty, which is a dependency property of the template for the cell whose data is provided in the Source property
    /// </summary>
    public DataTemplate HasDataCellTemplate
    {
        get => (DataTemplate)GetValue(HasDataCellTemplateProperty);
        set => SetValue(HasDataCellTemplateProperty, value);
    }
    #endregion

    #region Template dependency properties for auto-generated cells
    /// <summary>
    /// Template dependency properties for auto-generated cells
    /// </summary>
    public static readonly DependencyProperty AutoGenCellTemplateProperty
        = DependencyProperty.Register(
            nameof(AutoGenCellTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendar)
        );

    /// <summary>
    /// CLR property for AutoGenCellTemplateProperty, a dependency property of the template for auto-generated cells
    /// </summary>
    public DataTemplate AutoGenCellTemplate
    {
        get => (DataTemplate)GetValue(AutoGenCellTemplateProperty);
        set => SetValue(AutoGenCellTemplateProperty, value);
    }
    #endregion

    #region Template dependency properties for cells of the month not covered
    /// <summary>
    /// Template dependency properties for cells of the month not covered
    /// </summary>
    public static readonly DependencyProperty OtherMonthCellTemplateProperty
        = DependencyProperty.Register(
            nameof(OtherMonthCellTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendar)
        );

    /// <summary>
    /// CLR property for theOtherMonthCellTemplateProperty, which is a dependency property of the template for the cells of the month not covered
    /// </summary>
    public DataTemplate OtherMonthCellTemplate
    {
        get => (DataTemplate)GetValue(OtherMonthCellTemplateProperty);
        set => SetValue(OtherMonthCellTemplateProperty, value);
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

    #region Dependency property to get the currently selected item
    /// <summary>
    /// Dependency property to get the currently selected item
    /// </summary>
    public static readonly DependencyProperty SelectedDatesProperty
        = DependencyProperty.Register(
            nameof(SelectedDates),
            typeof(IEnumerable<DateTime>),
            typeof(CustomizableCalendar),
            new FrameworkPropertyMetadata(new List<DateTime>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

    /// <summary>
    /// CLR property for SelectedDatesProperty, a dependency property for retrieving the currently selected item
    /// </summary>
    public IEnumerable<DateTime> SelectedDates
    {
        get => (IEnumerable<DateTime>)GetValue(SelectedDatesProperty);
        set => SetValue(SelectedDatesProperty, value);
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

    #region RoutedEvent that can return the date of the double click
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
        /// Constructor
        /// </summary>
        /// <param name="routedEvent">The routed event identifier for this instance of the RoutedEventArgs class</param>
        /// <param name="dateTime">Date double-clicked</param>
        public CellDoubleClickedEventArgs(RoutedEvent routedEvent, DateTime dateTime) : base(routedEvent)
        {
            DateTime = dateTime;
        }
    }

    /// <summary>
    /// Handler for CellDoubleClickedEvent, which fires when a calendar cell is double-clicked.
    /// </summary>
    /// <param name="sender">Event Originator</param>
    /// <param name="e">Event argument containing the date of the double click</param>
    public delegate void CellDoubleClickedEventHandler(object sender, CellDoubleClickedEventArgs e);

    /// <summary>
    /// Event fired when a calendar cell is double-clicked
    /// </summary>
    public static readonly RoutedEvent CellDoubleClickedEvent 
        = EventManager.RegisterRoutedEvent(
            nameof(CellDoubleClicked), 
            RoutingStrategy.Bubble, 
            typeof(CellDoubleClickedEventHandler), 
            typeof(CustomizableCalendar)
        );

    /// <summary>
    /// CLR event for CellDoubleClickedEvent, a RoutedEvent that fires when a calendar cell is double-clicked.
    /// </summary>
    public event CellDoubleClickedEventHandler CellDoubleClicked
    {
        add { AddHandler(CellDoubleClickedEvent, value); }
        remove { RemoveHandler(CellDoubleClickedEvent, value); }
    }

    private void RaiseCellDoubleClickedEvent(DateTime dateTime)
    {
        var args = new CellDoubleClickedEventArgs(CellDoubleClickedEvent, dateTime);
        RaiseEvent(args);
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
            new PropertyMetadata(true)
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

    /// <summary>
    /// Methods for reloading the display content
    /// </summary>
    public void Reload()
    {
        _customizableCalendarDays.Reload();
    }

    /// <summary>
    /// Processing after template activation
    /// </summary>
    public override void OnApplyTemplate()
    {
        var calendarDays = _customizableCalendarDays;
        calendarDays.CellDoubleClicked += OnCalendarDaysCellDoubleClicked;
        calendarDays.Updated += OnCalendarDaysUpdated;
        calendarDays.Updating += OnCalendarDaysUpdating;

        base.OnApplyTemplate();
    }

    private CustomizableCalendarDays _customizableCalendarDays => (CustomizableCalendarDays)GetTemplateChild("calendarDays");

    private static void OnCalendarDaysUpdating(object? sender, EventArgs e)
    {
        var days = sender as CustomizableCalendarDays;
        Debug.Assert(days is not null);

        var customizableCalendar = (CustomizableCalendar)days.TemplatedParent;
        var transitionEffectContentControl = (TransitionEffectContentControl)customizableCalendar.GetTemplateChild("transitionEffectContentControl");
        transitionEffectContentControl.Snapshot();
    }

    private static void OnCalendarDaysUpdated(DependencyObject d, CustomizableCalendarDays.CalendarDaysUpdatedEventArgs args)
    {
        var days = (CustomizableCalendarDays)d;
        var customizableCalendar = (CustomizableCalendar)days.TemplatedParent;
        var transitionEffectContentControl = (TransitionEffectContentControl)customizableCalendar.GetTemplateChild("transitionEffectContentControl");
        var updateType = args.UpdatedType;
        if (updateType == CustomizableCalendarDays.CalendarDaysUpdatedType.Forward)
        {
            transitionEffectContentControl.RunForwardEffect();
        }
        else if (updateType == CustomizableCalendarDays.CalendarDaysUpdatedType.Backward)
        {
            transitionEffectContentControl.RunBackwardEffect();
        }
        else
        {
            transitionEffectContentControl.RunReloadwardEffect();
        }
    }

    private static void OnCalendarDaysCellDoubleClicked(object sender, CellDoubleClickedEventArgs e)
    {
        var calendarDays = (CustomizableCalendarDays)sender;
        var custamizableCalendar = (CustomizableCalendar)calendarDays.TemplatedParent;
        custamizableCalendar.RaiseCellDoubleClickedEvent(e.DateTime);

        if (custamizableCalendar.CellDoubleClickedCommand?.CanExecute(e.DateTime) ?? false)
            custamizableCalendar.CellDoubleClickedCommand.Execute(e.DateTime);
    }

    #region Mechanism for redrawing when the date changes during startup
    /// <summary>
    /// Constructor
    /// </summary>
    public CustomizableCalendar()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Debug.Assert(_timer is not null);
        _timer.Stop();
        _timer.Interval = SecondsToTomorrow();
        Reload();
        _timer.Start();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Debug.Assert(_timer is not null);
        _timer.Elapsed -= OnTimerElapsed;
        _timer.Stop();
        _timer.Dispose();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _timer = new Timer();
        _timer.AutoReset = false;
        _timer.Elapsed += OnTimerElapsed;
        _timer.Interval = SecondsToTomorrow();
        _timer.Start();
    }

    private static int SecondsToTomorrow()
    {
        var h = DateTime.Now.Hour;
        var m = DateTime.Now.Minute;
        var s = DateTime.Now.Second;

        var aDaySecond = 60 * 60 * 24;
        var nowSecond = s + m * 60 + h * 60 * 60;
        var margin = 1;
        return (aDaySecond - nowSecond) + margin;
    }

    private Timer? _timer;
    #endregion

    static CustomizableCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomizableCalendar), 
            new FrameworkPropertyMetadata(typeof(CustomizableCalendar))
        );
    }
}