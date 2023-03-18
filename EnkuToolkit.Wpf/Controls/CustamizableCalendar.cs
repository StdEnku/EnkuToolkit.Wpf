namespace EnkuToolkit.Wpf.Controls;

using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using UicCalendarSourceCollection = UiIndependent.Collections.CalendarSourceCollection;
using UicCalendarSource = UiIndependent.Collections.CalendarSource;
using UicICalendarSource = UiIndependent.Collections.ICalendarSource;
using System.Collections.Specialized;

/// <summary>
/// 本ライブラリのクライアントプロジェクトから
/// 通常のカスタムコントロールと同じxml名前空間で
/// CalendarSourceCollectionクラスを呼び出せるようにするためのクラス
/// </summary>
public class CalendarSourceCollection : UicCalendarSourceCollection { }

/// <summary>
/// 本ライブラリのクライアントプロジェクトから
/// 通常のカスタムコントロールと同じxml名前空間で
/// CalendarSourceクラスを呼び出せるようにするためのクラス
/// </summary>
public class CalendarSource : UicCalendarSource { }

/// <summary>
/// セルを簡単にカスタマイズ可能なカレンダーコントロール
/// </summary>
public class CustamizableCalendar : Control
{
    #region DayOfWeekSource依存関係プロパティ
    /// <summary>
    /// 曜日名セルに渡すデータを表す依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// セットできるアイテムの数は必ず7つである必要がある。
    /// それ以外の場合InvalidOperationExceptionが投げられる。
    /// </remarks>
    public static readonly DependencyProperty DayOfWeekSourceProperty
        = DependencyProperty.Register(
            nameof(DayOfWeekSource),
            typeof(IEnumerable),
            typeof(CustamizableCalendar),
            new PropertyMetadata(null, onDayOfWeekSourceChanged)
        );

    /// <summary>
    /// DayOfWeekSourceProperty用のCLRプロパティ
    /// </summary>
    public IEnumerable? DayOfWeekSource
    {
        get => (IEnumerable?)this.GetValue(DayOfWeekSourceProperty);
        set => this.SetValue(DayOfWeekSourceProperty, value);
    }

    private static void onDayOfWeekSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var newValue = (IEnumerable<object>)e.NewValue;
        var dayOfWeekCount = Enum.GetValues(typeof(DayOfWeek)).Length;

        if (newValue.Count() != dayOfWeekCount)
            throw new InvalidOperationException($"Only {dayOfWeekCount} elements can be specified for the WeekOfDaysSource property.");
    }
    #endregion

    #region DayOfWeekTemplate依存関係プロパティ
    /// <summary>
    /// 曜日名セル用のテンプレートを表す依存関係プロパティ
    /// DataContextにはDayOfWeekSourceプロパティの各要素が指定される。
    /// </summary>
    public static readonly DependencyProperty DayOfWeekTemplateProperty
        = DependencyProperty.Register(
            nameof(DayOfWeekTemplate),
            typeof(DataTemplate),
            typeof(CustamizableCalendar),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// DayOfWeekTemplateProperty用のCLRプロパティ
    /// </summary>
    public DataTemplate? DayOfWeekTemplate
    {
        get => (DataTemplate?)this.GetValue(DayOfWeekTemplateProperty);
        set => this.SetValue(DayOfWeekTemplateProperty, value);
    }
    #endregion

    #region IsStartWeekMonday依存関係プロパティ
    /// <summary>
    /// 最初の曜日を月曜日からにするか指定するための依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 月曜日からにする場合はTrueを指定する。
    /// 日曜日からにする場合はFalseを指定する。
    /// </remarks>
    public static readonly DependencyProperty IsStartWeekMondayProperty
        = DependencyProperty.Register(
            "IsStartWeekMonday",
            typeof(bool),
            typeof(CustamizableCalendar),
            new PropertyMetadata(false, onIsStartWeekMondayChanged)
        );

    /// <summary>
    /// IsStartWeekMondayProperty用のCLRプロパティ
    /// </summary>
    public bool IsStartWeekMonday
    {
        get => (bool)this.GetValue(IsStartWeekMondayProperty);
        set => this.SetValue(IsStartWeekMondayProperty, value);
    }

    private static void onIsStartWeekMondayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var custamizableCalendar = (CustamizableCalendar)d;
        custamizableCalendar.calendarReload();
    }
    #endregion

    #region CalendarCellTemplate依存関係プロパティ
    /// <summary>
    /// カレンダーセル用のデータテンプレートを表す依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty CellTemplateProperty
        = DependencyProperty.Register(
            nameof(CellTemplate),
            typeof(DataTemplate),
            typeof(CustamizableCalendar),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CellTemplateProperty用のCLRプロパティ
    /// </summary>
    public DataTemplate? CellTemplate
    {
        get => (DataTemplate?)this.GetValue(CellTemplateProperty);
        set => this.SetValue(CellTemplateProperty, value);
    }
    #endregion

    #region AutoGenCellTemplate依存関係プロパティ
    /// <summary>
    /// 自動生成されたカレンダーセル用のデータテンプレートを表す依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty AutoGenCellTemplateProperty
        = DependencyProperty.Register(
            nameof(AutoGenCellTemplate),
            typeof(DataTemplate),
            typeof(CustamizableCalendar),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// AutoGenCellTemplateProperty用のCLRプロパティ
    /// </summary>
    public DataTemplate? AutoGenCellTemplate
    {
        get => (DataTemplate?)this.GetValue(AutoGenCellTemplateProperty);
        set => this.SetValue(AutoGenCellTemplateProperty, value);
    }
    #endregion

    #region Source依存関係プロパティ
    /// <summary>
    /// カレンダーセル用のデータを表す依存関係プロパティ。
    /// 本プロパティの各要素がセルのデータコンテキストに指定される。
    /// 足りない場合は自動的に生成されたEnkuToolkit.UiIndependent.Collections.CalendarSource
    /// オブジェクトが指定される。
    /// </summary>
    public static readonly DependencyProperty SourceProperty
        = DependencyProperty.Register(
            nameof(Source),
            typeof(UicCalendarSourceCollection),
            typeof(CustamizableCalendar),
            new PropertyMetadata(
                new UicCalendarSourceCollection() 
                { 
                    Year=DateTime.Today.Year, 
                    Month=DateTime.Today.Month 
                }, 
                onSourceChanged
            )
        );

    /// <summary>
    /// SourceProperty用のCLRプロパティ
    /// </summary>
    public UicCalendarSourceCollection Source
    {
        get => (UicCalendarSourceCollection)this.GetValue(SourceProperty);
        set => this.SetValue(SourceProperty, value);
    }

    private static void onSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var custamizedCalendar = (CustamizableCalendar)d;
        var newSource = e.NewValue as UicCalendarSourceCollection;
        var oldSource = e.OldValue as UicCalendarSourceCollection;

        if (oldSource is not null)
            oldSource.CollectionChanged -= custamizedCalendar.onSourceCollectionChanged;

        if (newSource is not null)
            newSource.CollectionChanged += custamizedCalendar.onSourceCollectionChanged;

        custamizedCalendar.calendarReload();
    }

    // Sourceプロパティに指定されたコレクションのアイテム変化時に呼び出されるメソッド
    private void onSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        this.calendarReload();
    }
    #endregion

    //ある範囲のDateTimeのIEnumerableを取得するためのメソッド
    private static IEnumerable<DateTime> getDateTimeRange(DateTime startDate, DateTime endDate)
    {
        for (var i = startDate; i < endDate; i = i.AddDays(1))
            yield return i;
    }

    // カレンダーセルの再読み込み用メソッド
    private void calendarReload()
    {
        if (!this.IsLoaded) return;
        if (this.Source is null) throw new NullReferenceException("The CustamizableCalendar.Source property is null.");

        var listbox = (ListBox)this.GetTemplateChild("CalendarCells");
        listbox.Items.Clear();

        // 先月の日付を追加
        var firstDay = new DateTime(this.Source.Year, this.Source.Month, 1);
        var subLastMonday = (this.IsStartWeekMonday ? DayOfWeek.Monday : DayOfWeek.Sunday) - firstDay.DayOfWeek;
        var lastMonthDateTimes = getDateTimeRange(firstDay.AddDays(subLastMonday), firstDay);
        foreach (var i in lastMonthDateTimes)
        {
            var listBoxItem = new ListBoxItem()
            {
                Content = new UicCalendarSource { Day = i.Day },
                ContentTemplate = this.AutoGenCellTemplate,
                IsEnabled = false,
            };

            listbox.Items.Add(listBoxItem);
        }

        // 今月の日付を追加
        var daysInMonthNum = DateTime.DaysInMonth(this.Source.Year, this.Source.Month);
        UicICalendarSource? targetDay;
        foreach (var dayNum in Enumerable.Range(1, daysInMonthNum))
        {
            targetDay = (from i in this.Source
                         where i.Day == dayNum
                         select i).FirstOrDefault();

            var listBoxItem = new ListBoxItem()
            {
                Content = targetDay ?? new UicCalendarSource() { Day = dayNum },
                ContentTemplate = targetDay is null ? this.AutoGenCellTemplate : this.CellTemplate,
                IsEnabled = true,
            };
            listbox.Items.Add(listBoxItem);
        }

        // 来月の日付を追加
        var lastDayNum = DateTime.DaysInMonth(this.Source.Year, this.Source.Month);
        var lastDay = new DateTime(this.Source.Year, this.Source.Month, lastDayNum);
        var numberOfExtraCells = 43 - listbox.Items.Count;
        var nextMonthDateTimes = getDateTimeRange(lastDay.AddDays(1), lastDay.AddDays(numberOfExtraCells));
        foreach (var i in nextMonthDateTimes)
        {
            var listBoxItem = new ListBoxItem()
            {
                Content = new UicCalendarSource { Day = i.Day },
                ContentTemplate = this.AutoGenCellTemplate,
                IsEnabled = false,
            };

            listbox.Items.Add(listBoxItem);
        }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CustamizableCalendar()
    {
        this.Loaded += this.onLoaded;
    }

    // 本オブジェクトにてLoadedイベントが発火した際に実行されるメソッド
    private void onLoaded(object sender, RoutedEventArgs e)
    {
        this.calendarReload();
    }

    static CustamizableCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustamizableCalendar), new FrameworkPropertyMetadata(typeof(CustamizableCalendar)));
    }
}