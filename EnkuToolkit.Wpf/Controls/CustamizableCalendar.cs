namespace EnkuToolkit.Wpf.Controls;

using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

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

    static CustamizableCalendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustamizableCalendar), new FrameworkPropertyMetadata(typeof(CustamizableCalendar)));
    }
}