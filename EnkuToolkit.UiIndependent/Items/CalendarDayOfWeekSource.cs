namespace EnkuToolkit.UiIndependent.Items;

/// <summary>
/// CustamizableCalendarのDayOfWeekSourceプロパティで使用する曜日行をカスタマイズするときに使用するデータ
/// </summary>
public class CalendarDayOfWeekSource
{
    /// <summary>
    /// 設定対象の曜日
    /// </summary>
    [System.ComponentModel.Bindable(true)]
    public DayOfWeek TargetDayOfWeek { get; init; }

    /// <summary>
    /// 表示するテキスト
    /// </summary>
    [System.ComponentModel.Bindable(true)]
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// 対象の曜日が休日がどうかを取得するためのプロパティ
    /// </summary>
    [System.ComponentModel.Bindable(true)]
    public bool IsHoliday => this.TargetDayOfWeek == DayOfWeek.Saturday ||
                             this.TargetDayOfWeek == DayOfWeek.Sunday ?
                             true : false;
}