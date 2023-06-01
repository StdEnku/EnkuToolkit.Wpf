namespace EnkuToolkit.Wpf.Controls;

using EnkuToolkit.Wpf.DataObjects;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Globalization;

internal class CustomizableCalendarDayOfWeekLine : Control
{
    #region IsStartMonday
    public static readonly DependencyProperty IsStartMondayProperty
        = DependencyProperty.Register(
            nameof(IsStartMonday),
            typeof(bool),
            typeof(CustomizableCalendarDayOfWeekLine),
            new PropertyMetadata(true, OnIsStartMondayChanged)
        );

    public bool IsStartMonday
    {
        get => (bool)GetValue(IsStartMondayProperty);
        set => SetValue(IsStartMondayProperty, value);
    }

    private static void OnIsStartMondayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var dayOfWeekLine = (CustomizableCalendarDayOfWeekLine)d;
        if (dayOfWeekLine.IsLoaded)
            dayOfWeekLine.Update();
    }
    #endregion

    #region Source
    private static IEnumerable<DayOfWeekData> _defaultDayOfWeekLineSource
    {
        get
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            var dayOfWeekNames = cultureInfo.DateTimeFormat.AbbreviatedDayNames;
            foreach (var dayOfWeek in (IEnumerable<DayOfWeek>)Enum.GetValues(typeof(DayOfWeek)))
                yield return new DayOfWeekData() { DayOfWeek = dayOfWeek, Text = dayOfWeekNames[(int)dayOfWeek] };
        }
    }

    public static readonly DependencyProperty SourceProperty
        = DependencyProperty.Register(
            nameof(Source),
            typeof(List<DayOfWeekData>),
            typeof(CustomizableCalendarDayOfWeekLine),
            new PropertyMetadata(new List<DayOfWeekData>(), OnSourceChanged)
        );

    public List<DayOfWeekData> Source
    {
        get => (List<DayOfWeekData>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var baseDaysOfWeekDatas = (IEnumerable<DayOfWeekData>)e.NewValue;

        if (baseDaysOfWeekDatas.Count() != 0)
        {
            if (baseDaysOfWeekDatas.Count() != 7)
                throw new InvalidOperationException("The number of elements in the value set in the CustamizableCalendar.DayOfWeekLineSource property is not 7.");

            var duplicates = (from baseDaysOfWeekData in baseDaysOfWeekDatas
                              group baseDaysOfWeekData by baseDaysOfWeekData.DayOfWeek into duplicateGroup
                              where duplicateGroup.Count() > 1
                              select duplicateGroup).FirstOrDefault() is not null;

            if (duplicates)
                throw new InvalidOperationException("A duplicate day of the week is specified in the value element set to the CustamizableCalendar.DayOfWeekLineSource property.");
        }

        var dayOfWeekLine = (CustomizableCalendarDayOfWeekLine)d;
        if (dayOfWeekLine.IsLoaded)
            dayOfWeekLine.Update();
    }
    #endregion

    #region ItemTemplate
    public static readonly DependencyProperty ItemTemplateProperty
        = DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(CustomizableCalendarDayOfWeekLine)
        );

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
    #endregion

    #region CellMargin
    public static readonly DependencyProperty CellMarginProperty
        = DependencyProperty.Register(
            nameof(CellMargin),
            typeof(Thickness),
            typeof(CustomizableCalendarDayOfWeekLine),
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
            typeof(CustomizableCalendarDayOfWeekLine),
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
            typeof(CustomizableCalendarDayOfWeekLine),
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
            typeof(CustomizableCalendarDayOfWeekLine),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent))
        );

    public Brush CellBorderBrush
    {
        get => (Brush)GetValue(CellBorderBrushProperty);
        set => SetValue(CellBorderBrushProperty, value);
    }
    #endregion

    #region For update methods
    private IEnumerable<ContentControl> _items
    {
        get
        {
            foreach (var i in Enumerable.Range(0, 7))
                yield return (ContentControl)GetTemplateChild($"col{i}");
        }
    }

    private void Update()
    {
        var custamizableCalendar = (CustomizableCalendar)TemplatedParent;

        var source = Source.Count() == 0 ? _defaultDayOfWeekLineSource : Source;
        var sortedSource = (from dayOfWeekRowSource in source
                            orderby dayOfWeekRowSource.DayOfWeek
                            select dayOfWeekRowSource);

        foreach (var itr in _items.Zip(sortedSource, (item, source) => new { Item = item, Content = source }))
            itr.Item.Content = itr.Content;
    }
    #endregion

    public override void OnApplyTemplate()
    {
        Update();
        base.OnApplyTemplate();
    }

    static CustomizableCalendarDayOfWeekLine()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomizableCalendarDayOfWeekLine),
            new FrameworkPropertyMetadata(typeof(CustomizableCalendarDayOfWeekLine))
        );
    }
}