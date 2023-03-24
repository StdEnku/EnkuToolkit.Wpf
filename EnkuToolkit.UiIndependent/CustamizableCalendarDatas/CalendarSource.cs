﻿namespace EnkuToolkit.UiIndependent.CustamizableCalendarDatas;

/// <summary>
/// CustamizableCalendarのSourceプロパティで使用するデータ。
/// クライアント側で継承して使用することを想定している。
/// </summary>
public class CalendarSource
{
    /// <summary>
    /// 対象の日付を表すプロパティ
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Dateプロパティが土曜日か日曜日の場合trueとなるプロパティ
    /// </summary>
    public bool IsHoliday => this.Date.DayOfWeek == DayOfWeek.Sunday || 
                             this.Date.DayOfWeek == DayOfWeek.Saturday ?
                             true : false;

    /// <summary>
    /// Dateプロパティが今日の日付の場合trueとなるプロパティ
    /// </summary>
    public bool IsToday => this.Date == DateTime.Today;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CalendarSource()
    {

    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public CalendarSource(DateTime date)
    {
        this.Date = date;
    }
}