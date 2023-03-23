namespace EnkuToolkit.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using UiIndependent.Items;
using System;
using System.Windows.Input;
using EnkuToolkit.Wpf.Controls.Internals;

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
            typeof(ObservableCollection<CalendarDayOfWeekSource>),
            typeof(CustamizableCalendar),
            new FrameworkPropertyMetadata(new ObservableCollection<CalendarDayOfWeekSource>())
        );

    /// <summary>
    /// DayOfWeekSourceProperty用のCLRプロパティ
    /// </summary>
    public ObservableCollection<CalendarDayOfWeekSource> DayOfWeekSource
    {
        get => (ObservableCollection<CalendarDayOfWeekSource>)this.GetValue(DayOfWeekSourceProperty);
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
            typeof(ObservableCollection<CalendarSource>),
            typeof(CustamizableCalendar),
            new PropertyMetadata(new ObservableCollection<CalendarSource>())
        );

    /// <summary>
    /// SourceProperty用のCLRプロパティ
    /// </summary>
    public ObservableCollection<CalendarSource> Source
    {
        get => (ObservableCollection<CalendarSource>)this.GetValue(SourceProperty);
        set => this.SetValue(SourceProperty, value);
    }
    #endregion

    #region Year依存関係プロパティ
    /// <summary>
    /// 表示したい年を表す依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty YearProperty
        = DependencyProperty.Register(
            nameof(Year),
            typeof(int),
            typeof(CustamizableCalendar),
            new PropertyMetadata(DateTime.Today.Year)
        );

    /// <summary>
    /// YearProperty用のCLRプロパティ
    /// </summary>
    public int Year
    {
        get => (int)this.GetValue(YearProperty);
        set => this.SetValue(YearProperty, value);
    }
    #endregion

    #region Month依存関係プロパティ
    /// <summary>
    /// 表示したい月を表す依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty MonthProperty
        = DependencyProperty.Register(
            nameof(Month),
            typeof(int),
            typeof(CustamizableCalendar),
            new PropertyMetadata(DateTime.Today.Month)
        );

    /// <summary>
    /// MonthProperty用のCLRプロパティ
    /// </summary>
    public int Month
    {
        get => (int)this.GetValue(MonthProperty);
        set => this.SetValue(MonthProperty, value);
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
        if (this.Month == 12)
        {
            this.Month = 1;
            this.Year++;
        }
        else
        {
            this.Month++;
        }
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

    private void onLastMonthButtonClickedCommand()
    {
        if (this.Month == 1)
        {
            this.Month = 12;
            this.Year--;
        }
        else
        {
            this.Month--;
        }
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

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CustamizableCalendar()
    {
        this.ToNextMonthButtonCommand = new InternalDelegateCommand(this.onNextMonthButtonClickedCommand);
        this.ToLastMonthButtonCommand = new InternalDelegateCommand(this.onLastMonthButtonClickedCommand);
    }

    static CustamizableCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustamizableCalendar), new FrameworkPropertyMetadata(typeof(CustamizableCalendar)));
    }
}