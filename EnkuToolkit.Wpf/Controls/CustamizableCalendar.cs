namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using UiIndependent.CustamizableCalendarDatas;
using System;
using System.Windows.Input;
using EnkuToolkit.Wpf.Controls.Internals;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// セルを簡単にカスタマイズ可能なカレンダーコントロール
/// </summary>
public class CustamizableCalendar : Control
{
    #region DayOfWeekSourceプロパティ
    /// <summary>
    /// 曜日行用のデータソースを表すプロパティ
    /// </summary>
    public static readonly DependencyProperty DayOfWeekSourceProperty
        = DependencyProperty.Register(
            nameof(DayOfWeekSource),
            typeof(List<CalendarDayOfWeekSource>),
            typeof(CustamizableCalendar),
            new FrameworkPropertyMetadata(new List<CalendarDayOfWeekSource>())
        );

    /// <summary>
    /// DayOfWeekSourceProperty用のCLRプロパティ
    /// </summary>
    public List<CalendarDayOfWeekSource> DayOfWeekSource
    {
        get => (List<CalendarDayOfWeekSource>)this.GetValue(DayOfWeekSourceProperty);
        set => this.SetValue(DayOfWeekSourceProperty, value);
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
            new PropertyMetadata(false)
        );

    /// <summary>
    /// IsStartWeekMondayProperty用のCLRプロパティ
    /// </summary>
    public bool IsStartWeekMonday
    {
        get => (bool)this.GetValue(IsStartWeekMondayProperty);
        set => this.SetValue(IsStartWeekMondayProperty, value);
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
            typeof(List<CalendarSource>),
            typeof(CustamizableCalendar),
            new PropertyMetadata(new List<CalendarSource>())
        );

    /// <summary>
    /// SourceProperty用のCLRプロパティ
    /// </summary>
    public List<CalendarSource> Source
    {
        get => (List<CalendarSource>)this.GetValue(SourceProperty);
        set => this.SetValue(SourceProperty, value);
    }
    #endregion

    #region TargetYearAndMonth依存関係プロパティ
    /// <summary>
    /// 表示したい年と月を表す依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TargetYearAndMonthProperty
        = DependencyProperty.Register(
            nameof(TargetYearAndMonth),
            typeof(YearAndMonth),
            typeof(CustamizableCalendar),
            new PropertyMetadata(new YearAndMonth(DateTime.Today.Year, DateTime.Today.Month))
        );

    /// <summary>
    /// TargetYearProperty用のCLRプロパティ
    /// </summary>
    public YearAndMonth TargetYearAndMonth
    {
        get => (YearAndMonth)this.GetValue(TargetYearAndMonthProperty);
        set => this.SetValue(TargetYearAndMonthProperty, value);
    }
    #endregion

    #region ToNextMonthButtonCommandProperty依存関係プロパティ
    internal static readonly DependencyProperty ToNextMonthButtonCommandProperty
        = DependencyProperty.Register(
            nameof(ToNextMonthButtonCommand),
            typeof(ICommand),
            typeof(CustamizableCalendar),
            new PropertyMetadata(null)
        );

    internal ICommand? ToNextMonthButtonCommand
    {
        get => this.GetValue(ToNextMonthButtonCommandProperty) as ICommand;
        set => this.SetValue(ToNextMonthButtonCommandProperty, value);
    }

    private void onNextMonthButtonClickedCommand()
    {
        this.TargetYearAndMonth = this.TargetYearAndMonth.ForwardMonth();
    }
    #endregion

    #region ToLastMonthButtonCommandProperty依存関係プロパティ
    internal static readonly DependencyProperty ToLastMonthButtonCommandProperty
        = DependencyProperty.Register(
            nameof(ToLastMonthButtonCommand),
            typeof(ICommand),
            typeof(CustamizableCalendar),
            new PropertyMetadata(null)
        );

    internal ICommand? ToLastMonthButtonCommand
    {
        get => this.GetValue(ToLastMonthButtonCommandProperty) as ICommand;
        set => this.SetValue(ToLastMonthButtonCommandProperty, value);
    }

    private void onBackMonthButtonClickedCommand()
    {
        this.TargetYearAndMonth = this.TargetYearAndMonth.BackwardMonth();
    }
    #endregion

    #region IsShowHeader依存関係プロパティ
    /// <summary>
    /// ヘッダー部を表示するかどうか指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty IsShowHeaderProperty
        = DependencyProperty.Register(
            nameof(IsShowHeader),
            typeof(bool),
            typeof(CustamizableCalendar),
            new PropertyMetadata(true)
        );

    /// <summary>
    /// IsShowHeaderProperty用のCLRプロパティ
    /// </summary>
    public bool IsShowHeader
    {
        get => (bool)this.GetValue(IsShowHeaderProperty);
        set => this.SetValue(IsShowHeaderProperty, value);
    }
    #endregion

    #region SelectionMode依存関係プロパティ
    /// <summary>
    /// カレンダーセルのSelectionModeを指定するためのプロパティ
    /// </summary>
    public static readonly DependencyProperty SelectionModeProperty
        = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(SelectionMode),
            typeof(CustamizableCalendar)
        );

    /// <summary>
    /// SelectionModeProperty用のCLRプロパティ
    /// </summary>
    public SelectionMode SelectionMode
    {
        get => (SelectionMode)this.GetValue(SelectionModeProperty);
        set => this.SetValue(SelectionModeProperty, value);
    }
    #endregion

    #region SelectedDatesProperty依存関係プロパティ
    /// <summary>
    /// 複数選択モードの状態で選択されている日付の一覧を取得するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty SelectedDatesProperty
        = DependencyProperty.Register(
            nameof(SelectedDates),
            typeof(IEnumerable<DateTime>),
            typeof(CustamizableCalendar)
        );

    /// <summary>
    /// SelectedDatesProperty用のCLRプロパティ
    /// </summary>
    public IEnumerable<DateTime>? SelectedDates
    {
        get => this.GetValue(SelectedDatesProperty) as IEnumerable<DateTime>;
        set => throw new InvalidOperationException("CustamizableCalendar.SelectedDatescan dependency property only be bound in OneWayToSource mode.");
    }
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CustamizableCalendar()
    {
        this.ToNextMonthButtonCommand = new InternalDelegateCommand(this.onNextMonthButtonClickedCommand);
        this.ToLastMonthButtonCommand = new InternalDelegateCommand(this.onBackMonthButtonClickedCommand);
        this.Loaded += CustamizableCalendar_Loaded;
    }

    private void CustamizableCalendar_Loaded(object sender, RoutedEventArgs e)
    {
        var calendarCells = (ListBox)this.GetTemplateChild("CalendarCells");
        calendarCells.SelectionChanged += CalendarCells_SelectionChanged;
    }

    private void CalendarCells_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var calendarCells = (ListBox)sender;
        var selectedDates = from calendarCell in calendarCells.Items.Cast<ListBoxItem>().ToList()
                            where calendarCell.IsSelected == true
                            select ((CalendarSource)calendarCell.Content).Date;

        this.SetValue(SelectedDatesProperty, selectedDates);
    }

    static CustamizableCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustamizableCalendar), new FrameworkPropertyMetadata(typeof(CustamizableCalendar)));
    }
}